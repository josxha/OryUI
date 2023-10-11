using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Ory.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Edit
{
    private readonly FormData _formData = new();
    private ClientIdentity? _identity;
    private bool _isLoading = true;
    [Parameter] public string? UserId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _identity = await ApiService.IdentityApi.GetIdentityAsync(UserId);
        _isLoading = false;
    }

    private void Cancel()
    {
        Navigation.NavigateTo($"identities/{UserId}");
    }

    private async Task SubmitForm(EditContext arg)
    {
        var updateBody = new ClientUpdateIdentityBody();
        _ = await ApiService.IdentityApi.UpdateIdentityAsync(UserId, updateBody);
        Navigation.NavigateTo($"identities/{UserId}");
    }

    private class FormData
    {
        public string? Email { get; set; }
    }
}