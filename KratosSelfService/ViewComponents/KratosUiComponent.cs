using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiComponent : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiArgs model)
    {
        var nodeGroups = new Dictionary<KratosUiNode.GroupEnum, List<KratosUiNode>>();
        foreach (var node in model.ui.Nodes)
        {
            if (!nodeGroups.ContainsKey(node.Group)) nodeGroups[node.Group] = [];
            nodeGroups[node.Group].Add(node);
        }

        var defaultGroup = nodeGroups[KratosUiNode.GroupEnum.Default];
        nodeGroups.Remove(KratosUiNode.GroupEnum.Default);
        
        return View("Default", new KratosUiModel(
            model.ui, 
            model.flowType, 
            nodeGroups, 
            defaultGroup, 
            model.forgotPasswordUrl));
    }
}