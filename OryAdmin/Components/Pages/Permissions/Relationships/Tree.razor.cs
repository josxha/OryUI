using Microsoft.AspNetCore.Components;
using Ory.Keto.Client.Model;
using OryAdmin.Services;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace OryAdmin.Components.Pages.Permissions.Relationships;

public partial class Tree
{
    private string? _errorMessage;
    private bool _isLoading = true;
    private KetoExpandedPermissionTree? _permissionTree;

    [Parameter] public required string NamespaceName { get; set; }

    [SupplyParameterFromQuery(Name = "relation")]
    private string? Relation { get; set; }

    [SupplyParameterFromQuery(Name = "object")]
    private string? Object { get; set; }

    [Inject] private ApiService ApiService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _permissionTree = await ApiService.KetoPermissionRead.ExpandPermissionsAsync(NamespaceName,
                relation: Relation, _object: Object);
        }
        catch (Exception exception)
        {
            _errorMessage = exception.Message;
        }

        _isLoading = false;
    }
}