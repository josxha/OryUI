using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class Create
{
    private string? _errorMessage;
    private bool _isLoading = true;
    private readonly JObject _json = new();
    private bool _schemaDropdownActive;

    private List<string>? _schemaIds;
    private Dictionary<List<string>, JSchema>? _traitsSchemas;

    [SupplyParameterFromQuery(Name = "schema")]
    private string? SelectedSchemaId { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _schemaIds ??= await SchemaService.ListIds();
        SelectedSchemaId ??= _schemaIds.First();
        _isLoading = false;
    }

    protected override async Task OnParametersSetAsync()
    {
        _schemaIds ??= await SchemaService.ListIds();
        SelectedSchemaId ??= _schemaIds.First();
        _traitsSchemas = await SchemaService.GetTraits(SelectedSchemaId);
    }

    private void OnSelectionChanged()
    {
        _schemaDropdownActive = false;
    }

    private async Task SubmitForm()
    {
        var body = new KratosCreateIdentityBody(schemaId: SelectedSchemaId, traits: _json);
        try
        {
            _ = await ApiService.KratosIdentity.CreateIdentityAsync(body);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        nav.NavigateTo("identities/users");
    }
}