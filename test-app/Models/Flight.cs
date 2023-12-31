﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirlineId { get; set; }

        [ForeignKey("RouteId")]
        public Route Route { get; set; }
    }
}
