using KratosAdmin.Extensions;
using KratosAdmin.Models;
using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosAdmin.Components.Pages.Identities;

public partial class Edit
{
    private string? _errorMessage;
    private KratosIdentity? _identity;
    private bool _isLoading = true;
    private JObject? _json;
    private List<TraitsSchemaData>? _traitSchemas;
    [Parameter] public string? UserId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _identity = await ApiService.KratosIdentity.GetIdentityAsync(UserId);
        _json = (JObject?)_identity.Traits;
        _traitSchemas = await SchemaService.GetTraitSchemas(_identity.SchemaId);
        _isLoading = false;
    }

    private void Cancel()
    {
        Navigation.NavigateTo($"identities/{UserId}");
    }

    private async Task SubmitForm()
    {
        var updateBody = new KratosUpdateIdentityBody(traits: _json, schemaId: _identity!.SchemaId);
        try
        {
            _ = await ApiService.KratosIdentity.UpdateIdentityAsync(UserId, updateBody);
        }
        catch (ApiException exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        Navigation.NavigateTo($"identities/{UserId}");
    }

    private void UpdateValue(TraitsSchemaData schema, ChangeEventArgs args)
    {
        _json!.UpdateValueWithSchema(schema, args.Value);
    }
}