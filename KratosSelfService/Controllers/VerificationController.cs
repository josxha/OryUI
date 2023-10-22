using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class VerificationController(ILogger<VerificationController> logger, ApiService api) : Controller
{
    [HttpGet("verification")]
    public async Task<IActionResult> Verification(
        [FromQuery(Name = "flow")] string? flowId,
        [FromQuery(Name = "return_to")] string? returnTo,
        [FromQuery(Name = "message")] string? jsonMessages)
    {
        if (flowId == null)
        {
            // initiate flow
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            var url = api.GetUrlForBrowserFlow("verification", new Dictionary<string, string?>
            {
                ["return_to"] = returnTo
            });
            return Redirect(url);
        }

        KratosVerificationFlow flow;
        try
        {
            flow = await api.Frontend.GetVerificationFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not retrieve flow for flow ID: {Message}", exception.Message);
            var url = api.GetUrlForBrowserFlow("verification", new Dictionary<string, string?>
            {
                ["return_to"] = returnTo
            });
            return Redirect(url);
        }

        // check for custom messages in the query string
        if (string.IsNullOrWhiteSpace(jsonMessages))
        {
            var messages = JsonConvert.DeserializeObject<List<KratosUiText>>(jsonMessages);
            if (messages != null)
            {
                flow.Ui.Messages.AddRange(messages);
            }
        }

        return View("Verification", flow);
    }
}