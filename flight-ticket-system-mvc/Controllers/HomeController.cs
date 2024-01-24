using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
    public async Task<IActionResult> Profile()
    {
        int? id = HttpContext.Session.GetInt32("uid");
        if (id == null) return RedirectToAction("Login", "Login");
        
        PassengersJay? user = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5049/api/user/Passenger/{id}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            user = JsonConvert.DeserializeObject<PassengersJay>(usersRes);
        }

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Profile(int id, PassengersJay passenger)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        Console.WriteLine("passenger: " + passenger.PassengerId);
        Console.WriteLine("id: " + id);

        HttpContent content = new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json");

        HttpResponseMessage res = await client.PutAsync($"http://localhost:5049/api/user/Passenger/{id}", content);

        if (!res.IsSuccessStatusCode)
        {
            return RedirectToAction("ErrorPage", new { msg = "Error in updating flight" });
        }
        
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> FlightDetails(string id)
    {
        FlightsJay? flight = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5049/api/user/Flight/{id}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flight = JsonConvert.DeserializeObject<FlightsJay>(usersRes);
        }

        return View(flight);
    }

    [HttpGet]
    public async Task<IActionResult> Bookings()
    {
        int? id = HttpContext.Session.GetInt32("uid");
        if (id == null) return RedirectToAction("Login", "Login");

        List<BookingsJay>? bookings = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5049/api/user/Passenger/view-bookings/{id}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            bookings = JsonConvert.DeserializeObject<List<BookingsJay>>(usersRes);
        }
        return View(bookings);
    }

    [HttpGet]
    public async Task<IActionResult> DetailsBooking(int id)
    {
        BookingsJay? booking = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5049/api/user/Passenger/details-booking/{id}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            booking = JsonConvert.DeserializeObject<BookingsJay>(usersRes);
        }

        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> BookFlight(string id)
    {
        if (HttpContext.Session.GetInt32("uid") == null) return RedirectToAction("Login", "Login");

        FlightsJay? flight = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5049/api/user/Flight/{id}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flight = JsonConvert.DeserializeObject<FlightsJay>(usersRes);
        }

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
    public async Task<IActionResult> BookFlight(BookingsJay booking)
    {
        FlightsJay? flight = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // getting flight //

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5049/api/user/Flight/{booking.FlightNumber}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flight = JsonConvert.DeserializeObject<FlightsJay>(usersRes);
        }

        if (flight == null) return RedirectToAction("ErrorPage", new { msg = "No flights were found" });

        if (flight.AvailableSeats < 0) return RedirectToAction("ErrorPage", new { msg = "Sorry! " + booking.Passengers + " seats not available" });

        // updating flight //

        flight.AvailableSeats -= booking.Passengers;

        HttpContent content = new StringContent(JsonConvert.SerializeObject(flight), Encoding.UTF8, "application/json");

        res = await client.PutAsync($"http://localhost:5049/api/user/Flight/update-flight", content);

        if (!res.IsSuccessStatusCode)
        {
            return RedirectToAction("ErrorPage", new { msg = "Error in updating flight" });
        }

        // making a booking //

        booking.TotalCost *= booking.Passengers;
        booking.PassengerId = HttpContext.Session.GetInt32("uid");

        var bookingJson = JsonConvert.SerializeObject(booking);
        content = new StringContent(bookingJson, Encoding.UTF8, "application/json");

        res = await client.PostAsync("http://localhost:5049/api/user/Flight/book-flight", content);

        if (!res.IsSuccessStatusCode)
        {
            return RedirectToAction("ErrorPage", new { msg = "Error in making a booking" });
        }

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
