using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class HomeController(ILogger<LoginController> logger) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View("Index");
    }
}