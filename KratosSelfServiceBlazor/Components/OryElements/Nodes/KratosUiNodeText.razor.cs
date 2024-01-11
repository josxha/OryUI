using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes;

public partial class KratosUiNodeText
{
    [Parameter] public required KratosUiNode node { get; set; }

    private KratosUiNodeTextAttributes? _attributes;
    private bool _isLoading = true;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeTextAttributes();
        _isLoading = false;
    }
}