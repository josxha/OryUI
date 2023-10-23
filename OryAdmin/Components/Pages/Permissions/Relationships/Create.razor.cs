using Microsoft.AspNetCore.Components;
using Ory.Keto.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Permissions.Relationships;

public partial class Create
{
    private string? _errorMessage;
    private string? _object;
    private string? _relation;
    private string? _subjectId;

    [Inject] private ApiService ApiService { get; set; } = default!;
    [Parameter] public required string NamespaceName { get; set; }

    private async Task SubmitForm()
    {
        var body = new KetoCreateRelationshipBody(subjectId: _subjectId, relation: _relation, _object: _object,
            _namespace: NamespaceName);
        try
        {
            _ = await ApiService.KetoRelationshipWrite.CreateRelationshipAsync(body);
        }
        catch (Exception exception)
        {
            _errorMessage = exception.Message;
            return;
        }

        nav.NavigateTo($"permissions/{NamespaceName}");
    }
}