﻿using KratosSelfServiceBlazor.Extensions;
using KratosSelfServiceBlazor.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Controllers;

public class SessionsController(ApiService api) : Controller
{
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