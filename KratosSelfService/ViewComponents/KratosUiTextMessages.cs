using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiTextMessages : ViewComponent
{
    public Task<ViewViewComponentResult> InvokeAsync(List<KratosUiText>? model)
    {
        return Task.FromResult(View("Default", model));
    }
}