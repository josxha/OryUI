using KratosSelfService.Extensions;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class ProfileController(
    IdentitySchemaService schemaService, EnvService envService, ILogger logger) : Controller
{
    private HttpClient _httpClient = new();
    
    [HttpGet("")]
    public async Task<IActionResult> Profile()
    {
        var session = (KratosSession)HttpContext.Items[typeof(KratosSession)]!;
        var schema = await schemaService.FetchSchema(session.Identity.SchemaId,
            session.Identity.SchemaUrl);
        return View("Profile", new ProfileModel(session, IdentitySchemaService.GetTraits(schema)));
    }

    [HttpGet("export-data")]
    public async Task<IActionResult> ExportData(CancellationToken cancellationToken)
    {
        var session = HttpContext.GetSession()!;
        if (string.IsNullOrWhiteSpace(envService.ExportUserDataUrl))
        {
            logger.LogDebug("Called disabled export-data endpoint, return 404.");
            return NotFound();
        }
        var url = envService.ExportUserDataUrl.Replace("{Id}", session.Identity.Id);
        var stream = await _httpClient.GetStreamAsync(url, cancellationToken:cancellationToken);
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        return File(stream, "APPLICATION/octet-stream",
            $"user-export-{timestamp}.zip");
    }
}