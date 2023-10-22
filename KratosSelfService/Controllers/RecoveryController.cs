using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class RecoveryController(ILogger<RecoveryController> logger, ApiService api) : Controller
{
    [HttpGet("recovery")]
    public async Task<IActionResult> Recovery([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            return Redirect(api.GetUrlForFlow("recovery"));
        }

        KratosRecoveryFlow flow;
        try
        {
            flow = await api.Frontend.GetRecoveryFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            return Redirect("recovery");
        }

        return View("Recovery", flow);
    }
}