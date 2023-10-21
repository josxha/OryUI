using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class View
{
    private HydraOAuth2Client? _client;
    private bool _confirmDeleteClientModal;
    private bool _confirmNewSecretModal;
    private bool _isLoading = true;
    private bool _showNewSecretModal;
    [Parameter] public string? ClientId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _client = await ApiService.HydraOAuth2.GetOAuth2ClientAsync(ClientId);
        _isLoading = false;
    }

    private void EditClient()
    {
        nav.NavigateTo($"oauth2/clients/{ClientId}/edit");
    }

    private async Task DeleteClient()
    {
        await ApiService.HydraOAuth2.DeleteOAuth2ClientAsync(ClientId);
        nav.NavigateTo("oauth2/clients");
    }

    private void ShowDeleteModal()
    {
        _confirmDeleteClientModal = true;
    }

    private void HideDeleteModal()
    {
        _confirmDeleteClientModal = false;
    }

    private async Task CreateNewClientSecret()
    {
        _confirmNewSecretModal = false;
        // TODO generate new client secret
        _showNewSecretModal = true;
    }
}