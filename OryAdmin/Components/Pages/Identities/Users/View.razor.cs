using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

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
        var tasks = new List<Task>
        {
            Task.Run(async () => { _identity = await ApiService.KratosIdentity.GetIdentityAsync(UserId); }),
            Task.Run(async () =>
            {
                _activeSessions = await ApiService.KratosIdentity.ListIdentitySessionsAsync(UserId, active: true);
            })
        };

        await Task.WhenAll(tasks);
        _isLoading = false;
    }

    private void EditIdentity()
    {
        nav.NavigateTo($"identities/users/{UserId}/edit");
    }

    private async Task DeleteIdentity()
    {
        await ApiService.KratosIdentity.DeleteIdentityAsync(UserId);
        nav.NavigateTo("identities/users");
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
        var link = await ApiService.KratosIdentity.CreateRecoveryLinkForIdentityAsync(body);
        nav.NavigateTo(link.RecoveryLink);
    }
}