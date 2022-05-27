using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    /// <summary>
    /// Timetable contains (generates) all the Roadlines until the end of the month. 
    /// Meaning clients can't book (or reserve) a drive in June,
    /// no matter what day of May is today.
    /// </summary>
    internal class Timetable
    {
        public List<RoadLine> Roads { get; set; }
    }
}
