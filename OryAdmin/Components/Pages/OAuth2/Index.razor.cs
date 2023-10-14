using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;

namespace KratosAdmin.Components.Pages.OAuth2;

public partial class Index
{
    private bool _isLoading = true;
    private HydraJsonWebKeySet? _jsonWebKeySet;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _jsonWebKeySet = await ApiService.HydraWellknown.DiscoverJsonWebKeysAsync();
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}