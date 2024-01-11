using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements.Nodes;

public partial class KratosUiNodeImage
{
    [Parameter] public required KratosUiNode node { get; set; }

    private KratosUiNodeImageAttributes? _attributes;
    private bool _isLoading = true;

    protected override void OnInitialized()
    {
        _attributes = node.Attributes.GetKratosUiNodeImageAttributes();
    }
}