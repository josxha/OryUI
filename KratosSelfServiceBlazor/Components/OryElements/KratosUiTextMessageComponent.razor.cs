using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements;

public partial class KratosUiTextMessageComponent
{
    [Parameter] public required KratosUiText uiText { get; set; }

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