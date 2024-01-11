using Microsoft.Extensions.Localization;

namespace KratosSelfServiceBlazor;

/// <summary>
/// Custom localisation strings, This class have to be in the root project directory
/// </summary>
/// <param name="localizer"></param>
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