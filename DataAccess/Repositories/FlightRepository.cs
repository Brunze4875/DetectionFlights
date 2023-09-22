using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.DataAccess.DatabaseContext;
using test_app.DataAccess.Repositories.Interfaces;
using test_app.Models;

namespace test_app.DataAccess.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirlineContext _context;

        public FlightRepository(AirlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Flight> GetFlightsByRouteId(List<Route> routes)
        {
            var routeIds = routes.Select(r => r.Id).ToList();
            return _context.Flights.Where(f => routeIds.Contains(f.RouteId)).ToList();
        }
       
    }
}
