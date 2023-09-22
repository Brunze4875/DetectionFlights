using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using test_app.Models;

namespace test_app.Helpers
{
    public class CSVHelper
    {
        public static List<Flight> ReadFlightsCsv(string path)
        {
            if (path == null) throw new ArgumentNullException("path missing");

            var flights = new List<Flight>();

            foreach (var item in File.ReadLines(path).Skip(1))
            {
                var data = item.Split(',');
                flights.Add(new Flight
                {
                    Id = int.Parse(data[0]),
                    RouteId = int.Parse(data[1]),
                    DepartureTime = DateTime.Parse(data[2]),
                    ArrivalTime = DateTime.Parse(data[3]),
                    AirlineId = int.Parse(data[4])
                });

            }
            return flights;
        }

        public static List<Route> ReadRoutesCsv(string path)
        {
            if (path == null) throw new ArgumentNullException("path missing");

            var route = new List<Route>();

            foreach (var item in File.ReadLines(path).Skip(1))
            {
                var data = item.Split(',');
                route.Add(new Route
                {
                    Id = int.Parse(data[0]),
                    OriginCityId = int.Parse(data[1]),
                    DestinationCityId = int.Parse(data[2]),
                    DepartureDate = DateOnly.Parse(data[3])

                });

            }
            return route;
        }

        public static List<Subscription> ReadSubscriptionsCsv(string path)
        {
            if (path == null) throw new ArgumentNullException("path missing");

            var subscriptions = new List<Subscription>();

            foreach (var item in File.ReadLines(path).Skip(1))
            {
                var data = item.Split(',');
                subscriptions.Add(new Subscription
                {
                    AgencyId = int.Parse(data[0]),
                    OriginCityId = int.Parse(data[1]),
                    DestinationCityId = int.Parse(data[2])
                });

            }
            return subscriptions;
        }

        public static void WriteCsv (List<FlightResult> flightResult)
        {
            string format = "MMddyyyyhhmmsstt";
            string filePath = "result_" + DateTime.Now.ToString(format) + ".csv";

            PropertyInfo[] properties = typeof(FlightResult).GetProperties();
            string propertyNames = string.Empty;
            
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (PropertyInfo property in properties)
                {
                    propertyNames += property.Name + ";";
                }
                file.WriteLine(propertyNames);

                foreach(FlightResult result in flightResult)
                {
                    file.WriteLine($"{result.FlightId};" +
                        $"{result.OriginCityId};" +
                        $"{result.DestinationCityId};" +
                        $"{result.DepartureTime};" +
                        $"{result.ArrivalTime};" +
                        $"{result.AirlineId};" +
                        $"{result.status}");
                }
            }

        }
    }

}

