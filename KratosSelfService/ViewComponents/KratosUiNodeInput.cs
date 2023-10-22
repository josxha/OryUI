using KratosSelfService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.ViewComponents;

public class KratosUiNodeInput : ViewComponent
{
    public async Task<ViewViewComponentResult> InvokeAsync(KratosUiNodeModel model)
    {
        switch (model.node.Type)
        {
            case KratosUiNode.TypeEnum.Text:
                return View("Text", model);
            case KratosUiNode.TypeEnum.Input:
                switch (model.node.Attributes.GetKratosUiNodeInputAttributes().Type)
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
                        return View("InputField", model);
                    case KratosUiNodeInputAttributes.TypeEnum.Checkbox:
                        return View("InputCheckbox", model);
                    case KratosUiNodeInputAttributes.TypeEnum.Submit:
                        return View("InputSubmit", model);
                    case KratosUiNodeInputAttributes.TypeEnum.Button:
                        return View("InputButton", model);
                    default:
                        return View("InputDefault", model);
                }
            case KratosUiNode.TypeEnum.Img:
                return View("Image", model);
            case KratosUiNode.TypeEnum.A:
                return View("Anchor", model);
            case KratosUiNode.TypeEnum.Script:
                return View("Script", model);
            default:
                return View("Default", model);
        }
    }
}