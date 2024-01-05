using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.GrantIssuers;

public partial class Create
{
    private readonly List<string> _scope = [];
    private bool _allowAnySubject;
    private string? _errorMessage;
    private bool _isLoading = true;
    private string? _issuerName;
    private string? _subject;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _isLoading = false;
    }

    private async Task SubmitForm()
    {
        var jwk = new HydraJsonWebKey();
        try
        {
            _ = await ApiService.HydraOAuth2.TrustOAuth2JwtGrantIssuerAsync(new HydraTrustOAuth2JwtGrantIssuer(
                issuer: _issuerName,
                scope: _scope, jwk: jwk,
                subject: _subject,
                allowAnySubject: _allowAnySubject
            ));
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        nav.NavigateTo("oauth2/issuers");
    }
}