using Microsoft.AspNetCore.Components;
using OryAdmin.Services;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ConvertToConstant.Local

namespace OryAdmin.Components.Pages;

public partial class Home
{
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
        var tasks = new List<Task>();

        // kratos
        if (env.EnabledKratos)
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
                        _kratosVersion = version.VarVersion;
                    }
                    catch (Exception)
                    {
                        _kratosVersion = "?";
                    }
                })
            });

        // hydra
        if (env.EnabledHydra)
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
                        _hydraVersion = version.VarVersion;
                    }
                    catch (Exception)
                    {
                        _hydraVersion = "?";
                    }
                })
            });

        // oathkeeper
        if (env.EnabledOathkeeper)
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
                        _oathKeeperVersion = version.VarVersion;
                    }
                    catch (Exception)
                    {
                        _oathKeeperVersion = "?";
                    }
                })
            });

        // keto
        if (env.EnabledKeto)
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