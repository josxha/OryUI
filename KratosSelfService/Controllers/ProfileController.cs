using KratosSelfService.Extensions;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class ProfileController(IdentitySchemaService schemaService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Profile(CancellationToken cancellationToken)
    {
        var session = (KratosSession)HttpContext.Items[typeof(KratosSession)]!;
        var schema = await schemaService.FetchSchema(session.Identity.SchemaId,
            session.Identity.SchemaUrl, cancellationToken:cancellationToken);
        return View("Profile", new ProfileModel(session, IdentitySchemaService.GetTraits(schema)));
    }
}