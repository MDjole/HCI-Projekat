﻿using SerbRailway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway
{
    /// <summary>
    /// Here we generate Mock Data for RoadLine, Timetable and Tickets.
    /// This class is called when a user (Client or Manager) successfully logs in.
    /// </summary>
    internal class ModelStateInitializer
    {
        private readonly DateTime Today = DateTime.Now.Date;
        public ModelStateInitializer()
        {
            InitializeModel();
        }

        public void InitializeModel()
        {
            InitializeRoadLines();
        }

        public void InitializeRoadLines()
        {
            List<int> travelDays = new List<int>() { 1,2,3,4,5 };
            List<RoadLine> roads = new List<RoadLine>();
            roads.Add(new RoadLine() 
            {
                LineNumber = 1,
                Origin = new Station("Beograd Centar"),
                Destination = new Station("Novi Sad ZS"),
                Train = Train.AllTrains[2],
                TravelDays = travelDays,
                TravelStartHour = TimeSpan.FromHours(12),
                ETA = TimeSpan.FromHours(1),
            });
            roads.Add(new RoadLine()
            {
                LineNumber = 2,
                Origin = new Station("Nis ZS"),
                Destination = new Station("Kragujevac ZS"),
                Train = Train.AllTrains[1],
                TravelDays = travelDays,
                TravelStartHour = TimeSpan.FromHours(12),
                ETA = TimeSpan.FromHours(2),
            });
            roads.Add(new RoadLine()
            {
                LineNumber = 3,
                Origin = new Station("Trebinje ZS"),
                Destination = new Station("Beograd Centar"),
                Train = Train.AllTrains[0],
                TravelDays = travelDays,
                TravelStartHour = TimeSpan.FromHours(12),
                ETA = TimeSpan.FromHours(1),
            });
            roads.Add(new RoadLine()
            {
                LineNumber = 4,
                Origin = new Station("Novi Sad ZS"),
                Destination = new Station("Kragujevac ZS"),
                Train = Train.AllTrains[2],
                TravelDays = travelDays,
                TravelStartHour = TimeSpan.FromHours(14),
                ETA = TimeSpan.FromHours(2),
            });
            roads.Add(new RoadLine() 
            {
                LineNumber = 5,
                Origin = new Station("Beograd Centar"),
                Destination = new Station("Nis ZS"),
                Train = Train.AllTrains[3],
                TravelDays = travelDays,
                TravelStartHour = TimeSpan.FromHours(12),
                ETA = TimeSpan.FromHours(1),
            });
            
            foreach (RoadLine line in roads)
            {
                Timetable.AddRoadline(line);
            }
        }

    }
}