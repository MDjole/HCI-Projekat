using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    internal class Schedule
    {
        public const string format = @"([0-1][0-9]|2[0-4]):[0-5][0-9]";
        public int LineNumber { get; set; }
        public List<string> StartTimes { get; set; }

        public Schedule() { }
        public Schedule(int line, List<string> times)
        {
            this.LineNumber = line;
            this.StartTimes = times;
        }

        public static List<Schedule> AllSchedules = new List<Schedule>()
        {
            new Schedule(1,new List<string> { "08:15", "12:45", "18:30"}),
            new Schedule(2,new List<string> { "06:30", "07:45", "11:50", "19:30"}),
            new Schedule(3,new List<string> { "12:30", "16:45", "22:30"}),
            new Schedule(4,new List<string> { "01:30", "06:20", "12:40"}),
        };

        public static bool scheduleExists(Schedule newSchedule)
        {
            foreach(Schedule s in AllSchedules)
            {
                if (s.LineNumber == newSchedule.LineNumber) return true;
            }
            return false;
        }
    }
}
