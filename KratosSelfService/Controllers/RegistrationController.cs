using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class RegistrationController(ILogger<RegistrationController> logger, ApiService api) : Controller
{
    [HttpGet("registration")]
    public async Task<IActionResult> Registration(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery(Name = "return_to")] string? returnTo,
        [FromQuery(Name = "after_verification_return_to")]
        string? afterVerificationReturnTo,
        [FromQuery(Name = "login_challenge")] string? loginChallenge,
        [FromQuery] string? organization)
    {
        if (loginChallenge != null)
            logger.LogDebug("login_challenge found in URL query: {Challenge}", loginChallenge);
        else
            logger.LogDebug("no login_challenge found in URL query.");

        if (flowId == null)
        {
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            // initiate flow
            var parameters = new Dictionary<string, string?>();
            if (returnTo != null) parameters["return_to"] = returnTo;
            if (organization != null) parameters["organization"] = organization;
            if (afterVerificationReturnTo != null)
                parameters["after_verification_return_to"] = afterVerificationReturnTo;
            if (loginChallenge != null) parameters["login_challenge"] = loginChallenge;
            return Redirect(api.GetUrlForBrowserFlow("registration", parameters));
        }

        KratosRegistrationFlow flow;
        try
        {
            flow = await api.Frontend.GetRegistrationFlowAsync(flowId.ToString(), Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            var parameters = new Dictionary<string, string?>();
            if (returnTo != null) parameters["return_to"] = returnTo;
            if (organization != null) parameters["organization"] = organization;
            if (afterVerificationReturnTo != null)
                parameters["after_verification_return_to"] = afterVerificationReturnTo;
            if (loginChallenge != null) parameters["login_challenge"] = loginChallenge;
            return Redirect(api.GetUrlForBrowserFlow("registration", parameters));
        }

        // Render the data using a view
        var loginParams = new Dictionary<string, string?>();
        if (returnTo != null) loginParams["return_to"] = returnTo;
        if (loginChallenge != null) loginParams["login_challenge"] = loginChallenge;
        var loginUrl = api.GetUrlForBrowserFlow("login", loginParams);
        var model = new RegistrationModel(flow, loginUrl);
        return View("Registration", model);
    }
}