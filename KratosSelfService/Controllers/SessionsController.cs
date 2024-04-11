using KratosSelfService.Extensions;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

[Route("sessions")]
public class SessionsController(ApiService api) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Sessions(CancellationToken cancellationToken)
    {
        var currentSession = HttpContext.GetSession()!;
        // retrieve all other active sessions
        var otherSessions = await api.Frontend
            .ListMySessionsAsync(cookie: Request.Headers.Cookie, cancellationToken: cancellationToken) ?? [];
        var model = new SessionsModel(currentSession, otherSessions);
        return View("Sessions", model);
    }

    [HttpPost("")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogoutAllOtherSessions([FromForm] string? action,
        CancellationToken cancellationToken)
    {
        if (action == "invokeSessions")
        {
            _ = await api.Frontend.DisableMyOtherSessionsAsync(cookie: Request.Headers.Cookie,
                cancellationToken: cancellationToken);
        }

        return Redirect("sessions");
    }
}