using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiComponent : ViewComponent
{
    public Task<ViewViewComponentResult> InvokeAsync(KratosUiArgs args)
    {
        var nodeGroups = new Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>>();
        var defaultGroup = new List<KratosUiNode>();

        var visibleNodes = args.ui.Nodes.Where(node => !(args.hiddenGroups?.Contains(node.Group) ?? false));
        foreach (var node in visibleNodes)
        {
            if (node.Group == KratosUiNode.GroupEnum.Default)
            {
                defaultGroup.Add(node);
                continue;
            }

            if (!nodeGroups.TryGetValue(node.Group, out var list)) 
                nodeGroups[node.Group] = [node];
            else
                list.Add(node);
        }
        
        if (nodeGroups.Count == 1)
            return Task.FromResult(View("Default", new KratosUiModel(
                args.ui,
                args.flowType,
                nodeGroups,
                defaultGroup,
                args.forgotPasswordUrl)));

        // merge form fields as much as possible
        var otherGroups = nodeGroups.Values.Skip(1).ToList();
        for (var i = 0; i < nodeGroups.First().Value.Count; i++)
        {
            var compareNode = nodeGroups.First().Value[i];
            if (otherGroups.Any(nodes => 
                    nodes.Count < i || 
                    !nodes[i].Equals(compareNode))) break;

            defaultGroup.Add(compareNode);
            foreach (var group in nodeGroups.Keys)
                nodeGroups[group].RemoveAt(i);
        }

        return Task.FromResult(View("MergedForms", new KratosUiModel(
            args.ui,
            args.flowType,
            nodeGroups,
            defaultGroup,
            args.forgotPasswordUrl)));
    }
}