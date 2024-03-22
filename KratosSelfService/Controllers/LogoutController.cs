using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;

namespace KratosSelfService.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{
    [HttpGet("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> LogoutGet([FromQuery(Name = "logout_challenge")] string? logoutChallenge)
    {
        HydraOAuth2RedirectTo? hydraResponse = null;
        if (api.HydraOAuth2 != null && !string.IsNullOrWhiteSpace(logoutChallenge))
        {
            // end hydra session
            try
            {
                hydraResponse = await api.HydraOAuth2.AcceptOAuth2LogoutRequestAsync(logoutChallenge);
            }
            catch (ApiException exception)
            {
                logger.LogWarning("Could not logout: {Message}", exception.Message);
                return Redirect("~/");
            }
        }
        
        // end kratos session
        try
        {
            var flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie, 
                hydraResponse?.RedirectTo);
            return Redirect(flow.LogoutUrl);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not get logout flow: {Message}", exception.Message);
            return Redirect("~/");
        }
    }
}