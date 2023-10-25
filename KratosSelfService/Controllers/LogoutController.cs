using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{
    [HttpGet("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        KratosLogoutFlow flow;
        try
        {
            flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not get logout flow: {Message}", exception.Message);
            return Redirect("~/");
        }

        return Redirect(flow.LogoutUrl);
    }
}