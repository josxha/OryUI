using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiTextMessageComponent(IOryElementsTranslator oryTranslator) : ViewComponent
{
    public Task<ViewViewComponentResult> InvokeAsync(KratosUiText uiText)
    {
        var content = oryTranslator.ForUiText(uiText);
        var model = new KratosUiTextMessageModel(uiText, content!, GetCssClass(uiText.Type));
        return Task.FromResult(View("Default", model));
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