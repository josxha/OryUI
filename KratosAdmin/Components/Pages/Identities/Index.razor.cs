using KratosAdmin.Models;
using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;
using Ory.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Index
{
    private List<ClientIdentity>? _identities;
    private bool _isLoading = true;
    private List<TraitsSchemaData>? _traitSchemes;
    [Inject] private IdentityService IdentityService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var schemeIds = await SchemaService.ListIds();
        _traitSchemes = await SchemaService.GetTraitSchemas(schemeIds.First());

        // TODO use pagination to support a large amount of identities
        _identities = await IdentityService.ListIdentities();

        _isLoading = false;
    }

    private string? GetIdentityTraitValueFromPath(ClientIdentity identity, List<string> path)
    {
        var traits = (JObject)identity.Traits;
        var jToken = traits[path.First()];
        foreach (var pathSection in path.Skip(1)) jToken = jToken?[pathSection];
        return jToken?.ToString();
    }

    private void ViewIdentity(string identityId)
    {
        Navigation.NavigateTo($"identities/{identityId}");
    }

    private void RefreshPage(MouseEventArgs arg)
    {
        Navigation.Refresh();
    }
}