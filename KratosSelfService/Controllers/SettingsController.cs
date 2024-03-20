using KratosSelfService.Extensions;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

[Route("settings")]
public class SettingsController(
    ILogger<SettingsController> logger,
    ApiService api,
    EnvService envService) : Controller
{
    private readonly HttpClient _httpClient = new();

    [HttpGet("")]
    public async Task<IActionResult> Settings(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo)
    {
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query initializing settings flow");
            var newFlow = await api.Frontend
                .CreateBrowserSettingsFlowAsync(cookie: Request.Headers.Cookie, returnTo: returnTo);
            return Redirect($"settings?flow={newFlow.Id}");
        }

        KratosSettingsFlow flow;
        try
        {
            flow = await api.Frontend.GetSettingsFlowAsync(flowId.ToString(), cookie: Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            // create new flow
            logger.LogDebug("Exception while retrieving settings flow: {Message}", exception.Message);
            var newFlow = await api.Frontend
                .CreateBrowserSettingsFlowAsync(cookie: Request.Headers.Cookie, returnTo: returnTo);
            return Redirect($"/settings?flow={newFlow.Id}");
        }

        var model = new SettingsModel(flow, !string.IsNullOrWhiteSpace(envService.ExportUserDataUrl));
        return View("Settings", model);
    }

    [HttpGet("delete-account")]
    public async Task<IActionResult> DeleteAccount(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo)
    {
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query");
            return Redirect($"/settings?return_to={returnTo}");
        }

        KratosSettingsFlow flow;
        try
        {
            flow = await api.Frontend.GetSettingsFlowAsync(flowId.ToString(), cookie: Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Couldn't retrieve flow for provided flowId: {Message}", exception.Message);
            return Redirect($"/settings?return_to={returnTo}");
        }

        var model = new SettingsModel(flow, !string.IsNullOrWhiteSpace(envService.ExportUserDataUrl));
        return View("DeleteAccount", model);
    }

    [HttpPost("delete-account")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmAccountDeletion(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo)
    {
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query");
            return Redirect($"/settings?return_to={returnTo}");
        }

        try
        {
            _ = await api.Frontend.GetSettingsFlowAsync(flowId.ToString(), cookie: Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Couldn't retrieve flow for provided flowId: {Message}", exception.Message);
            return Redirect($"/settings?return_to={returnTo}");
        }

        var session = HttpContext.GetSession()!;
        // TODO require the user to reauthenticate!
        await api.KratosIdentity.DeleteIdentityAsync(session.Identity.Id);
        return Redirect("/login");
    }

    [HttpGet("export-data")]
    public async Task<IActionResult> ExportData(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo)
    {
        if (string.IsNullOrWhiteSpace(envService.ExportUserDataUrl))
        {
            logger.LogDebug("Called disabled export-data endpoint, return 404.");
            return NotFound();
        }
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query");
            return Redirect($"/settings?return_to={returnTo}");
        }

        KratosSettingsFlow flow;
        try
        {
            flow = await api.Frontend.GetSettingsFlowAsync(flowId.ToString(), cookie: Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Couldn't retrieve flow for provided flowId: {Message}", exception.Message);
            return Redirect($"/settings?return_to={returnTo}");
        }

        return View("ExportData", new SettingsModel(flow, true));
    }

    [HttpPost("export-data")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmExportData(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(envService.ExportUserDataUrl))
        {
            logger.LogDebug("Called disabled export-data endpoint, return 404.");
            return NotFound();
        }
        
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query");
            return Redirect($"/settings?return_to={returnTo}");
        }

        try
        {
            _ = await api.Frontend.GetSettingsFlowAsync(flowId.ToString(), 
                cookie: Request.Headers.Cookie, cancellationToken: cancellationToken);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Couldn't retrieve flow for provided flowId: {Message}", exception.Message);
            return Redirect($"/settings?return_to={returnTo}");
        }

        var session = HttpContext.GetSession()!;
        var url = envService.ExportUserDataUrl.Replace("{Id}", session.Identity.Id);
        var stream = await _httpClient.GetStreamAsync(url, cancellationToken: cancellationToken);
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        return File(stream, "APPLICATION/octet-stream",
            $"user-export-{timestamp}.zip");
    }
}