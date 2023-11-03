using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace KratosSelfService.ViewComponents;

public class KratosUi : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiModel model)
    {
        return View("Default", model);
    }
}