using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class SessionsController(ApiService api) : Controller
{
    [HttpGet("sessions")]
    public async Task<IActionResult> Sessions()
    {
        var currentSession = await api.FrontendApi.ToSessionAsync(cookie: Request.Headers.Cookie);
        // retrieve all other active sessions
        var otherSessions = await api.FrontendApi
            .ListMySessionsAsync(cookie: Request.Headers.Cookie) ?? new List<KratosSession>();
        var model = new SessionsModel(currentSession, otherSessions);
        return View("Sessions", model);
    }

    [HttpGet("sessions-logout")]
    public async Task<IActionResult> LogoutAllOtherSessions()
    {
        _ = await api.FrontendApi.DisableMyOtherSessionsAsync(cookie: Request.Headers.Cookie);
        return Redirect("sessions");
    }
}