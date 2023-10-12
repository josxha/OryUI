using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class LogoutController(ILogger<LogoutController> logger, ApiService api) : Controller
{
    [HttpGet("logout")]
    public async Task<IActionResult> Index()
    {
        var headers = Request.GetTypedHeaders();
        var cookie = headers.Cookie.FirstOrDefault(cookie => cookie.Name == "ory_kratos_session")?.Value;
        var token = cookie?.ToString();
        var flow = await api.FrontendApi.CreateBrowserLogoutFlowAsync(token);
        return Redirect($"{flow.LogoutUrl}?token={flow.LogoutToken}");
    }
}