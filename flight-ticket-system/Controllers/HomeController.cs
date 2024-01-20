using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using flight_ticket_system.Models;

namespace flight_ticket_system.Controllers;

public class HomeController(Ace52024Context _db) : Controller
{
    private readonly Ace52024Context db = _db;

    private List<FlightsJay>? searchedFlights;

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.username = HttpContext.Session.GetString("uname");
        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(SearchFlight searchFlight)
    {
        DateTime? dep = searchFlight.DepartureDateTime;
        string? depCode = searchFlight.DepartureAirportCode;
        string? arrCode = searchFlight.ArrivalAirportCode;

        List<FlightsJay> flights = [.. db.FlightsJays.Where(f => dep != null ? f.DepartureDateTime == dep : true && depCode != null ? f.DepartureAirportCode == depCode : true && arrCode != null ? f.ArrivalCode == arrCode : true)];

        searchedFlights = flights;

        ViewBag.searchedFlights = searchedFlights;

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
