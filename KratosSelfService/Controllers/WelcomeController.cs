using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class WelcomeController(IdentitySchemaService schemaService) : Controller
{
    [HttpGet("welcome")]
    [AllowAnonymous]
    public IActionResult Welcome()
    {
        // this endpoint exists for parity reason to the ory kratos self service ui
        return Redirect("~/");
    }
}