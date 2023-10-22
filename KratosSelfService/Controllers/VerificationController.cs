using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class VerificationController(ILogger<VerificationController> logger, ApiService api) : Controller
{
    [HttpGet("verification")]
    public async Task<IActionResult> Verification(
        [FromQuery(Name = "flow")] Guid? flowId,
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
            flow = await api.Frontend.GetVerificationFlowAsync(flowId.ToString(), Request.Headers.Cookie);
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
            try
            {
                var messages = JsonConvert.DeserializeObject<List<KratosUiText>>(jsonMessages);
                if (messages != null) flow.Ui.Messages.AddRange(messages);
            }
            catch (Exception exception)
            {
                logger.LogError("Could not parse UiText Message. Message: {Message}, {Json}", exception.Message,
                    jsonMessages);
            }

        return View("Verification", flow);
    }
}