using Microsoft.AspNetCore.Components;
using Ory.Hydra.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.OAuth2.Clients;

public partial class View
{
    private HydraOAuth2Client? _client;
    private bool _isLoading = true;
    private bool _showDeleteModal;
    [Parameter] public string? ClientId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _client = await ApiService.HydraOAuth2.GetOAuth2ClientAsync(ClientId);
        _isLoading = false;
    }

    private void EditClient()
    {
        Navigation.NavigateTo($"oauth2/clients/{ClientId}/edit");
    }

    private async Task DeleteClient()
    {
        await ApiService.HydraOAuth2.DeleteOAuth2ClientAsync(ClientId);
        Navigation.NavigateTo("oauth2/clients");
    }

    private void ShowDeleteModal()
    {
        _showDeleteModal = true;
    }

    private void HideDeleteModal()
    {
        _showDeleteModal = false;
    }
}