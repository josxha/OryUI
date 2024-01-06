using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class HomeController(IdentitySchemaService schemaService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Home()
    {
        var session = (KratosSession)HttpContext.Items[typeof(KratosSession)]!;
        var schema = await schemaService.FetchSchema(session.Identity.SchemaId,
            session.Identity.SchemaUrl);
        return View("Profile", new ProfileModel(session, IdentitySchemaService.GetTraits(schema)));
    }

    [HttpGet("welcome")]
    [AllowAnonymous]
    public IActionResult Welcome()
    {
        // this endpoint exists for parity reason to the ory kratos self service ui
        return Redirect("~/");
    }

    [HttpGet("links")]
    [AllowAnonymous]
    public IActionResult Links()
    {
        return View("Links");
    }
}