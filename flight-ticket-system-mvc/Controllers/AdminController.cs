using System.Net.Http.Headers;
using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace flight_ticket_system.Controllers;

public class AdminController(Ace52024Context _db) : Controller
{
    private readonly Ace52024Context db = _db;

    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View();
    }

    ////////// USERS //////////

    [HttpGet]
    public async Task<IActionResult> ShowUsers()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        List<PassengersJay>? users = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Passenger");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            users = JsonConvert.DeserializeObject<List<PassengersJay>>(usersRes);
        }

        return View(users);
    }

    [HttpGet]
    public IActionResult DeleteUser(int id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        PassengersJay? user = db.PassengersJays.Find(id);
        return View(user);
    }

    [HttpPost]
    [ActionName("DeleteUser")]
    public IActionResult DeleteUserPost(int id)
    {
        PassengersJay? user = db.PassengersJays.Find(id);
        if (user != null) db.PassengersJays.Remove(user);
        db.SaveChanges();
        return RedirectToAction("ShowUsers");
    }

    [HttpGet]
    public async Task<IActionResult> EditUserAsync(int id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        PassengersJay? user = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Passenger/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            user = JsonConvert.DeserializeObject<PassengersJay>(usersRes);
        }

        return View(user);
    }

    [HttpPost]
    public IActionResult EditUser(PassengersJay user)
    {
        db.PassengersJays.Update(user);
        db.SaveChanges();
        return RedirectToAction("ShowUsers");
    }

    [HttpGet]
    public IActionResult DetailsUser(int id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        PassengersJay? user = db.PassengersJays.Find(id);
        return View(user);
    }

    ////////// USERS //////////

    ////////// FLIGHTS //////////

    [HttpGet]
    public IActionResult ShowFlights()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.FlightsJays);
    }

    [HttpGet]
    public IActionResult AddFlight()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        ViewBag.airports = new SelectList(db.AirportsJays.Select(x => x.AirportCode));
        ViewBag.airlines = new SelectList(db.AirlinesJays.Select(x => x.AirlineCode));

        return View();
    }

    [HttpPost]
    public IActionResult AddFlight(FlightsJay flight)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        db.FlightsJays.Add(flight);
        db.SaveChanges();
        return RedirectToAction("ShowFlights");
    }

    [HttpGet]
    public IActionResult EditFlight(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.FlightsJays.Find(id));
    }

    [HttpPost]
    public IActionResult EditFlight(FlightsJay flight)
    {
        db.FlightsJays.Update(flight);
        db.SaveChanges();
        return RedirectToAction("ShowFlights");
    }

    [HttpGet]
    public IActionResult DeleteFlight(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.FlightsJays.Find(id));
    }

    [HttpPost]
    [ActionName("DeleteFlight")]
    public IActionResult DeleteFlightPost(string id)
    {
        FlightsJay? flight = db.FlightsJays.Find(id);
        if (flight != null) db.FlightsJays.Remove(flight);
        db.SaveChanges();
        return RedirectToAction("ShowFlights");
    }

    [HttpGet]
    public IActionResult DetailsFlight(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        FlightsJay? flight = db.FlightsJays.Include(f => f.AirlineCodeNavigation).Include(f => f.DepartureAirportCodeNavigation).Include(f => f.ArrivalCodeNavigation).FirstOrDefault(f => f.FlightNumber == id);
        return View(flight);
    }

    ////////// FLIGHTS //////////

    ////////// AIRLINES //////////

    [HttpGet]
    public IActionResult ShowAirlines()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.AirlinesJays);
    }

    [HttpGet]
    public IActionResult AddAirline()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View();
    }

    [HttpPost]
    public IActionResult AddAirline(AirlinesJay airline)
    {
        db.AirlinesJays.Add(airline);
        db.SaveChanges();
        return RedirectToAction("ShowAirlines");
    }

    [HttpGet]
    public IActionResult EditAirline(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.AirlinesJays.Find(id));
    }

    [HttpPost]
    public IActionResult EditAirline(AirlinesJay airline)
    {
        db.AirlinesJays.Update(airline);
        db.SaveChanges();
        return RedirectToAction("ShowAirlines");
    }

    [HttpGet]
    public IActionResult DeleteAirline(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.AirlinesJays.Find(id));
    }

    [HttpPost]
    [ActionName("DeleteAirline")]
    public IActionResult DeleteAirlinePost(string id)
    {
        AirlinesJay? airline = db.AirlinesJays.Find(id);
        if (airline != null) db.AirlinesJays.Remove(airline);
        db.SaveChanges();
        return RedirectToAction("ShowAirlines");
    }

    ////////// AIRLINES //////////

    ////////// AIRPORTS //////////

    [HttpGet]
    public IActionResult ShowAirports()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.AirportsJays);
    }

    [HttpGet]
    public IActionResult AddAirport()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View();
    }

    [HttpPost]
    public IActionResult AddAirport(AirportsJay airport)
    {
        db.AirportsJays.Add(airport);
        db.SaveChanges();
        return RedirectToAction("ShowAirports");
    }

    [HttpGet]
    public IActionResult EditAirport(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.AirportsJays.Find(id));
    }

    [HttpPost]
    public IActionResult EditAirport(AirportsJay airport)
    {
        db.AirportsJays.Update(airport);
        db.SaveChanges();
        return RedirectToAction("ShowAirports");
    }

    [HttpGet]
    public IActionResult DeleteAirport(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View(db.AirportsJays.Find(id));
    }

    [HttpPost]
    [ActionName("DeleteAirport")]
    public IActionResult DeleteAirportPost(string id)
    {
        AirportsJay? airport = db.AirportsJays.Find(id);
        if (airport != null) db.AirportsJays.Remove(airport);
        db.SaveChanges();
        return RedirectToAction("ShowAirports");
    }


    ////////// AIRPORTS //////////

    ////////// BOOKINGS //////////

    [HttpGet]
    public IActionResult Bookings(int id)
    {
        return View(db.BookingsJays.Where(b => b.PassengerId == id));
    }

    [HttpGet]
    public IActionResult DetailsBooking(int id)
    {
        BookingsJay? bookings = db.BookingsJays.Include(x => x.FlightNumberNavigation).ThenInclude(f => f.AirlineCodeNavigation).Include(x => x.FlightNumberNavigation).ThenInclude(f => f.ArrivalCodeNavigation).Include(x => x.FlightNumberNavigation).ThenInclude(f => f.DepartureAirportCodeNavigation).FirstOrDefault(y => y.BookingId == id);
        return View(bookings);
    }

    ////////// BOOKINGS //////////

}