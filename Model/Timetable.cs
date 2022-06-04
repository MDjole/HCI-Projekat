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
        private static DateTime Today = DateTime.Now.Date;
        private static int DayOfWeek = (int)Today.DayOfWeek;

        private static int daysInMonth = DateTime.DaysInMonth(Today.Year, Today.Month);
        private static DateTime EndOfMonth = new DateTime(Today.Year, Today.Month, daysInMonth);
        
        public Dictionary<RoadLine, List<DateTime>> Roads = new Dictionary<RoadLine, List<DateTime>>();

        public List<RoadLine> GetRoadLinesInDate(DateTime date)
        {
            List<RoadLine> list = new List<RoadLine>();
            foreach (RoadLine rl in Roads.Keys)
            {
                if (Roads[rl].Contains(date))
                {
                    list.Add(rl);
                }
            }
            return list;
        }

        public void AddRoadline(RoadLine rl)
        {
            List<DateTime> dates = new List<DateTime>();
            Roads.Add(rl, dates);
            foreach (DateTime day in EachDay(Today, EndOfMonth))
            {
                if (rl.TravelDays.Contains((int)day.DayOfWeek))
                {
                    Roads[rl].Add(day);
                }
            }
        }
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (DateTime day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

    }
}
