using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using NLua;
using wxDCS_Injector.Presentation;
using static wxDCS_Injector.Helper.Convert;

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

        string _strMission;
        Lua _lua;

        public InjectService(ILog log) => _log = log;

        public void InjectMETAR(string fileName, Schema.METAR metar)
        {
            try
            {
                using (var fileStream = File.Open(fileName, FileMode.Open))
                {
                    Log("Opening file...");
                    using (var zip = new ZipArchive(fileStream, ZipArchiveMode.Update))
                    {
                        Log("Opening mission...");
                        var mission = zip.GetEntry("mission");
                        if (mission == null)
                            throw new Exception("'mission' not found");

                        using (var reader = new StreamReader(mission.Open()))
                            _strMission = reader.ReadToEnd();
                        if (_strMission.Length == 0)
                            throw new Exception("'mission' is empty");

                        _lua = new Lua();
                        _lua.DoString(_strMission);

                        Log("Updating mission...");
                        Change("mission.weather.season.temperature", metar.temp_c);
                        Change("mission.weather.wind.atGround.dir", metar.wind_dir_degrees);
                        Change("mission.weather.wind.atGround.speed", KtsToMps(metar.wind_speed_kt));
                        Change("mission.weather.qnh", InHgToMmHg(metar.altim_in_hg));
                        var clouds = metar.sky_condition.ToList().OrderBy(x => x.cloud_base_ft_agl).FirstOrDefault();
                        var cloudBase = FtToM(clouds?.cloud_base_ft_agl ?? 0);
                        Change("mission.weather.clouds.base", cloudBase);
                        Change("mission.weather.clouds.thickness", cloudBase > 0 ? 200 : 0); // DCS default
                        Change("mission.weather.clouds.density", GetSkyCover(clouds?.sky_cover ?? string.Empty));
                        Change("mission.weather.clouds.iprecptns", GetPrecipitation(metar.precip_in, metar.snow_in));
                        var vis = MiToM(metar.visibility_statute_mi);
                        Change("mission.weather.enable_fog", vis <= 5000F);
                        Change("mission.weather.fog.visibility", vis);
                        Change("mission.weather.fog.thickness", vis * 0.1F);
                        if (UseCurrentDate)
                        {
                            Change("mission.date.Year", DateTime.Now.Year);
                            Change("mission.date.Day", DateTime.Now.Day);
                            Change("mission.date.Month", DateTime.Now.Month);
                        }
                        if (UseCurrentTime)
                            Change("mission.start_time", (int)new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).TotalSeconds);

                        _strMission = $"mission={Print(_lua.GetTable("mission"))}";

                        mission.Delete();

                        Log("Writing mission...");
                        mission = zip.CreateEntry("mission");

                        Log("Writing file...");
                        using (var writer = new StreamWriter(mission.Open()))
                            writer.Write(_strMission);
                    }
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

        static int GetSkyCover(string cover)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            switch (cover)
            {
                case "FEW":
                    return rand.Next(1, 2);
                case "SCT":
                    return rand.Next(3, 5);
                case "BKN":
                    return rand.Next(6, 8);
                case "OVC":
                case "OVX":
                    return rand.Next(9, 10);
                default: // SKC/CLR/CAVOK
                    return 0;
            }
        }

        static int GetPrecipitation(float? rain, float? snow)
        {
            switch (snow)
            {
                case null when rain < 0.3F:
                    return 1;
                case null when rain >= 0.3F:
                    return 2;
            }

            switch (rain)
            {
                case null when snow < 0.5F:
                    return 3;
                case null when snow >= 0.5F:
                    return 4;
            }

            return 0;
        }

        static object Print(object obj)
        {
            if (obj is LuaTable table)
            {
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

            switch (obj)
            {
                case string _:
                    return $"\"{obj}\"";
                case bool _:
                    return obj.ToString().ToLower();
                default:
                    return obj;
            }
        }

        void Change(string key, object val)
        {
            if (val == null || _lua[key] == val)
                return;

            Log($"{key}: {_lua[key]} -> {val}");
            _lua[key] = val;
        }

        void Success(string txt) => _log.Success(txt);

        void Error(string txt, Exception ex) => _log.Error(txt, ex);

        void Log(string txt) => _log.Log(txt);

        public bool UseCurrentDate { get; set; }

        public bool UseCurrentTime { get; set; }
    }
}
