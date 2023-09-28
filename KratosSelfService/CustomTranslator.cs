using Microsoft.Extensions.Localization;

namespace KratosSelfService;

public class CustomTranslator(IStringLocalizer<CustomTranslator> localizer) : ICustomTranslator
{
    public string GetTranslation(string text)
    {
        return Localizer[text];
    }

    private IStringLocalizer<CustomTranslator> Localizer { get;  } = localizer;
}

public interface ICustomTranslator
{
    string GetTranslation(string text);
}