using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiComponent : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiArgs args)
    {
        var nodeGroups = new Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>>();
        foreach (var node in args.ui.Nodes)
        {
            if (!nodeGroups.ContainsKey(node.Group)) nodeGroups[node.Group] = [];
            nodeGroups[node.Group].Add(node);
        }

        var defaultGroup = nodeGroups[KratosUiNode.GroupEnum.Default];
        nodeGroups.Remove(KratosUiNode.GroupEnum.Default);
        
        foreach (var hiddenGroup in args.hiddenGroups ?? [])
            nodeGroups.Remove(hiddenGroup);

        return View("Default", new KratosUiModel(
            args.ui,
            args.flowType,
            nodeGroups,
            defaultGroup,
            args.forgotPasswordUrl));
    }
}