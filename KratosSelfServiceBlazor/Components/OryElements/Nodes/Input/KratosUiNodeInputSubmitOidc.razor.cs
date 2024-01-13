using KratosSelfServiceBlazor.models;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes.Input;

public partial class KratosUiNodeInputSubmitOidc
{
    [Parameter] public required KratosUiNode node { get; set; }
    [Parameter] public required FlowType flowType { get; set; }

    private KratosUiNodeInputAttributes _attributes = default!;
    private bool _isLoading = true;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeInputAttributes();
        _isLoading = false;
    }
}