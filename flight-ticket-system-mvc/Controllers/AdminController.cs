using System.Net.Http.Headers;
using System.Text;
using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace flight_ticket_system.Controllers;

public class AdminController() : Controller
{
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
    public async Task<IActionResult> DeleteUser(int id)
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
    [ActionName("DeleteUser")]
    public async Task<IActionResult> DeleteUserPost(int id)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.DeleteAsync($"http://localhost:5039/api/admin/Passenger/{id}");

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowUsers");
        }
        else
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
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
    public async Task<IActionResult> EditUser(int id, PassengersJay user)
    {
        using(HttpClient client = new())
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync($"http://localhost:5039/api/admin/Passenger/{id}", content);

            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowUsers");
            }
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DetailsUser(int id)
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

    ////////// USERS //////////

    ////////// FLIGHTS //////////

    [HttpGet]
    public async Task<IActionResult> ShowFlights()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        List<FlightsJay>? flights = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Flight");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flights = JsonConvert.DeserializeObject<List<FlightsJay>>(usersRes);
        }

        return View(flights);
    }

    [HttpGet]
    public async Task<IActionResult> AddFlight()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        List<AirportsJay>? airports = [];
        List<AirlinesJay>? airlines = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airport");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airports = JsonConvert.DeserializeObject<List<AirportsJay>>(usersRes);
        }
        
        res = await client.GetAsync("http://localhost:5039/api/admin/Airline");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airlines = JsonConvert.DeserializeObject<List<AirlinesJay>>(usersRes);
        }

        ViewBag.airports = new SelectList(airports?.Select(x => x.AirportCode));
        ViewBag.airlines = new SelectList(airlines?.Select(x => x.AirlineCode));

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddFlight(FlightsJay flight)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var flightJson = JsonConvert.SerializeObject(flight);
        HttpContent content = new StringContent(flightJson, Encoding.UTF8, "application/json");

        HttpResponseMessage res = await client.PostAsync("http://localhost:5039/api/admin/Flight", content);

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowFlights");
        }
        else 
        {
            return View("Error");
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> EditFlight(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        FlightsJay? flight = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Flight/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flight = JsonConvert.DeserializeObject<FlightsJay>(usersRes);
        }

        return View(flight);
    }

    [HttpPost]
    public async Task<IActionResult> EditFlight(string id, FlightsJay flight)
    {
        using(HttpClient client = new())
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(flight), Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync($"http://localhost:5039/api/admin/Flight/{id}", content);

            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowFlights");
            }
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteFlight(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        FlightsJay? flight = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Flight/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flight = JsonConvert.DeserializeObject<FlightsJay>(usersRes);
        }

        return View(flight);
    }

    [HttpPost]
    [ActionName("DeleteFlight")]
    public async Task<IActionResult> DeleteFlightPost(string id)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.DeleteAsync($"http://localhost:5039/api/admin/Flight/{id}");

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowFlights");
        }
        else
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> DetailsFlight(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        FlightsJay? flight = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Flight/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            flight = JsonConvert.DeserializeObject<FlightsJay>(usersRes);
        }

        return View(flight);
    }

    ////////// FLIGHTS //////////

    ////////// AIRLINES //////////

    [HttpGet]
    public async Task<IActionResult> ShowAirlines()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        List<AirlinesJay>? airlines = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airline");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airlines = JsonConvert.DeserializeObject<List<AirlinesJay>>(usersRes);
        }

        return View(airlines);
    }

    [HttpGet]
    public IActionResult AddAirline()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAirline(AirlinesJay airline)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var airlineJson = JsonConvert.SerializeObject(airline);
        HttpContent content = new StringContent(airlineJson, Encoding.UTF8, "application/json");

        HttpResponseMessage res = await client.PostAsync("http://localhost:5039/api/admin/Airline", content);

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowAirlines");
        }
        else 
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditAirline(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        AirlinesJay? airline = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airline/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airline = JsonConvert.DeserializeObject<AirlinesJay>(usersRes);
        }

        return View(airline);
    }

    [HttpPost]
    public async Task<IActionResult> EditAirline(string id, AirlinesJay airline)
    {
        using(HttpClient client = new())
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(airline), Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync($"http://localhost:5039/api/admin/Airline/{id}", content);

            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAirlines");
            }
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAirline(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        AirlinesJay? airline = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airline/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airline = JsonConvert.DeserializeObject<AirlinesJay>(usersRes);
        }

        return View(airline);
    }

    [HttpPost]
    [ActionName("DeleteAirline")]
    public async Task<IActionResult> DeleteAirlinePost(string id)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.DeleteAsync($"http://localhost:5039/api/admin/Airline/{id}");

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowAirlines");
        }
        else
        {
            return View("Error");
        }
    }

    ////////// AIRLINES //////////

    ////////// AIRPORTS //////////

    [HttpGet]
    public async Task<IActionResult> ShowAirports()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");

        List<AirportsJay>? airports = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airport");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airports = JsonConvert.DeserializeObject<List<AirportsJay>>(usersRes);
        }

        return View(airports);
    }

    [HttpGet]
    public IActionResult AddAirport()
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAirport(AirportsJay airport)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var airportJson = JsonConvert.SerializeObject(airport);
        HttpContent content = new StringContent(airportJson, Encoding.UTF8, "application/json");

        HttpResponseMessage res = await client.PostAsync("http://localhost:5039/api/admin/Airport", content);

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowAirports");
        }
        else 
        {
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditAirport(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        AirportsJay? airport = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airport/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airport = JsonConvert.DeserializeObject<AirportsJay>(usersRes);
        }

        return View(airport);
    }

    [HttpPost]
    public async Task<IActionResult> EditAirport(string id, AirportsJay airport)
    {
        using(HttpClient client = new())
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(airport), Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync($"http://localhost:5039/api/admin/Airport/{id}", content);

            if(res.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAirports");
            }
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAirport(string id)
    {
        if (HttpContext.Session.GetString("uname") != "admin") return RedirectToAction("Login", "Login");
        
        AirportsJay? airport = new();

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync("http://localhost:5039/api/admin/Airport/" + id);

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            airport = JsonConvert.DeserializeObject<AirportsJay>(usersRes);
        }

        return View(airport);
    }

    [HttpPost]
    [ActionName("DeleteAirport")]
    public async Task<IActionResult> DeleteAirportPost(string id)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.DeleteAsync($"http://localhost:5039/api/admin/Airport/{id}");

        if(res.IsSuccessStatusCode)
        {
            return RedirectToAction("ShowAirports");
        }
        else
        {
            return View("Error");
        }
    }


    ////////// AIRPORTS //////////

    ////////// BOOKINGS //////////

    [HttpGet]
    public async Task<IActionResult> Bookings(int id)
    {
        List<BookingsJay>? bookings = [];

        HttpClient client = new();
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5039/api/admin/Passenger/view-bookings/{id}");

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

        HttpResponseMessage res = await client.GetAsync($"http://localhost:5039/api/admin/Passenger/details-booking/{id}");

        if (res.IsSuccessStatusCode)
        {
            var usersRes = res.Content.ReadAsStringAsync().Result;

            booking = JsonConvert.DeserializeObject<BookingsJay>(usersRes);
        }

        return View(booking);
    }

    ////////// BOOKINGS //////////

}