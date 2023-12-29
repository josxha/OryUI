using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;

namespace KratosSelfService.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{
    [HttpGet("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> LogoutGet([FromQuery(Name = "logout_challenge")] string? logoutChallenge)
    {
        // show a dialog to let the user confirm that he wants to log out
        if (!string.IsNullOrWhiteSpace(logoutChallenge)) return View("Logout", new LogoutModel(logoutChallenge));

        try
        {
            var flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie);
            return Redirect(flow.LogoutUrl);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not get logout flow: {Message}", exception.Message);
            return Redirect("~/");
        }
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> LogoutPost([FromForm(Name = "challenge")] string logoutChallenge,
        [FromForm(Name = "submit")] string action)
    {
        if (api.HydraOAuth2 == null) return NotFound();

        if (action == "no")
        {
            logger.LogDebug("User rejected to log out.");
            // The user rejected to log out
            await api.HydraOAuth2.RejectOAuth2LogoutRequestAsync(logoutChallenge);
            return Redirect("/");
        }

        logger.LogDebug("User agreed to log out.");
        // The user agreed to log out, let's accept the logout request.
        try
        {
            var response = await api.HydraOAuth2.AcceptOAuth2LogoutRequestAsync(logoutChallenge);
            return Redirect(response.RedirectTo);
        }
        catch (ApiException exception)
        {
            logger.LogWarning("Could not logout: {Message}", exception.Message);
            return Redirect("~/");
        }
    }
}