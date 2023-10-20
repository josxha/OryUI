using Microsoft.Extensions.Localization;
using Ory.Kratos.Client.Model;

namespace KratosSelfService;

/// <summary>
///     Localized strings from ory/elements.
///     https://github.com/ory/elements/tree/main/src/locales
///     This class has to be in the root project directory.
/// </summary>
/// <param name="localizer"></param>
public class OryElementsTranslator(IStringLocalizer<OryElementsTranslator> localizer) : IOryElementsTranslator
{
    private IStringLocalizer<OryElementsTranslator> Localizer { get; } = localizer;

    public string Get(string text)
    {
        return Localizer[text];
    }

    public string? ForUiText(KratosUiText? uiText)
    {
        if (uiText == null) return null;
        if (uiText.Id == 1070002) return uiText.Text;
        return Get($"identities.messages.{uiText.Id}");
    }
}

public interface IOryElementsTranslator
{
    string Get(string text);
    string? ForUiText(KratosUiText? uiText);
}