using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes;

public partial class KratosUiNodeScript
{
    [Parameter] public required KratosUiNode node { get; set; }

    private KratosUiNodeScriptAttributes? _attributes;
    private bool _isLoading = true;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeScriptAttributes();
        _isLoading = false;
    }
}