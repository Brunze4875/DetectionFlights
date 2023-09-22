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
    public class RouteRepository : IRouteRepository
    {
        private readonly AirlineContext _context;

        public RouteRepository(AirlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Route> GetRoutesBySubscriptions(List<Subscription> subscriptions)
        {
            var subsOriginCityIds = subscriptions.Select(s => s.OriginCityId).ToList();
            var destinationCityIds = subscriptions.Select(s => s.DestinationCityId).ToList();

            return _context.Routes.Where(r => subsOriginCityIds.Contains(r.OriginCityId) && destinationCityIds.Contains(r.DestinationCityId)).ToList();

        }

    }
}
