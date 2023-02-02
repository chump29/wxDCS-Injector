using System.Collections.Generic;
using System.Linq;

namespace wxDCS_Injector.Helper
{
    static class Theatre
    {
        static readonly List<TheatreData> Theatres;

        enum Hemispheres
        {
            North = 30,
            South = -30
        }

        static Theatre()
        {
            Theatres = new List<TheatreData>
            {
                new() { Name = "Caucasus", Hemisphere = (int)Hemispheres.North, UTC = 4 },
                new() { Name = "Falklands", Hemisphere = (int)Hemispheres.South, UTC = -3 },
                new() { Name = "MarianaIslands", Hemisphere = (int)Hemispheres.North, UTC = 10 },
                new() { Name = "Nevada", Hemisphere = (int)Hemispheres.North, UTC = -8 },
                // Normandy
                new() { Name = "PersianGulf", Hemisphere = (int)Hemispheres.North, UTC = 4 },
                new() { Name = "Syria", Hemisphere = (int)Hemispheres.North, UTC = 3 }
                // TheChannel
            };
        }

        internal static TheatreData GetTheatreData(string theatre) => Theatres.SingleOrDefault(t => t.Name == theatre);
    }

    class TheatreData
    {
        public string Name { get; set; }
        public int Hemisphere { get; set; }
        public int UTC { get; set; }
    }
}
