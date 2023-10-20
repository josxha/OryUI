using Microsoft.Extensions.Localization;

namespace KratosSelfService;

/// <summary>
/// Localized strings from ory/elements.
/// https://github.com/ory/elements/tree/main/src/locales
/// This class has to be in the root project directory.
/// </summary>
/// <param name="localizer"></param>
public class OryElementsTranslator(IStringLocalizer<OryElementsTranslator> localizer) : IOryElementsTranslator
{
    private IStringLocalizer<OryElementsTranslator> Localizer { get; } = localizer;

    public string Get(string text)
    {
        return Localizer[text];
    }
}

public interface IOryElementsTranslator
{
    string Get(string text);
}