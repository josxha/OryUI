using KratosSelfServiceBlazor.models;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes.Input;

public partial class KratosUiNodeInputCheckbox
{
    [Parameter] public required KratosUiNode node { get; set; }
    [Parameter] public required FlowType flowType { get; set; }

    private KratosUiNodeInputAttributes _attributes = default!;
    private KratosUiText _uiText = default!;
    private bool _isLoading = true;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeInputAttributes();
        _uiText = _attributes.Label ?? node.Meta.Label;
        _isLoading = false;
    }
}