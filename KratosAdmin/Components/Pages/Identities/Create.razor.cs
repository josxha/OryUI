using KratosAdmin.Models;
using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Create
{
    private bool _isLoading = true;

    private List<string>? _schemaIds;
    private string? _selectedSchema;
    private List<TraitsSchemaData>? _traitsSchemas;
    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _schemaIds = await SchemaService.ListIds();
        await OnSchemaSelect(_schemaIds.First());
        _isLoading = false;
    }

    private async Task OnSchemaSelect(string schemaId)
    {
        _selectedSchema = schemaId;
        _traitsSchemas = await SchemaService.GetTraitSchemas(_selectedSchema);
    }

    private async Task SubmitForm()
    {
    }
}