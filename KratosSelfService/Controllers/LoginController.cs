using KratosSelfService.Extensions;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class LoginController(ILogger<LoginController> logger, ApiService api) : Controller
{
    [HttpGet("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromQuery(Name = "flow")] Guid? flowId,
        [FromQuery] string? aal,
        [FromQuery] string? refresh,
        [FromQuery(Name = "return_to")] string? returnTo,
        [FromQuery] string? organization,
        [FromQuery(Name = "login_challenge")] string? loginChallenge, 
        CancellationToken cancellationToken)
    {
        // oauth2 login challenge
        if (!string.IsNullOrWhiteSpace(loginChallenge))
            logger.LogDebug("login_challenge found in URL query: {LoginChallenge}", loginChallenge);

        if (flowId == null)
        {
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            // initiate flow
            return Redirect(GetInitFlowUrl(aal, refresh, returnTo, organization, loginChallenge));
        }

        KratosLoginFlow flow;
        try
        {
            flow = await api.Frontend.GetLoginFlowAsync(flowId.ToString(), Request.Headers.Cookie, cancellationToken:cancellationToken);
        }
        catch (ApiException exception)
        {
            logger.LogError("Error while getting the login flow, starting new flow. {Message}", exception.Message);
            // restart flow
            return Redirect(GetInitFlowUrl(aal, refresh, returnTo, organization, loginChallenge));
        }

        if (flow.Ui.Messages?.Any(text => text.Id == 4000010) ?? false)
            // the login requires that the user verifies their email address before logging in
            // we will create a new verification flow and redirect the user to the verification page
            return await RedirectToVerificationFlow(flow, cancellationToken:cancellationToken);

        // Render the data using a view:
        var initRegistrationQuery = new Dictionary<string, string?>
        {
            ["return_to"] = flow.ReturnTo
        };
        if (flow.Oauth2LoginRequest?.Challenge != null)
            initRegistrationQuery["login_challenge"] = flow.Oauth2LoginChallenge;

        string? initRecoveryUrl = null;
        var initRegistrationUrl = api.GetUrlForBrowserFlow("registration", initRegistrationQuery);
        if (!flow.Refresh)
            initRecoveryUrl = api.GetUrlForBrowserFlow("recovery", new Dictionary<string, string?>
            {
                ["return_to"] = flow.ReturnTo
            });

        string? logoutUrl = null;
        if (flow.RequestedAal == KratosAuthenticatorAssuranceLevel.Aal2 || flow.Refresh)
            logoutUrl = await GetLogoutUrl(flow, cancellationToken:cancellationToken);

        var model = new LoginModel(flow, initRecoveryUrl, initRegistrationUrl, logoutUrl);
        return View("Login", model);
    }

    private async Task<string?> GetLogoutUrl(KratosLoginFlow flow, CancellationToken cancellationToken)
    {
        // It is probably a bit strange to have a logout URL here, however this screen
        // is also used for 2FA flows. If something goes wrong there, we probably want
        // to give the user the option to sign out!
        try
        {
            var logoutFlow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie, flow.ReturnTo, cancellationToken:cancellationToken);
            return logoutFlow.LogoutUrl;
        }
        catch (Exception exception)
        {
            logger.LogError("Unable to create logout URL. {Message}", exception.Message);
            return null;
        }
    }

    private string GetInitFlowUrl(string? aal, string? refresh, string? returnTo, string? organization,
        string? loginChallenge)
    {
        var query = new Dictionary<string, string?>
        {
            ["aal"] = aal ?? "",
            ["refresh"] = refresh ?? "",
            ["return_to"] = returnTo ?? "",
            ["organization"] = organization ?? ""
        };
        if (!string.IsNullOrWhiteSpace(loginChallenge)) query["login_challenge"] = loginChallenge;

        return api.GetUrlForBrowserFlow("login", query);
    }

    private async Task<IActionResult> RedirectToVerificationFlow(KratosLoginFlow flow, CancellationToken cancellationToken)
    {
        try
        {
            var response = await api.Frontend
                .CreateBrowserVerificationFlowWithHttpInfoAsync(flow.ReturnTo, cancellationToken:cancellationToken);
            var verificationFlow = response.Data;
            // we need the csrf cookie from the verification flow
            Response.Headers.Append(HeaderNames.SetCookie, response.Headers[HeaderNames.SetCookie].ToString());
            // encode the verification flow id in the query parameters
            var paramDict = new Dictionary<string, string?>
            {
                ["flow"] = verificationFlow.Id,
                ["message"] = flow.Ui.Messages.ToString()
            };
            var parameters = paramDict.EncodeQueryString();

            var url = $"{Request.PathBase}/verification?{parameters}";
            return Redirect(url);
        }
        catch (Exception exception)
        {
            logger.LogError("{Message}", exception.Message);
            var url = api.GetUrlForBrowserFlow("verification", new Dictionary<string, string?>
            {
                ["return_to"] = flow.ReturnTo
            });
            return Redirect(url);
        }
    }
}