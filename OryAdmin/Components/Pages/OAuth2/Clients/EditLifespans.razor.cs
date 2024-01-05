using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class EditLifespans
{
    private HydraOAuth2Client? _client;
    private string? _errorMessage;
    private bool _isLoading = true;
    private HydraOAuth2ClientTokenLifespans _lifespans = new();

    [Parameter] public required string ClientId { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _client = await ApiService.HydraOAuth2.GetOAuth2ClientAsync(ClientId);
        _lifespans = new HydraOAuth2ClientTokenLifespans
        {
            ImplicitGrantIdTokenLifespan = _client.ImplicitGrantIdTokenLifespan,
            AuthorizationCodeGrantAccessTokenLifespan = _client.AuthorizationCodeGrantAccessTokenLifespan,
            ImplicitGrantAccessTokenLifespan = _client.ImplicitGrantAccessTokenLifespan,
            AuthorizationCodeGrantIdTokenLifespan = _client.AuthorizationCodeGrantAccessTokenLifespan,
            AuthorizationCodeGrantRefreshTokenLifespan = _client.AuthorizationCodeGrantAccessTokenLifespan,
            ClientCredentialsGrantAccessTokenLifespan = _client.ClientCredentialsGrantAccessTokenLifespan,
            JwtBearerGrantAccessTokenLifespan = _client.JwtBearerGrantAccessTokenLifespan,
            RefreshTokenGrantAccessTokenLifespan = _client.RefreshTokenGrantAccessTokenLifespan,
            RefreshTokenGrantIdTokenLifespan = _client.RefreshTokenGrantIdTokenLifespan,
            RefreshTokenGrantRefreshTokenLifespan = _client.RefreshTokenGrantRefreshTokenLifespan
        };
        _isLoading = false;
    }

    private async Task SubmitForm()
    {
        try
        {
            _client = await ApiService.HydraOAuth2.SetOAuth2ClientLifespansAsync(ClientId, _lifespans);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        nav.NavigateTo($"oauth2/clients/{ClientId}");
    }
}