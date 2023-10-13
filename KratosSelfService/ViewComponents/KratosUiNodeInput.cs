using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiNodeInput : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiNode text)
    {
        return View("Default", text);
    }
}