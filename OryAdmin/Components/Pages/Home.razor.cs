using Microsoft.AspNetCore.Components;
using OryAdmin.Models;
using OryAdmin.Services;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ConvertToConstant.Local

namespace OryAdmin.Components.Pages;

public partial class Home
{
    private bool _enabledHydra;
    private bool _enabledKeto;
    private bool _enabledKratos;
    private bool _enabledOathKeeper;

    private bool? _hydraAlive;
    private bool? _hydraReady;
    private string? _hydraVersion;

    private bool? _ketoAlive;
    private bool? _ketoReady;
    private string? _ketoVersion;
    private bool? _kratosAlive;
    private bool? _kratosReady;
    private string? _kratosVersion;

    private bool? _oathKeeperAlive;
    private bool? _oathKeeperReady;
    private string? _oathKeeperVersion;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _enabledKratos = EnvService.ServiceEnabled(OryService.Kratos);
        _enabledHydra = EnvService.ServiceEnabled(OryService.Hydra);
        _enabledOathKeeper = EnvService.ServiceEnabled(OryService.OathKeeper);
        _enabledKeto = EnvService.ServiceEnabled(OryService.Keto);

        var tasks = new List<Task>();

        // kratos
        if (EnvService.ServiceEnabled(OryService.Kratos))
            tasks.AddRange(new[]
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var alive = await ApiService.KratosMetadata.IsAliveAsync();
                        _kratosAlive = alive.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _kratosAlive = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var ready = await ApiService.KratosMetadata.IsReadyAsync();
                        _kratosReady = ready.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _kratosReady = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var version = await ApiService.KratosMetadata.GetVersionAsync();
                        _kratosVersion = version._Version;
                    }
                    catch (Exception)
                    {
                        _kratosVersion = "?";
                    }
                })
            });

        // hydra
        if (EnvService.ServiceEnabled(OryService.Hydra))
            tasks.AddRange(new[]
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var alive = await ApiService.HydraMetadata.IsAliveAsync();
                        _hydraAlive = alive.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _hydraAlive = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var ready = await ApiService.HydraMetadata.IsReadyAsync();
                        _hydraReady = ready.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _hydraReady = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var version = await ApiService.HydraMetadata.GetVersionAsync();
                        _hydraVersion = version._Version;
                    }
                    catch (Exception)
                    {
                        _hydraVersion = "?";
                    }
                })
            });

        // oathkeeper
        if (EnvService.ServiceEnabled(OryService.Keto))
            tasks.AddRange(new[]
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var alive = await ApiService.OathKeeperHealth.IsInstanceAliveAsync();
                        _oathKeeperAlive = alive.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _oathKeeperAlive = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var ready = await ApiService.OathKeeperHealth.IsInstanceReadyAsync();
                        _oathKeeperReady = ready.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _oathKeeperReady = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var version = await ApiService.OathKeeperVersion.GetVersionAsync();
                        _oathKeeperVersion = version._Version;
                    }
                    catch (Exception)
                    {
                        _oathKeeperVersion = "?";
                    }
                })
            });

        // keto
        if (EnvService.ServiceEnabled(OryService.Keto))
            tasks.AddRange(new[]
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var alive = await ApiService.KetoMetadata.IsAliveAsync();
                        _ketoAlive = alive.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _ketoAlive = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var ready = await ApiService.KetoMetadata.IsReadyAsync();
                        _ketoReady = ready.Status == "ok";
                    }
                    catch (Exception)
                    {
                        _ketoReady = false;
                    }
                }),
                Task.Run(async () =>
                {
                    try
                    {
                        var version = await ApiService.KetoMetadata.GetVersionAsync();
                        _ketoVersion = version._Version;
                    }
                    catch (Exception)
                    {
                        _ketoVersion = "?";
                    }
                })
            });
        await Task.WhenAll(tasks);
    }
}