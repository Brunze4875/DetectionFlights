using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.DataAccess.DatabaseContext;
using test_app.DataAccess.Repositories;
using test_app.DataAccess.Repositories.Interfaces;
using test_app.Models;

namespace test_app.Services
{
    public class FlightDetectionService : IFlightDetectionService
    {

        private readonly IFlightRepository _flightRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public FlightDetectionService(IFlightRepository flightRepository, IRouteRepository routeRepository, ISubscriptionRepository subscriptionRepository)
        {
            _flightRepository = flightRepository;
            _routeRepository = routeRepository;
            _subscriptionRepository = subscriptionRepository;
        }
        public List<FlightResult> detectFlights(int agencyId, DateOnly startDate, DateOnly endDate)
        {
            // get flights if date of route is between specified dates
            DateOnly prevStartDate = DateOnly.FromDateTime(startDate.ToDateTime(TimeOnly.MinValue).AddDays(-7).AddMinutes(-30));
            DateOnly nextEndDate = DateOnly.FromDateTime(endDate.ToDateTime(TimeOnly.MinValue).AddDays(7).AddMinutes(30));


            // get all subs by agencyID
            var subscriptions = _subscriptionRepository.GetSubscriptionsByAgencyId(agencyId);

            // get all routes with obtained subs
            var routes = _routeRepository.GetRoutesBySubscriptions(subscriptions);


            // get all flights with filtered routes
            var flights = _flightRepository.GetFlightsByRouteId(routes.Where(r => r.DepartureDate >= prevStartDate &&
                                                                                  r.DepartureDate <= nextEndDate).ToList());

            // group
            var groupedFlights = flights.GroupBy(f => new { f.Route.OriginCityId, f.Route.DestinationCityId, f.AirlineId }).ToDictionary(g => g.Key, g => g.ToList());

            // fill result list
            List<FlightResult> result = new List<FlightResult>();
            foreach (var group in groupedFlights)
            {
                foreach (var item in group.Value)
                {
                    // don't know is this flight new or discontinued - skip
                    bool hasOtherFlights = group.Value.Where(g => g.Id != item.Id).Count() == 0 ? false : true;


                    // check only if departure date between input dates
                    if (item.Route.DepartureDate <= startDate || item.Route.DepartureDate >= endDate || !hasOtherFlights)
                    {
                        continue;
                    }
                    var prevDepTime = item.DepartureTime.AddDays(-7);
                    var nextDepTime = item.DepartureTime.AddDays(7);


                    // check if current flight is new
                    int countPast = group.Value.Where(g => g.Id != item.Id).Select(g => g.DepartureTime).Count(date => date >= prevDepTime.AddMinutes(-30) &&
                                                                                        date <= prevDepTime.AddMinutes(30));


                    // check if current flight is discontinued
                    int countFuture = group.Value.Where(g => g.Id != item.Id).Select(g => g.DepartureTime).Count(date => date >= nextDepTime.AddMinutes(-30) &&
                                                                                        date <= nextDepTime.AddMinutes(30));


                    // new or discontinued ??
                    if (countPast == 0 && countFuture == 0) { continue; }

                    if (countFuture == 0)
                    {
                        FlightResult res = new FlightResult();
                        res.FlightId = item.Id;
                        res.OriginCityId = item.Route.OriginCityId;
                        res.DestinationCityId = item.Route.DestinationCityId;
                        res.DepartureTime = item.DepartureTime;
                        res.ArrivalTime = item.ArrivalTime;
                        res.AirlineId = item.AirlineId;
                        res.status = "DISCONTINUED";
                        result.Add(res);

                    }


                    if (countPast == 0)
                    {
                        FlightResult res = new FlightResult();
                        res.FlightId = item.Id;
                        res.OriginCityId = item.Route.OriginCityId;
                        res.DestinationCityId = item.Route.DestinationCityId;
                        res.DepartureTime = item.DepartureTime;
                        res.ArrivalTime = item.ArrivalTime;
                        res.AirlineId = item.AirlineId;
                        res.status = "NEW";
                        result.Add(res);

                    }
                }



            }

            return result;

        }

    }
}

