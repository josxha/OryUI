using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class WellknownController(ApiService api) : Controller
{
    [HttpGet("/.well-known/ory/webauthn.js")]
    [AllowAnonymous]
    public async Task<IActionResult> Webauthn(CancellationToken cancellationToken)
    {
        var script = await api.Frontend.GetWebAuthnJavaScriptAsync(cancellationToken);
        //Response.ContentType = "text/javascript";
        return new ObjectResult(script);
    }
}