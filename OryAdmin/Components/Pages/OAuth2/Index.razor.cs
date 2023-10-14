using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2;

public partial class Index
{
    private bool _isLoading = true;
    private HydraJsonWebKeySet? _jsonWebKeySet;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var tasks = new List<Task>
        {
            Task.Run(async () =>
            {
                try
                {
                    _jsonWebKeySet = await ApiService.HydraWellknown.DiscoverJsonWebKeysAsync();
                }
                catch (Exception)
                {
                    // ignored
                }
            })
        };

        await Task.WhenAll(tasks);
        _isLoading = false;
    }
}