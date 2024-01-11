using KratosSelfServiceBlazor.models;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes.Input;

public partial class KratosUiNodeInputSubmit
{
    [Parameter] public required KratosUiNode node { get; set; }
    [Parameter] public required FlowType flowType { get; set; }

    private KratosUiNodeInputAttributes _attributes = default!;
    private KratosUiText _uiText = default!;
    private bool _isLoading = true;
    private bool _isFullWidth;
    private string _buttonType = default!;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeInputAttributes();
        _uiText = _attributes.Label ?? node.Meta.Label;
        _isFullWidth = flowType == FlowType.Settings;
        _buttonType = _attributes.Name switch
        {
            "lookup_secret_disable" or "webauthn_remove" or "totp_unlink" or "unlink" => "is-warning",
            _ => "is-info"
        };
        _isLoading = false;
    }
}