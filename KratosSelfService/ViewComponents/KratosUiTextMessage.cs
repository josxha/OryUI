using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiTextMessage : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiText text)
    {
        var model = new KratosUiTextMessageModel(text, GetCssClass(text.Type));
        return View("Default", model);
    }

    private static string GetCssClass(KratosUiText.TypeEnum type)
    {
        return type switch
        {
            KratosUiText.TypeEnum.Error => "is-danger",
            KratosUiText.TypeEnum.Success => "is-success",
            KratosUiText.TypeEnum.Info => "is-info",
            _ => "is-info"
        };
    }
}