using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class RecoveryController(ILogger<RecoveryController> logger, ApiService api) : Controller
{
    [HttpGet("recovery")]
    public async Task<IActionResult> Recovery(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo)
    {
        if (flowId == null)
        {
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            return Redirect(api.GetUrlForBrowserFlow("recovery", new Dictionary<string, string?>()
            {
                ["return_to"] = returnTo
            }));
        }

        KratosRecoveryFlow flow;
        try
        {
            flow = await api.Frontend.GetRecoveryFlowAsync(flowId.ToString(), Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            return Redirect(api.GetUrlForBrowserFlow("recovery", new Dictionary<string, string?>()
            {
                ["return_to"] = returnTo
            }));
        }

        var loginUrl = api.GetUrlForBrowserFlow("login", new Dictionary<string, string?>()
        {
            ["return_to"] = returnTo
        });
        var model = new RecoveryModel(flow, loginUrl);
        return View("Recovery", model);
    }
}