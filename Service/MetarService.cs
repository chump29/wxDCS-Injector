using System;
using System.Linq;
using RestSharp;

namespace wxDCS_Injector.Service
{
    interface IMetarService
    {
        METARdto GetMETAR(string icao);
    }

    class MetarService : IMetarService
    {
        public METARdto GetMETAR(string icao)
        {
            try
            {
                var client = new RestClient("https://www.aviationweather.gov/adds/dataserver_current/httpparam");

                var request = new RestRequest()
                    .AddQueryParameter("dataSource", "metars")
                    .AddQueryParameter("requestType", "retrieve")
                    .AddQueryParameter("format", "xml")
                    .AddQueryParameter("stationString", icao)
                    .AddQueryParameter("hoursBeforeNow", 1)
                    .AddQueryParameter("mostRecent", true);

                var response = client.Get<Schema.response>(request);

                if (!string.IsNullOrEmpty(response.errors.error))
                    throw new Exception(response.errors.error);

                if (response.data.METAR == null)
                    throw new Exception($"No data found for {icao}");

                return new METARdto { METAR = response.data.METAR.FirstOrDefault() };
            }
            catch (Exception ex)
            {
                return new METARdto { Error = ex };
            }
        }
    }

    class METARdto
    {
        public Schema.METAR METAR { get; set; }
        public Exception Error { get; set; }
    }
}
