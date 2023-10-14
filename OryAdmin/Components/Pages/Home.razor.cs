using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ConvertToConstant.Local

namespace KratosAdmin.Components.Pages;

public partial class Home
{
    private string _hydraAdminUrl = "?";
    private bool _hydraAlive;
    private bool _hydraReady;
    private string _hydraVersion = "?";
    private bool _ketoAlive;

    private string _ketoReadUrl = "?";
    private bool _ketoReady;
    private string _ketoVersion = "?";
    private string _ketoWriteUrl = "?";

    private string _kratosAdminUrl = "?";
    private bool _kratosAlive;
    private bool _kratosReady;
    private string _kratosVersion = "?";

    private string _oathKeeperAdminUrl = "?";
    private bool _oathKeeperAlive = false;
    private bool _oathKeeperReady = false;
    private string _oathKeeperVersion = "?";

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        // kratos
        _kratosAdminUrl = EnvService.KratosAdminUrl;
        try
        {
            var version = await ApiService.KratosMetadata.GetVersionAsync();
            _kratosVersion = version._Version;
            var alive = await ApiService.KratosMetadata.IsAliveAsync();
            _kratosAlive = alive.Status == "ok";
            var ready = await ApiService.KratosMetadata.IsReadyAsync();
            _kratosReady = ready.Status == "ok";
        }
        catch (Exception)
        {
            // ignored
        }

        // hydra
        _hydraAdminUrl = EnvService.HydraAdminUrl;
        try
        {
            var version = await ApiService.HydraMetadata.GetVersionAsync();
            _hydraVersion = version._Version;
            var alive = await ApiService.HydraMetadata.IsAliveAsync();
            _hydraAlive = alive.Status == "ok";
            var ready = await ApiService.HydraMetadata.IsReadyAsync();
            _hydraReady = ready.Status == "ok";
        }
        catch (Exception)
        {
            // ignored
        }

        // keto
        _ketoWriteUrl = EnvService.KetoWriteUrl;
        _ketoReadUrl = EnvService.KetoReadUrl;
        try
        {
            var version = await ApiService.KetoMetadata.GetVersionAsync();
            _ketoVersion = version._Version;
            var alive = await ApiService.KetoMetadata.IsAliveAsync();
            _ketoAlive = alive.Status == "ok";
            var ready = await ApiService.KetoMetadata.IsReadyAsync();
            _ketoReady = ready.Status == "ok";
        }
        catch (Exception)
        {
            // ignored
        }
    }
}