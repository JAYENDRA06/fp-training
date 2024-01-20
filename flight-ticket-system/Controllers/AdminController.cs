using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace flight_ticket_system.Controllers;

public class AdminController(Ace52024Context _db) : Controller
{
    private readonly Ace52024Context db = _db;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    ////////// USERS //////////

    [HttpGet]
    public IActionResult ShowUsers()
    {
        return View(db.PassengersJays);
    }

    [HttpGet]
    public IActionResult DeleteUser(int id)
    {
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
    public IActionResult EditUser(int id)
    {
        PassengersJay? user = db.PassengersJays.Find(id);
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
        PassengersJay? user = db.PassengersJays.Find(id);
        return View(user);
    }

    ////////// USERS //////////

    ////////// FLIGHTS //////////

    [HttpGet]
    public IActionResult ShowFlights()
    {
        return View(db.FlightsJays);
    }

    [HttpGet]
    public IActionResult AddFlight()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddFlight(FlightsJay flight)
    {
        db.FlightsJays.Add(flight);
        db.SaveChanges();
        return RedirectToAction("ShowFlights");
    }

    [HttpGet]
    public IActionResult EditFlight(string id)
    {
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
        return View(db.FlightsJays.Find(id));
    }

    ////////// FLIGHTS //////////

    ////////// AIRLINES //////////

    [HttpGet]
    public IActionResult ShowAirlines()
    {
        return View(db.AirlinesJays);
    }

    [HttpGet]
    public IActionResult AddAirline()
    {
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
        return View(db.AirportsJays);
    }

    [HttpGet]
    public IActionResult AddAirport()
    {
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
}