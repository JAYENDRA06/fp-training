using first_mvc_application.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc_application.Controllers
{
    public class LoginController(Ace52024Context _db) : Controller
    {
        private readonly Ace52024Context db = _db;

        [HttpGet]
        public IActionResult LoginSuccess()
        {
            ViewBag.username = HttpContext.Session.GetString("uname");
            if(ViewBag.username != null) return View();
            else return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsertblJay _user)
        {
            UsertblJay? u = db.UsertblJays.Where(user => user.Email == _user.Email && user.Password == _user.Password).FirstOrDefault();
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
        public IActionResult Register(UsertblJay _user)
        {
            db.UsertblJays.Add(_user);
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