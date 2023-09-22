using Microsoft.EntityFrameworkCore;
using test_app.DataAccess.DatabaseContext;
using test_app.DataAccess.Repositories;
using test_app.DataAccess.Repositories.Interfaces;
using test_app.Helpers;
using test_app.Models;
using test_app.Services;

int rowsCount = 0;
List <Route> routes = new List<Route>();
List <Flight> flights = new List<Flight>();
List <Subscription> subscriptions = new List<Subscription>();
List<FlightResult> result = new List<FlightResult>();

using (var context = new AirlineContext())
{
    IRouteRepository routeRepository = new RouteRepository(context);
    IFlightRepository flightRepository = new FlightRepository(context);
    ISubscriptionRepository subscriptionRepository = new SubscriptionRepository(context);

    IFlightDetectionService ws = new FlightDetectionService(flightRepository, routeRepository, subscriptionRepository);

    Console.WriteLine("Create or updating database structure...");
    context.Database.Migrate();

    rowsCount = context.Routes.Count();
    if (rowsCount == 0)
    {
        Console.WriteLine("Reading routes from CSV...");
        routes = CSVHelper.ReadRoutesCsv("files/routes.csv");
    }

    rowsCount = context.Flights.Count();
    if (rowsCount == 0)
    {
        Console.WriteLine("Reading flights from CSV...");
        flights = CSVHelper.ReadFlightsCsv("files/flights.csv");
    }

    rowsCount = context.Subscriptions.Count();
    if (rowsCount == 0)
    {
        Console.WriteLine("Reading subscriptions from CSV...");
        subscriptions = CSVHelper.ReadSubscriptionsCsv("files/subscriptions.csv");
    }

    Console.WriteLine("Inserting...");
    context.AddRange(routes);
    context.AddRange(flights);
    context.AddRange(subscriptions);
    context.SaveChanges();
    Console.WriteLine("Done!");

    Console.WriteLine("Detection flights...");
    var watch = System.Diagnostics.Stopwatch.StartNew();
    result = ws.detectFlights(1,new DateOnly(2018, 01, 01), new DateOnly(2018, 01, 15));
    watch.Stop();
    Console.WriteLine("Execution time of algorithm: " + watch.Elapsed.TotalSeconds);
    Console.WriteLine("Writing csv...");
    CSVHelper.WriteCsv(result);
    Console.WriteLine("Done!");

}

