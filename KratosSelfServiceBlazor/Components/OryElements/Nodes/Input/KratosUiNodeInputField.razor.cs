using KratosSelfServiceBlazor.models;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes.Input;

public partial class KratosUiNodeInputField
{
    [Parameter] public required KratosUiNode node { get; set; }
    [Parameter] public required FlowType flowType { get; set; }
    [Parameter] public required string forgotPasswordUrl { get; set; }

    private KratosUiNodeInputAttributes _attributes = default!;
    private KratosUiText _uiText = default!;
    private bool _showForgotPasswordLink;
    private bool _isLoading = true;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeInputAttributes();
        _uiText = _attributes.Label ?? node.Meta.Label;
        _showForgotPasswordLink = _attributes.Type == KratosUiNodeInputAttributes.TypeEnum.Password
                                  && node.Group == KratosUiNode.GroupEnum.Password
                                  && flowType == FlowType.Login;
        _isLoading = false;
    }
}