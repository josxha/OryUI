using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

[Route("health")]
public class HealthController
{
    [HttpGet("alive")]
    public string Alive()
    {
        return "ok";
    }

    [HttpGet("ready")]
    public string Ready()
    {
        return "ok";
    }
}