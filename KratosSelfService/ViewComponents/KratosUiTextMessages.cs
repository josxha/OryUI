using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiTextMessages : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(List<KratosUiText>? model)
    {
        return View("Default", model);
    }
}