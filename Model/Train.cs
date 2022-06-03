using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    internal class Train
    {
        public static List<Train> AllTrains = new List<Train>()
        {
            new Train("Voz 2203"),
            new Train("Voz 2212"),
            new Train("Soko 3143"),
            new Train("Soko 1565")
        };
        public string Name { get; set; }
        public Train(string name)
        {
            Name = name;
        }
    }
}
