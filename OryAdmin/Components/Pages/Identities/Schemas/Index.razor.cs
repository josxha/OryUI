using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Schemas;

public partial class Index
{
    private bool _isLoading = true;

    private List<KratosIdentitySchemaContainer> _schemas;
    private string? _selectedSchema;
    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _schemas = await ApiService.KratosIdentity.ListIdentitySchemasAsync();
        _isLoading = false;
    }
}