using KratosAdmin.Models;
using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Create
{
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
        var properties = new List<JProperty>();
        foreach (var schema in _traitsSchemas!)
        {
            var path = string.Join(".", schema.Path);
            var property = new JProperty(path, "test");
            properties.Add(property);
        }

        var traits = new JObject(properties);

        Console.WriteLine(traits);
        var body = new KratosCreateIdentityBody(schemaId: _selectedSchema, traits: traits);
        try
        {
            _ = await ApiService.Identity.CreateIdentityAsync(body);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        Navigation.NavigateTo("identities");
    }
}