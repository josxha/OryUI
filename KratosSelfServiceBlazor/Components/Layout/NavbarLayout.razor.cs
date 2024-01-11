using Microsoft.JSInterop;

namespace KratosSelfServiceBlazor.Components.Layout;

public partial class NavbarLayout
{
    private bool _expanded;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await jsRuntime.InvokeVoidAsync("import", "/lib/fontawesome-all.min.js");
            await jsRuntime.InvokeVoidAsync("import", "/js/site.js");
        }
    }
}