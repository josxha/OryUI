using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Home()
    {
        return View("Home");
    }
}