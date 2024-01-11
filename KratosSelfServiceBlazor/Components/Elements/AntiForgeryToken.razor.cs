namespace KratosSelfServiceBlazor.Components.Elements;

public partial class AntiForgeryToken
{
    private string? _name;
    private string? _value;

    protected override void OnInitialized()
    {
        var token = antiforgeryProvider.GetAntiforgeryToken();
        _name = token?.FormFieldName;
        _value = token?.Value;
    }
}