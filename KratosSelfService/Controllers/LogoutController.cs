using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        KratosLogoutFlow flow;
        try
        {
            flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }

        return Redirect(flow.LogoutUrl);
    }
}