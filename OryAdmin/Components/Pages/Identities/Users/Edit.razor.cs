using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Identities.Users;

public partial class Edit
{
    private string? _errorMessage;
    private KratosIdentity? _identity;
    private bool _isLoading = true;
    private JObject? _json;
    private Dictionary<List<string>, JSchema>? _traitSchemas;
    [Parameter] public string? UserId { get; set; }
    [Inject] private ApiService ApiService { get; set; } = default!;
    [Inject] private IdentitySchemaService SchemaService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _identity = await ApiService.KratosIdentity.GetIdentityAsync(UserId);
        _json = (JObject?)_identity.Traits;
        _traitSchemas = await SchemaService.GetTraits(_identity.SchemaId);
        _isLoading = false;
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

        nav.NavigateTo($"identities/users/{UserId}");
    }
}