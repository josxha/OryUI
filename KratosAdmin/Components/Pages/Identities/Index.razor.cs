using KratosAdmin.Models;
using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ory.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Index
{
    private List<ClientIdentity>? _identities;
    private bool _isLoading = true;
    private List<TraitsSchemaData>? _traitSchemes;

    [SupplyParameterFromQuery(Name = "page")]
    private int? PageNr { get; set; }

    [Inject] private IdentityService IdentityService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        PageNr ??= 1;
        var schemeIds = await SchemaService.ListIds();
        _traitSchemes = await SchemaService.GetTraitSchemas(schemeIds.First());

        // TODO use pagination to support a large amount of identities
        _identities = await IdentityService.ListIdentities();

        _isLoading = false;
    }

    private void ViewIdentity(string identityId)
    {
        Navigation.NavigateTo($"identities/{identityId}");
    }

    private void RefreshPage(MouseEventArgs arg)
    {
        Navigation.Refresh(true);
    }
}