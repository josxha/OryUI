using Microsoft.AspNetCore.Components;
using Ory.Keto.Client.Model;
using OryAdmin.Services;

namespace OryAdmin.Components.Pages.Permissions;

public partial class Index
{
    private bool _isLoading = true;
    private KetoRelationshipNamespaces? _namespaces;
    private KetoRelationships? _relationships;

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var tasks = new List<Task>
        {
            Task.Run(async () =>
            {
                try
                {
                    _namespaces = await ApiService.KetoRelationship.ListRelationshipNamespacesAsync();
                }
                catch (Exception)
                {
                    // ignored
                }
            }),
            Task.Run(async () =>
            {
                try
                {
                    _relationships = await ApiService.KetoRelationship.GetRelationshipsAsync();
                }
                catch (Exception)
                {
                    // ignored
                }
            })
        };
        await Task.WhenAll(tasks);
        _isLoading = false;
    }
}