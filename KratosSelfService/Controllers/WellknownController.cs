﻿using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class WellknownController(ILogger<WellknownController> logger, ApiService api) : Controller
{
    [HttpGet("/.well-known/ory/webauthn.js")]
    public async Task<IActionResult> Webauthn()
    {
        var script = await api.Frontend.GetWebAuthnJavaScriptAsync();
        //Response.ContentType = "text/javascript";
        return new ObjectResult(script);
    }
}