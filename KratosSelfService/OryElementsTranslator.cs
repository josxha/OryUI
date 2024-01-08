using System.Globalization;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
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
        var text = Localizer[$"identities.messages.{uiText.Id}"].ToString();
        var context = (JObject?)uiText.Context;
        if (context != null)
        {
            foreach (var (key, jToken) in context)
            {
                text = text.Replace("{" + key + "}", jToken?.ToString());
            }
        }

        if (text.First() == '{' && text.Last() == '}' && !text.Contains(' '))
            text = uiText.Text;
        
        return text;
    }
}

public interface IOryElementsTranslator
{
    string Get(string text);
    string? ForUiText(KratosUiText? uiText);
}