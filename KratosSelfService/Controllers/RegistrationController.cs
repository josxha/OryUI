using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class RegistrationController(ILogger<RegistrationController> logger, ApiService api) : Controller
{
    [HttpGet("registration")]
    public async Task<IActionResult> Registration([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            // initiate flow
            var url = api.GetUrlForFlow("registration");
            return Redirect(url);
        }

        KratosRegistrationFlow flow;
        try
        {
            flow = await api.Frontend.GetRegistrationFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            return Redirect("registration");
        }

        return View("Registration", flow);
    }
}