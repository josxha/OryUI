using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Schemas;

public partial class Index
{
    private bool _isLoading = true;

    private List<KratosIdentitySchemaContainer>? _schemas;

    private KratosIdentitySchemaContainer? _selectedSchema;

    [SupplyParameterFromQuery(Name = "id")]
    private string? SchemaId { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _schemas ??= await ApiService.KratosIdentity.ListIdentitySchemasAsync();
        if (_schemas.Count == 0)
        {
            _isLoading = false;
            return;
        }

        SchemaId ??= _schemas.First().Id;
        _isLoading = false;
    }

    protected override void OnParametersSet()
    {
        _selectedSchema = _schemas?.FirstOrDefault(schema => schema.Id == SchemaId);
    }
}