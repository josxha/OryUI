using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Hydra.Client.Model;

namespace KratosSelfService.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{
    [HttpGet("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> LogoutGet(
        [FromQuery(Name = "logout_challenge")] string? logoutChallenge, 
        CancellationToken cancellationToken)
    {
        HydraOAuth2RedirectTo? hydraResponse = null;
        if (api.HydraOAuth2 != null && !string.IsNullOrWhiteSpace(logoutChallenge))
        {
            // end hydra session
            try
            {
                hydraResponse = await api.HydraOAuth2.AcceptOAuth2LogoutRequestAsync(logoutChallenge, cancellationToken);
            }
            catch (Ory.Hydra.Client.Client.ApiException exception)
            {
                logger.LogWarning("Could not logout: {Message}", exception.Message);
                return Redirect("~/");
            }
        }
        
        // end kratos session
        try
        {
            var flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie, 
                hydraResponse?.RedirectTo, cancellationToken);
            return Redirect(flow.LogoutUrl);
        }
        catch (Ory.Kratos.Client.Client.ApiException exception)
        {
            logger.LogDebug("Could not get logout flow: {Message}", exception.Message);
            return Redirect("~/");
        }
    }
}