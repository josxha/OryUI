using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfServiceBlazor.Controllers;

[Route("health")]
public class HealthController : Controller
{
    [HttpGet("alive")]
    [AllowAnonymous]
    public string Alive()
    {
        return "ok";
    }

    [HttpGet("ready")]
    [AllowAnonymous]
    public string Ready()
    {
        return "ok";
    }
}