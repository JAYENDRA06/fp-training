using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using first_mvc_application.Models;

namespace first_mvc_application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() // if we are sure we return a view we can make it ViewResult
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Sample()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
