using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace flight_ticket_system.Controllers;

public class HomeController(Ace52024Context _db) : Controller
{
    private readonly Ace52024Context db = _db;

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.username = HttpContext.Session.GetString("uname");
        List<string?> departureCodes = [.. db.FlightsJays.Select(f => f.DepartureAirportCode).Distinct()];
        List<string?> arrivalCodes = [.. db.FlightsJays.Select(f => f.ArrivalCode).Distinct()];

        ViewBag.depCodes = new SelectList(departureCodes);
        ViewBag.arrCodes = new SelectList(arrivalCodes);

        Console.WriteLine(ViewBag.depCodes);
        return View();
    }

    [HttpPost]
    public IActionResult Index(SearchFlight searchFlight)
    {
        DateTime? dep = searchFlight.DepartureDateTime;
        string? depCode = searchFlight.DepartureAirportCode;
        string? arrCode = searchFlight.ArrivalAirportCode;

        List<FlightsJay> flights = [.. db.FlightsJays.Where(f => (dep == null || f.DepartureDateTime >= dep) && (depCode == null || f.DepartureAirportCode == depCode) && (arrCode == null || f.ArrivalCode == arrCode))];

        ViewBag.searchedFlights = flights;
        List<string?> departureCodes = db.FlightsJays.Select(f => f.DepartureAirportCode).Distinct().ToList();
        List<string?> arrivalCodes = db.FlightsJays.Select(f => f.ArrivalCode).Distinct().ToList();

        ViewBag.depCodes = new SelectList(departureCodes);
        ViewBag.arrCodes = new SelectList(arrivalCodes);

        ViewBag.username = HttpContext.Session.GetString("uname");

        return View("Index", searchFlight);
    }

    [HttpGet]
    public IActionResult Profile()
    {
        int id = ViewBag.username = HttpContext.Session.GetInt32("uid");
        return View(db.PassengersJays.Find(id));
    }

    [HttpPost]
    public IActionResult Profile(PassengersJay passenger)
    {
        db.PassengersJays.Update(passenger);
        db.SaveChanges();
        return View();
    }

    [HttpGet]
    public IActionResult FlightDetails(string id)
    {
        return View(db.FlightsJays.Find(id));
    }

    [HttpGet]
    public IActionResult Bookings()
    {
        int? id = HttpContext.Session.GetInt32("uid");
        if(id == null) return RedirectToAction("Login", "Login");

        return View(db.BookingsJays.Find(id));
    }

    [HttpGet]
    public IActionResult DetailsBooking(int id)
    {
        BookingsJay? bookings = db.BookingsJays.Include(x => x.FlightNumberNavigation).FirstOrDefault(y => y.PassengerId == id);
        if(bookings == null) return RedirectToAction("Bookings");
        return View(bookings);
    }

    [HttpGet]
    public IActionResult BookFlight(string id)
    {
        if(HttpContext.Session.GetInt32("uid") == null) return RedirectToAction("Login", "Login");
        FlightsJay? flight = db.FlightsJays.Find(id);
        if(flight == null) return Error();

        BookingsJay booking = new() {
            FlightNumber = flight.FlightNumber,
            PassengerId = HttpContext.Session.GetInt32("uid"),
            BookingDate = DateTime.Now
        };

        return View(booking);
    }

    [HttpPost]
    public IActionResult BookFlight(BookingsJay booking)
    {
        FlightsJay? flight = db.FlightsJays.Find(booking.FlightNumber);
        if(flight == null) return Error();

        flight.AvailableSeats -= booking.Passengers;
        
        db.FlightsJays.Update(flight);
        db.BookingsJays.Add(booking);
        db.SaveChanges();

        return RedirectToAction("Bookings");
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
