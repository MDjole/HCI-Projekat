using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    public class Day
    {

        public string Name { get; set; }

        public int Number { get; set; }

        public Day() { }

        public Day(string Name, int Number)
        {
            this.Name = Name;
            this.Number = Number;
        }

    }
}
