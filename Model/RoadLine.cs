using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    /// <summary>
    /// For RoadLine we define it's identifying number;
    /// Manager defines it's Origin, Destination, days of the week that it travels (TravelDays)
    /// time of the day that it starts (TravelStartHour) and how long it travels (Estimated Time of Arrival - ETA)
    /// and the Train that travels.
    /// </summary>
    internal class RoadLine
    {
        public int LineNumber { get; set; }
        public Station Origin { get; set; }
        public Station Destination { get; set; }
        public Train Train { get; set; }
        public List<int> TravelDays { get; set; }
        public TimeSpan TravelStartHour { get; set; }
        public TimeSpan ETA { get; set; }

    }
}
