﻿using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class SessionsController(ApiService api) : Controller
{
    [HttpGet("sessions")]
    public async Task<IActionResult> Sessions()
    {
        var currentSession = await api.FrontendApi.ToSessionAsync(Request.Headers.Cookie);
        // retrieve all other active sessions
        var otherSessions = await api.FrontendApi
            .ListMySessionsAsync(cookie: Request.Headers.Cookie) ?? new List<KratosSession>();
        var model = new SessionsModel(currentSession, otherSessions);
        return View("Sessions", model);
    }
}