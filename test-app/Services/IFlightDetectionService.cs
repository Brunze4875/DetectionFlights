using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.Models;

namespace test_app.Services
{
    public interface IFlightDetectionService
    {
        public List<FlightResult> detectFlights(int agencyId, DateOnly startDate, DateOnly endDate);
    }
}
