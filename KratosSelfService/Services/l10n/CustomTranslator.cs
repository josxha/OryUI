using Microsoft.Extensions.Localization;

namespace KratosSelfService.Services.l10n;

public class CustomTranslator(IStringLocalizer<CustomTranslator> localizer) : ICustomTranslator
{
    private IStringLocalizer<CustomTranslator> Localizer { get; } = localizer;

    public string Get(string text)
    {
        return Localizer[text];
    }
}

public interface ICustomTranslator
{
    string Get(string text);
}