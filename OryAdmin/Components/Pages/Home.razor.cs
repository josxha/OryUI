using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace KratosAdmin.Components.Pages;

public partial class Home
{
    private string? _kratosAdminUrl;
    private bool _kratosAlive;
    private string? _kratosPublicUrl;
    private bool _kratosReady;
    private string? _kratosVersion;
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _kratosPublicUrl = EnvService.KratosPublicUrl;
        _kratosAdminUrl = EnvService.KratosAdminUrl;
        try
        {
            var version = await ApiService.Metadata.GetVersionAsync();
            _kratosVersion = version._Version;
            var alive = await ApiService.Metadata.IsAliveAsync();
            _kratosAlive = alive.Status == "ok";
            var ready = await ApiService.Metadata.IsReadyAsync();
            _kratosReady = ready.Status == "ok";
        }
        catch (Exception exception)
        {
            _kratosVersion = exception.Message;
            _kratosAlive = false;
            _kratosReady = false;
        }
    }
}