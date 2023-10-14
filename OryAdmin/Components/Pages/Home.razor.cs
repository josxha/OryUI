using KratosAdmin.Models;
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

    private bool _oathKeeperAlive;
    private string _oathKeeperApiUrl = "?";
    private bool _oathKeeperReady;
    private string _oathKeeperVersion = "?";

    [Inject] private ApiService ApiService { get; set; } = default!;

    private bool ServiceEnabled(OryService service)
    {
        return EnvService.ServiceEnabled(service);
    }

    protected override async Task OnInitializedAsync()
    {
        // kratos
        if (EnvService.ServiceEnabled(OryService.Kratos))
        {
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
        }

        // hydra
        if (EnvService.ServiceEnabled(OryService.Hydra))
        {
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
        }

        // oathkeeper
        if (EnvService.ServiceEnabled(OryService.Keto))
        {
            _oathKeeperApiUrl = EnvService.OathKeeperApiUrl;
            try
            {
                var version = await ApiService.OathKeeperVersion.GetVersionAsync();
                _oathKeeperVersion = version._Version;
                var alive = await ApiService.OathKeeperHealth.IsInstanceAliveAsync();
                _oathKeeperAlive = alive.Status == "ok";
                var ready = await ApiService.OathKeeperHealth.IsInstanceReadyAsync();
                _oathKeeperReady = ready.Status == "ok";
            }
            catch (Exception)
            {
                // ignored
            }
        }

        // keto
        if (EnvService.ServiceEnabled(OryService.Keto))
        {
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
}