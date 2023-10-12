using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class View
{
    private List<KratosSession>? _activeSessions;
    private KratosIdentity? _identity;
    private bool _isLoading = true;
    private bool _showDeleteModal;
    [Parameter] public string? UserId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _identity = await ApiService.IdentityApi.GetIdentityAsync(UserId);
        _activeSessions = await ApiService.IdentityApi.ListIdentitySessionsAsync(UserId, active: true);
        _isLoading = false;
    }

    private void EditIdentity()
    {
        Navigation.NavigateTo($"identities/{UserId}/edit");
    }

    private async Task DeleteIdentity()
    {
        await ApiService.IdentityApi.DeleteIdentityAsync(UserId);
        Navigation.NavigateTo("identities");
    }

    private void ShowDeleteModal()
    {
        _showDeleteModal = true;
    }

    private void HideDeleteModal()
    {
        _showDeleteModal = false;
    }

    private async Task UpdatePassword()
    {
        var body = new KratosCreateRecoveryLinkForIdentityBody(identityId: UserId);
        var link = await ApiService.IdentityApi.CreateRecoveryLinkForIdentityAsync(body);
        Navigation.NavigateTo(link.RecoveryLink);
    }
}