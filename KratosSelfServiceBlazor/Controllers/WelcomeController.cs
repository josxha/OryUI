using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfServiceBlazor.Controllers;

public class WelcomeController : Controller
{

    [HttpGet("welcome")]
    [AllowAnonymous]
    public IActionResult Welcome()
    {
        // this endpoint exists for parity reason to the ory kratos self service ui
        return Redirect("~/");
    }
}