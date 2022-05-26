using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway
{
    public enum Status
    {
        FREE,
        RESERVED,
        BOUGHT
    }
    internal class Ticket
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public RoadLine Line { get; set; }
        public Client Owner { get; set; }
        public DateTime DateSold { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime ETA { get; set; }
        //public int TravelTimeHours { get; set; }
        //public int TravelTimeMinutes { get; set; }
    }
}
