using KratosSelfServiceBlazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfServiceBlazor.Controllers;

public class WellknownController(ApiService api) : Controller
{
    [HttpGet("/.well-known/ory/webauthn.js")]
    [AllowAnonymous]
    public async Task<IActionResult> Webauthn()
    {
        var script = await api.Frontend.GetWebAuthnJavaScriptAsync();
        //Response.ContentType = "text/javascript";
        return new ObjectResult(script);
    }
}