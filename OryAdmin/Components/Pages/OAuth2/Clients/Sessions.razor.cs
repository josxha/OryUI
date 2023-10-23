using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class Sessions : ComponentBase
{
    private bool _isLoading = true;
    private List<HydraOAuth2ConsentSession>? _sessions;
    [Parameter] public string? ClientId { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _sessions = await ApiService.HydraOAuth2.ListOAuth2ConsentSessionsAsync(ClientId);
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}