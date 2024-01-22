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
        int? id = HttpContext.Session.GetInt32("uid");
        if (id == null) return RedirectToAction("Login", "Login");
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
        FlightsJay? flight = db.FlightsJays.Include(f => f.AirlineCodeNavigation).Include(f => f.DepartureAirportCodeNavigation).Include(f => f.ArrivalCodeNavigation).FirstOrDefault(f => f.FlightNumber == id);
        return View(flight);
    }

    [HttpGet]
    public IActionResult Bookings()
    {
        int? id = HttpContext.Session.GetInt32("uid");
        if (id == null) return RedirectToAction("Login", "Login");

        return View(db.BookingsJays.Where(b => b.PassengerId == id));
    }

    [HttpGet]
    public IActionResult DetailsBooking(int id)
    {
        BookingsJay? booking = db.BookingsJays.Include(x => x.FlightNumberNavigation).ThenInclude(f => f.AirlineCodeNavigation).Include(x => x.FlightNumberNavigation).ThenInclude(f => f.ArrivalCodeNavigation).Include(x => x.FlightNumberNavigation).ThenInclude(f => f.DepartureAirportCodeNavigation).FirstOrDefault(y => y.BookingId == id);
        if (booking == null) return RedirectToAction("Bookings");
        return View(booking);
    }

    [HttpGet]
    public IActionResult BookFlight(string id)
    {
        if (HttpContext.Session.GetInt32("uid") == null) return RedirectToAction("Login", "Login");
        FlightsJay? flight = db.FlightsJays.Find(id);
        if (flight == null) return RedirectToAction("ErrorPage", new { msg = "No flights were found" });

        BookingsJay booking = new()
        {
            FlightNumber = flight.FlightNumber,
            PassengerId = HttpContext.Session.GetInt32("uid"),
            BookingDate = DateTime.Now,
            TotalCost = flight.TicketPrice
        };

        return View(booking);
    }

    [HttpPost]
    public IActionResult BookFlight(BookingsJay booking)
    {
        FlightsJay? flight = db.FlightsJays.Find(booking.FlightNumber);
        if (flight == null) return RedirectToAction("ErrorPage", new { msg = "No flights were found" });

        flight.AvailableSeats -= booking.Passengers;

        if (flight.AvailableSeats < 0) return RedirectToAction("ErrorPage", new { msg = "Sorry! " + booking.Passengers + " seats not available" });

        booking.TotalCost *= booking.Passengers;
        booking.PassengerId = HttpContext.Session.GetInt32("uid");

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

    [HttpGet]
    public IActionResult ErrorPage(string? msg)
    {
        ViewBag.msg = msg;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
