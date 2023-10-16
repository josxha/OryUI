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
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private string? SchemaId { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _schemas = await ApiService.KratosIdentity.ListIdentitySchemasAsync();
        if (_schemas.Count == 0)
        {
            _isLoading = false;
            return;
        }

        if (SchemaId == null)
        {
            Navigation.NavigateTo($"identities/schemas?id={_schemas.First().Id}");
            return;
        }

        _selectedSchema = _schemas.First(schema => schema.Id == SchemaId);
        _isLoading = false;
    }
}