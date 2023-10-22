using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class SettingsController(ILogger<SettingsController> logger, ApiService api) : Controller
{
    [HttpGet("settings")]
    public async Task<IActionResult> Settings(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo
    )
    {
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query initializing login flow");
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
                .CreateBrowserSettingsFlowAsync(cookie: Request.Headers.Cookie, returnTo:returnTo);
            return Redirect($"settings?flow={newFlow.Id}");
        }

        var model = new SettingsModel(flow, api.GetSettingsUrl(flow.Id));
        return View("Settings", model);
    }
}