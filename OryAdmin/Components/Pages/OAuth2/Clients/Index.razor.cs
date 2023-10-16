using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class Index
{
    private List<HydraOAuth2Client>? _clients;
    private bool _isLoading = true;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _clients = await ApiService.HydraOAuth2.ListOAuth2ClientsAsync();
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}