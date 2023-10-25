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

    [HttpGet("welcome")]
    [AllowAnonymous]
    public IActionResult Welcome()
    {
        // this endpoint exists for parity reason to the ory kratos self service ui
        return Redirect("~/");
    }
}