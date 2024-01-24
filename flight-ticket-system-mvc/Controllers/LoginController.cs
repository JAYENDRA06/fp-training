using System.Net.Http.Headers;
using System.Text;
using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace first_mvc_application.Controllers
{
    public class LoginController() : Controller
    {

        [HttpGet]
        public IActionResult LoginSuccess()
        {
            string? temp = HttpContext.Session.GetString("uname");
            if (temp == "admin") return RedirectToAction("Index", "Admin");
            else if (temp != null) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(PassengersJay _user)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(JsonConvert.SerializeObject(_user), Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync("http://localhost:5049/api/Login/login", content);

            if (!res.IsSuccessStatusCode)
            {
                var usersRes = res.Content.ReadAsStringAsync().Result;
                PassengersJay? userRet = JsonConvert.DeserializeObject<PassengersJay>(usersRes);

                System.Console.WriteLine("user", userRet.Email);

                HttpContext.Session.SetString("uname", userRet.Name);
                HttpContext.Session.SetInt32("uid", userRet.PassengerId);
                return RedirectToAction("LoginSuccess", "Login");       
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(PassengersJay _user)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(JsonConvert.SerializeObject(_user), Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync("http://localhost:5049/api/Login/register", content);

            if (!res.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");       
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}