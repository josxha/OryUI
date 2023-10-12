using KratosAdmin.Models;
using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Ory.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Edit
{
    private ClientIdentity? _identity;
    private bool _isLoading = true;
    private List<TraitsSchemaData>? _traitSchemas;
    [Parameter] public string? UserId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _identity = await ApiService.IdentityApi.GetIdentityAsync(UserId);
        _traitSchemas = await SchemaService.GetTraitSchemas(_identity.SchemaId);
        _isLoading = false;
    }

    private void Cancel()
    {
        Navigation.NavigateTo($"identities/{UserId}");
    }

    private async Task SubmitForm()
    {
        var traits = (JObject)_identity!.Traits;
        var updateBody = new ClientUpdateIdentityBody(traits: traits);
        _ = await ApiService.IdentityApi.UpdateIdentityAsync(UserId, updateBody);
        Navigation.NavigateTo($"identities/{UserId}");
    }
}