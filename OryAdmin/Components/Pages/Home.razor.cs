using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ConvertToConstant.Local

namespace KratosAdmin.Components.Pages;

public partial class Home
{
    private string _hydraAdminUrl = "?";
    private bool _hydraAlive = false;
    private string _hydraPublicUrl = "?";
    private bool _hydraReady = false;
    private string _hydraVersion = "?";

    private string _ketoAdminUrl = "?";
    private bool _ketoAlive = false;
    private string _ketoPublicUrl = "?";
    private bool _ketoReady = false;
    private string _ketoVersion = "?";

    private string _kratosAdminUrl = "?";
    private bool _kratosAlive;
    private string _kratosPublicUrl = "?";
    private bool _kratosReady;
    private string _kratosVersion = "?";

    private string _oathKeeperAdminUrl = "?";
    private bool _oathKeeperAlive = false;
    private string _oathKeeperPublicUrl = "?";
    private bool _oathKeeperReady = false;
    private string _oathKeeperVersion = "?";

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