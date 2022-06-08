using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model
{
    public enum Status
    {
        RESERVED,
        BOUGHT
    }
    internal class Ticket
    {
        public static List<Ticket> AllTickets = new List<Ticket>();

        public int Id { get; set; }
        public Status Status { get; set; }
        public RoadLine Line { get; set; }
        public Client Owner { get; set; }
        public DateTime DateSold { get; set; }
        public DateTime TravelDate { get; set; }

        public static List<Ticket> GetAllClientTickets(Client c)
        {
            List<Ticket> allTickets = new List<Ticket>();
            foreach(Ticket t in AllTickets)
            {
                if (t.Owner.Equals(c))
                    allTickets.Add(t);
            }
            return allTickets;
        }


    }
}
