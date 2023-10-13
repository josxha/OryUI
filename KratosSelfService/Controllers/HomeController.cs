using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class HomeController(ILogger<EntranceController> logger) : Controller
{
    [HttpGet("")]
    public IActionResult Home()
    {
        return View("Home");
    }
}