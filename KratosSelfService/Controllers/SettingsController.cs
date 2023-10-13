using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KratosSelfService.Controllers;

public class SettingsController(ApiService api) : Controller
{
    [HttpGet("settings")]
    public async Task<IActionResult> Settings([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            var newFlow = await api.Frontend
                .CreateBrowserSettingsFlowAsync(cookie: Request.Headers.Cookie);
            return Redirect($"settings?flow={newFlow.Id}");
        }

        var flow = await api.Frontend.GetSettingsFlowAsync(flowId, cookie: Request.Headers.Cookie);
        return View("Settings", flow);
    }
}