using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiNodeInput : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiNode node)
    {
        switch (node.Type)
        {
            case KratosUiNode.TypeEnum.Text:
                return View("Text", node);
            case KratosUiNode.TypeEnum.Input:
                switch (node.Attributes.GetKratosUiNodeInputAttributes().Type)
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
                        return View("InputField", node);
                    case KratosUiNodeInputAttributes.TypeEnum.Checkbox:
                        return View("InputCheckbox", node);
                    case KratosUiNodeInputAttributes.TypeEnum.Submit:
                        return View("InputSubmit", node);
                    case KratosUiNodeInputAttributes.TypeEnum.Button:
                        return View("InputButton", node);
                    default:
                        return View("InputDefault", node);
                }
            case KratosUiNode.TypeEnum.Img:
                return View("Img", node);
            case KratosUiNode.TypeEnum.A:
                return View("Anchor", node);
            case KratosUiNode.TypeEnum.Script:
                return View("Script", node);
            default:
                return View("Default", node);
        }
    }
}