using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using Ory.Kratos.Client.Client;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class Create
{
    private readonly HydraOAuth2Client _client = new();
    private HydraOAuth2Client? _createdClient;
    private string? _errorMessage;

    [Inject] private ApiService ApiService { get; set; } = default!;

    private async Task SubmitForm()
    {
        try
        {
            _createdClient = await ApiService.HydraOAuth2.CreateOAuth2ClientAsync(_client);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
        }
    }
}