using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.TrustedIssuers;

public partial class Index
{
    private bool _isLoading = true;
    private List<HydraTrustedOAuth2JwtGrantIssuer>? _trustedIssuers;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _trustedIssuers =
                await ApiService.HydraOAuth2.ListTrustedOAuth2JwtGrantIssuersAsync();
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}