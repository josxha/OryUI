using Microsoft.AspNetCore.Components;
using Ory.Keto.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Permissions.Relationships;

public partial class Index
{
    private bool _isLoading = true;
    private KetoRelationships? _relationships;
    [Parameter] public required string NamespaceName { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _relationships = await ApiService.KetoRelationshipRead.GetRelationshipsAsync(_namespace: NamespaceName);
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}