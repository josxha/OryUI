using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;
using OryAdmin.Extensions;
using OryAdmin.Models;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities;

public partial class Create
{
    private readonly JObject _json = new();
    private string? _errorMessage;
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
        var body = new KratosCreateIdentityBody(schemaId: _selectedSchema, traits: _json);
        try
        {
            _ = await ApiService.KratosIdentity.CreateIdentityAsync(body);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        Navigation.NavigateTo("identities");
    }

    private void UpdateValue(TraitsSchemaData schema, ChangeEventArgs args)
    {
        _json.UpdateValueWithSchema(schema, args.Value);
    }
}