using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class Index
{
    private List<KratosIdentity>? _identities;
    private bool _isLoading = true;

    [SupplyParameterFromQuery(Name = "page")]
    private int? PageNr { get; set; }

    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;
    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        PageNr ??= 1;

        // TODO use pagination to support a large amount of identities
        _identities = await ApiService.KratosIdentity.ListIdentitiesAsync();

        _isLoading = false;
    }
}