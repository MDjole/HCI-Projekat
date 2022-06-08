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
        { new Station("Beograd Centar"),
          new Station("Novi Sad ZS"),
          new Station("Nis ZS"),
          new Station("Kragujevac ZS"),
          new Station("Trebinje ZS")};
        public string Name { get; set; }
        public Station(string name)
        {
            Name = name;
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
