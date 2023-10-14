using KratosAdmin.Services;
using Microsoft.AspNetCore.Components;
using Ory.Keto.Client.Model;

namespace KratosAdmin.Components.Pages.Permissions;

public partial class Index
{
    private bool _isLoading = true;
    private KetoRelationshipNamespaces? _namespaces;
    private KetoRelationships? _relationships;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            //_namespaces = await ApiService.KetoRelationship.ListRelationshipNamespacesAsync();
            _relationships = await ApiService.KetoRelationship.GetRelationshipsAsync();
        }
        catch (Exception)
        {
            // ignored
        }

        _isLoading = false;
    }
}