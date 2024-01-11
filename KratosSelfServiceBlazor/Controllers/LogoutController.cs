using KratosSelfServiceBlazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;

namespace KratosSelfServiceBlazor.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogoutPost([FromForm(Name = "challenge")] string logoutChallenge,
        [FromForm(Name = "submit")] string action)
    {
        if (api.HydraOAuth2 == null) return NotFound();

        if (action == "no")
        {
            logger.LogDebug("User rejected to log out.");
            // The user rejects to log out
            await api.HydraOAuth2.RejectOAuth2LogoutRequestAsync(logoutChallenge);
            return Redirect("/");
        }

        // The user agreed to log out, let's accept the logout request.
        logger.LogDebug("User agreed to log out.");

        // end hydra session
        HydraOAuth2RedirectTo hydraResponse;
        try
        {
            hydraResponse = await api.HydraOAuth2.AcceptOAuth2LogoutRequestAsync(logoutChallenge);
        }
        catch (ApiException exception)
        {
            logger.LogWarning("Could not logout: {Message}", exception.Message);
            return Redirect("~/");
        }

        // end kratos session
        try
        {
            var flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie,
                hydraResponse.RedirectTo);
            return Redirect(flow.LogoutUrl);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not get logout flow: {Message}", exception.Message);
            return Redirect(hydraResponse.RedirectTo);
        }
    }
}