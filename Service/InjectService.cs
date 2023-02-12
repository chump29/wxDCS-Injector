using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NLua;
using wxDCS_Injector.Helper;
using wxDCS_Injector.Presentation;
using static wxDCS_Injector.Helper.Convert;
using static wxDCS_Injector.Helper.Theatre;

namespace wxDCS_Injector.Service
{
    interface IInjectService
    {
        void InjectMETAR(string fileName, Schema.METAR metar);

        bool UseCurrentDate { get; set; }

        bool UseCurrentTime { get; set; }
    }

    class InjectService : IInjectService
    {
        readonly ILog _log;

        static string _strMission;
        static Lua _lua;

        static Random _rand;

        static TheatreData _theatreData;

        const float WindSpeedIncrease = 2.15F;

        public InjectService(ILog log)
        {
            _log = log;
            _rand = new Random(Guid.NewGuid().GetHashCode());
        }

        public void InjectMETAR(string fileName, Schema.METAR metar)
        {
            try
            {
                using (var fileStream = File.Open(fileName, FileMode.Open))
                {
                    Log("Opening file...");
                    using var zip = new ZipArchive(fileStream, ZipArchiveMode.Update);

                    Log("Opening mission...");
                    var mission = zip.GetEntry("mission");
                    if (mission == null)
                        throw new Exception("'mission' not found");

                    using (var reader = new StreamReader(mission.Open()))
                        _strMission = reader.ReadToEnd();
                    if (_strMission.Length == 0)
                        throw new Exception("'mission' is empty");

                    _lua = new Lua();
                    _lua.State.Encoding = Encoding.UTF8;

                    _lua.DoString(_strMission);

                    _theatreData = GetTheatreData(_lua["mission.theatre"].ToString());

                    Log("Updating mission...");
                    Change("mission.weather.season.temperature", metar.temp_c);
                    var dir = ReverseDirection(metar.wind_dir_degrees);
                    var speed = KtsToMps(metar.wind_speed_kt);
                    Change("mission.weather.wind.atGround.dir", dir);
                    Change("mission.weather.wind.atGround.speed", speed);
                    var newDir = GetWindDirection(dir);
                    var newSpeed = GetWindSpeed(speed) * WindSpeedIncrease;
                    Change("mission.weather.wind.at2000.dir", newDir);
                    Change("mission.weather.wind.at2000.speed", newSpeed);
                    newDir = GetWindDirection(newDir);
                    newSpeed = GetWindSpeed(newSpeed);
                    Change("mission.weather.wind.at8000.dir", newDir);
                    Change("mission.weather.wind.at8000.speed", newSpeed);
                    Change("mission.weather.qnh", InHgToMmHg(metar.altim_in_hg));
                    Change("mission.weather.clouds.preset", null); // clear
                    var clouds = metar.sky_condition.ToList().OrderBy(x => x.cloud_base_ft_agl).FirstOrDefault();
                    var cloudBase = FtToM(clouds?.cloud_base_ft_agl ?? 0);
                    Change("mission.weather.clouds.base", cloudBase);
                    Change("mission.weather.clouds.thickness", cloudBase > 0 ? 200 : 0); // DCS default
                    Change("mission.weather.clouds.density", GetSkyCover(clouds?.sky_cover ?? string.Empty));
                    Change("mission.weather.clouds.iprecptns", GetPrecipitation(metar.precip_in, metar.snow_in));
                    var vis = MiToM(metar.visibility_statute_mi);
                    var hasFog = vis <= 5000F;
                    Change("mission.weather.enable_fog", hasFog);
                    Change("mission.weather.fog.visibility", hasFog ? vis : 0F);
                    Change("mission.weather.fog.thickness", hasFog ? vis * 0.1F : 0F);

                    if (UseCurrentDate || UseCurrentTime)
                        if (DateTimeOffset.TryParse(metar.observation_time, out var dt))
                        {
                            if (UseCurrentDate)
                            {
                                Change("mission.date.Day", int.Parse(dt.ToString("dd")));
                                Change("mission.date.Month", int.Parse(dt.ToString("MM")));
                                Change("mission.date.Year", int.Parse(dt.ToString("yyyy")));
                            }
                            if (UseCurrentTime)
                                Change("mission.start_time", (int)new TimeSpan(dt.AddHours(_theatreData.UTC).Hour, dt.Minute, dt.Second).TotalSeconds);
                        }
                        else
                            throw new Exception("Could not parse date/time");

                    _strMission = $"mission={Print(_lua.GetTable("mission"))}";
                    _strMission = $"-- Injected METAR: {metar.station_id}\n{_strMission}";

                    mission.Delete();

                    Log("Writing mission...");
                    mission = zip.CreateEntry("mission");

                    Log("Writing file...");
                    using var writer = new StreamWriter(mission.Open());
                    writer.Write(_strMission);
                }
                _strMission = null;
                _lua = null;
                Success("Done!");
            }
            catch (Exception ex)
            {
                Error("Error reading/writing mission file", ex);
            }
        }

        static int GetWindDirection(int dir)
        {
            var val = _theatreData?.Hemisphere ?? 0;
            var newDir = dir + (val > 0 ? _rand.Next(0, val + 1) : _rand.Next(val, 1));
            return newDir > 360 ? newDir - 360 : newDir;
        }

        static float GetWindSpeed(float speed) => speed * (_rand.Next(-10, 11) / 100F + 1F);

        static int GetSkyCover(string cover)
        {
            return cover switch
            {
                "FEW" => _rand.Next(1, 3),
                "SCT" => _rand.Next(3, 6),
                "BKN" => _rand.Next(6, 9),
                "OVC" => _rand.Next(9, 11),
                "OVX" => _rand.Next(9, 11),
                _ => 0 // SKC/CLR/CAVOK
            };
        }

        static int GetPrecipitation(float? rain, float? snow)
        {
            return snow switch
            {
                null when rain < 0.3F => 1,
                null when rain >= 0.3F => 2,
                _ => rain switch
                {
                    null when snow < 0.5F => 3,
                    null when snow >= 0.5F => 4,
                    _ => 0
                }
            };
        }

        static object Print(object obj)
        {
            if (obj is not LuaTable table)
                return obj switch
                {
                    string => $"\"{obj.ToString().Replace("\"", "\\\"")}\"",
                    bool => obj.ToString().ToLower(),
                    _ => obj
                };

            var retVal = "{";
            foreach (KeyValuePair<object, object> tbl in table)
            {
                var key = tbl.Key;
                if (key is string)
                    key = $"\"{key}\"";
                retVal += $"[{key}]={Print(tbl.Value)},";
            }
            return $"{retVal}}}";
        }

        void Change(string key, object val)
        {
            if (_lua[key] == null)
                return;

            Log($"{key}: {_lua[key]} -> {val ?? "null"}");
            _lua[key] = val;
        }

        void Success(string txt) => _log.Success(txt);

        void Error(string txt, Exception ex) => _log.Error(txt, ex);

        void Log(string txt) => _log.Log(txt);

        public bool UseCurrentDate { get; set; }

        public bool UseCurrentTime { get; set; }
    }
}
