using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiNodeComponent : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiNodeArgs args)
    {
        switch (args.node.Type)
        {
            case KratosUiNode.TypeEnum.Text:
                return View("Text", args);
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
                        return View("InputField", args);
                    case KratosUiNodeInputAttributes.TypeEnum.Checkbox:
                        return View("InputCheckbox", args);
                    case KratosUiNodeInputAttributes.TypeEnum.Submit:
                        var inputAttr = args.node.Attributes.GetKratosUiNodeInputAttributes();
                        if (args.FlowType != FlowType.Settings 
                            && inputAttr.Type == KratosUiNodeInputAttributes.TypeEnum.Submit
                            && inputAttr.Name == "provider") 
                            return View("InputSubmitOidc", args);
                        return View("InputSubmit", args);
                    case KratosUiNodeInputAttributes.TypeEnum.Button:
                        return View("InputButton", args);
                    default:
                        return View("InputDefault", args);
                }
            case KratosUiNode.TypeEnum.Img:
                return View("Image", args);
            case KratosUiNode.TypeEnum.A:
                return View("Anchor", args);
            case KratosUiNode.TypeEnum.Script:
                return View("Script", args);
            default:
                return View("Default", args);
        }
    }
}