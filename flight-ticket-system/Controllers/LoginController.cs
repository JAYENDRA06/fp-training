using flight_ticket_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc_application.Controllers
{
    public class LoginController(Ace52024Context _db, IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly Ace52024Context db = _db;
        private readonly ISession session = httpContextAccessor.HttpContext.Session;
        
        [HttpGet]
        public IActionResult LoginSuccess()
        {
            string? temp = HttpContext.Session.GetString("uname");
            if(temp == "admin") return RedirectToAction("Index", "Admin");
            else if(temp != null) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(PassengersJay _user)
        {
            PassengersJay? u = db.PassengersJays.Where(user => user.Email == _user.Email && user.Password == _user.Password).SingleOrDefault();
            if (u == null) return View();
            else
            {
                if (u.Name != null) HttpContext.Session.SetString("uname", u.Name);
                return RedirectToAction("LoginSuccess", "Login");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(PassengersJay _user)
        {
            db.PassengersJays.Add(_user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}