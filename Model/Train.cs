using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    internal class Train
    {
        private static int NextId = 0;
        private int id { get; set; }
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
            this.Name = name;
            this.id = NextId;
            NextId++;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Train() { 
            this.Name = "";
            this.id = NextId;
            NextId++;
        }

        public bool Equals(Train other)
        {
            return id == other.GetId();
        }

        public int GetId() { return id; }

        public static bool TrainNameExists(Train other)
        {
            foreach (Train t in AllTrains)
            {
                if (t.Name.Equals(other.Name)) {
                    return true;
                }
            }
            return false;
        }
    }
}
