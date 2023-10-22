using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    [HttpGet("")]
    public IActionResult Home()
    {
        return View("Home");
    }
}