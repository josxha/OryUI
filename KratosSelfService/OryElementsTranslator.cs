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
        return uiText.Id switch
        {
            // title
            1070002 => uiText.Text,
            // totp secrets
            1050006 or 1050009 => uiText.Text,
            // backup codes, secrets list
            1050015 => uiText.Text,
            // reason
            4000001 or 5000001 => uiText.Text,
            _ => Get($"identities.messages.{uiText.Id}")
        };
    }
}

public interface IOryElementsTranslator
{
    string Get(string text);
    string? ForUiText(KratosUiText? uiText);
}