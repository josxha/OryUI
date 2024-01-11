using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes;

public partial class KratosUiNodeAnchor
{
    [Parameter] public required KratosUiNode node { get; set; }

    private KratosUiNodeAnchorAttributes? _attributes;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeAnchorAttributes();
    }
}