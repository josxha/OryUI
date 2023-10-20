using Microsoft.Extensions.Localization;

namespace KratosSelfService.Services.l10n;

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