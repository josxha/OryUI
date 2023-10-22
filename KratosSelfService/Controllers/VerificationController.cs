using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class VerificationController(ILogger<VerificationController> logger, ApiService api) : Controller
{
    [HttpGet("verification")]
    public async Task<IActionResult> Verification([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            // initiate flow
            var url = api.GetUrlForBrowserFlow("verification");
            return Redirect(url);
        }

        KratosVerificationFlow flow;
        try
        {
            flow = await api.Frontend.GetVerificationFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }

        return View("Verification", flow);
    }
}