using KratosSelfService.Extensions;
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
        var currentSession = HttpContext.GetSession()!;
        // retrieve all other active sessions
        var otherSessions = await api.Frontend
            .ListMySessionsAsync(cookie: Request.Headers.Cookie) ?? new List<KratosSession>();
        var model = new SessionsModel(currentSession, otherSessions);
        return View("Sessions", model);
    }

    [HttpPost("sessions")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogoutAllOtherSessions([FromForm] string? action)
    {
        if (action == "invokeSessions")
        {
            _ = await api.Frontend.DisableMyOtherSessionsAsync(cookie: Request.Headers.Cookie);
        }

        return Redirect("sessions");
    }
}