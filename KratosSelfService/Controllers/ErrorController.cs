using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class ErrorController(ApiService api) : Controller
{
    [HttpGet("error")]
    public async Task<IActionResult> Error([FromQuery(Name = "id")] Guid? flowId)
    {
        var error = await api.Frontend.GetFlowErrorAsync(flowId.ToString());
        return View("Error", error);
    }
}