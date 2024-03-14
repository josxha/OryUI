using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiNodeComponent : ViewComponent
{
    public Task<ViewViewComponentResult> InvokeAsync(KratosUiNodeArgs args)
    {
        switch (args.node.Type)
        {
            case KratosUiNode.TypeEnum.Text:
                return Task.FromResult(View("Text", args));
            case KratosUiNode.TypeEnum.Input:
                switch (args.node.Attributes.GetKratosUiNodeInputAttributes().Type)
                {
                    case KratosUiNodeInputAttributes.TypeEnum.Text:
                    case KratosUiNodeInputAttributes.TypeEnum.Password:
                    case KratosUiNodeInputAttributes.TypeEnum.Number:
                    case KratosUiNodeInputAttributes.TypeEnum.Hidden:
                    case KratosUiNodeInputAttributes.TypeEnum.Email:
                    case KratosUiNodeInputAttributes.TypeEnum.Tel:
                    case KratosUiNodeInputAttributes.TypeEnum.DatetimeLocal:
                    case KratosUiNodeInputAttributes.TypeEnum.Date:
                    case KratosUiNodeInputAttributes.TypeEnum.Url:
                        return Task.FromResult(View("InputField", args));
                    case KratosUiNodeInputAttributes.TypeEnum.Checkbox:
                        return Task.FromResult(View("InputCheckbox", args));
                    case KratosUiNodeInputAttributes.TypeEnum.Submit:
                        var inputAttr = args.node.Attributes.GetKratosUiNodeInputAttributes();
                        if (args.FlowType != FlowType.Settings
                            && inputAttr.Type == KratosUiNodeInputAttributes.TypeEnum.Submit
                            && inputAttr.Name == "provider")
                            return Task.FromResult(View("InputSubmitProvider", args));
                        if (args.FlowType == FlowType.Settings && inputAttr.Name == "link" ||
                            inputAttr.Name == "unlink")
                            return Task.FromResult(View("InputSubmitLink", args));
                        return Task.FromResult(View("InputSubmit", args));
                    case KratosUiNodeInputAttributes.TypeEnum.Button:
                        return Task.FromResult(View("InputButton", args));
                    default:
                        return Task.FromResult(View("InputDefault", args));
                }
            case KratosUiNode.TypeEnum.Img:
                return Task.FromResult(View("Image", args));
            case KratosUiNode.TypeEnum.A:
                return Task.FromResult(View("Anchor", args));
            case KratosUiNode.TypeEnum.Script:
                return Task.FromResult(View("Script", args));
            default:
                return Task.FromResult(View("Default", args));
        }
    }
}