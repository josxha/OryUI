using KratosSelfService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class ErrorController(ILogger<ErrorController> logger, ApiService api) : Controller
{
    [HttpGet("error")]
    [AllowAnonymous]
    public async Task<IActionResult> Error([FromQuery(Name = "id")] Guid? flowId)
    {
        var error = await api.Frontend.GetFlowErrorAsync(flowId.ToString());
        logger.LogError("{Error}", error.ToString());
        return View("Error", error);
    }
}