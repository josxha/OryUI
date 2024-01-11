using KratosSelfServiceBlazor.models;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements;

public partial class KratosUiComponent
{
    [Parameter] public required KratosUiContainer ui { get; set; }
    [Parameter] public required FlowType flowType { get; set; }
    [Parameter] public required List<KratosUiNode.GroupEnum>? hiddenGroups { get; set; }
    [Parameter] public required string? forgotPasswordUrl { get; set; }

    private Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>> _nodeGroups = default!;
    private List<KratosUiNode> _defaultGroup = default!;

    private bool _isLoading = true;
    
    protected override void OnInitialized()
    {
        _nodeGroups = new Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>>();
        foreach (var node in ui.Nodes)
        {
            if (!_nodeGroups.ContainsKey(node.Group)) _nodeGroups[node.Group] = [];
            _nodeGroups[node.Group].Add(node);
        }

        _defaultGroup = _nodeGroups[KratosUiNode.GroupEnum.Default];
        _nodeGroups.Remove(KratosUiNode.GroupEnum.Default);

        foreach (var hiddenGroup in hiddenGroups ?? [])
            _nodeGroups.Remove(hiddenGroup);

        _isLoading = false;
    }
}