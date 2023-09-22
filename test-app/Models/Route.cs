using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    public class Route
    {
        public int Id { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateOnly DepartureDate { get;set; }
        public ICollection<Flight> Flights { get; set;}
    }
}
