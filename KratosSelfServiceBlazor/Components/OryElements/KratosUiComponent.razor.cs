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
    private bool _mergedFormFields;
    private KratosUiNode.GroupEnum? _selectedMethod;

    private bool _isLoading = true;
    
    protected override void OnInitialized()
    {
        _nodeGroups = new Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>>();
        _defaultGroup = [];

        var visibleNodes = ui.Nodes.Where(node => !(hiddenGroups?.Contains(node.Group) ?? false));
        foreach (var node in visibleNodes)
        {
            if (node.Group == KratosUiNode.GroupEnum.Default)
            {
                _defaultGroup.Add(node);
                continue;
            }

            if (!_nodeGroups.TryGetValue(node.Group, out var list)) 
                _nodeGroups[node.Group] = [node];
            else
                list.Add(node);
        }

        if (_nodeGroups.Count == 1)
        {
            _mergedFormFields = false;
            return;
        }

        // merge form fields as much as possible
        var otherGroups = _nodeGroups.Values.Skip(1).ToList();
        for (var i = 0; i < _nodeGroups.First().Value.Count; i++)
        {
            var compareNode = _nodeGroups.First().Value[i];
            if (otherGroups.Any(nodes => 
                    nodes.Count < i || 
                    !nodes[i].Equals(compareNode))) break;

            _defaultGroup.Add(compareNode);
            foreach (var group in _nodeGroups.Keys)
                _nodeGroups[group].RemoveAt(i);
        }

        _mergedFormFields = true;
        _isLoading = false;
    }

    private void SelectGroup(KratosUiNode.GroupEnum group)
    {
        _selectedMethod = group;
        Console.WriteLine("SelectGroup");
        StateHasChanged();
    }
}