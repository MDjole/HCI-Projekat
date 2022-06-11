using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    internal class Station
    {

        public static List<Station> AllStations = new List<Station>()
        { new Station("Beograd Centar", 328.6, 282),
          new Station("Novi Sad ZS", 240.6, 181),
          new Station("Nis ZS", 539.6, 585),
          new Station("Kragujevac ZS", 396.6, 452),
          new Station("Jagodina ZS", 461.6, 471)};

        public string Icon = "../../Data/station_30x30.png";

        public double Xpos { get; set; }
        public double Ypos { get; set; }
        public string Name { get; set; }
        public Station(string name, double xpos, double ypos)
        {
            Name = name;
            Xpos = xpos;
            Ypos = ypos;
        }
        public bool Equals(Station other)
        {
            return Name == other.Name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
