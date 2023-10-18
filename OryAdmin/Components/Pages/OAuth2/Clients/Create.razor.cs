using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class Create
{
    private readonly HydraOAuth2Client _client = new();
    private string? _errorMessage;
    private bool _isLoading = true;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _isLoading = false;
    }

    private async Task SubmitForm()
    {
        try
        {
            _ = await ApiService.HydraOAuth2.CreateOAuth2ClientAsync(_client);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        Navigation.NavigateTo("oauth2/clients");
    }
}