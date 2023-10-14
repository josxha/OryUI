using Microsoft.Extensions.Localization;

namespace OryAdmin;

public class CustomTranslator(IStringLocalizer<CustomTranslator> localizer) : ICustomTranslator
{
    private IStringLocalizer<CustomTranslator> Localizer { get; } = localizer;

    public string GetTranslation(string text)
    {
        return Localizer[text];
    }
}

public interface ICustomTranslator
{
    string GetTranslation(string text);
}