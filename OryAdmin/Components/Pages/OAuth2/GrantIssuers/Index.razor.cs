using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.GrantIssuers;

public partial class Index : ComponentBase
{
    private bool _isLoading = true;
    private List<HydraTrustedOAuth2JwtGrantIssuer>? _trustedIssuers;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _trustedIssuers = await ApiService.HydraOAuth2.ListTrustedOAuth2JwtGrantIssuersAsync();
        _isLoading = false;
    }
}