using KratosSelfServiceBlazor.models;
using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.OryElements;

public partial class KratosUiNodeComponent
{
    [Parameter] public required KratosUiNode node { get; set; }
    [Parameter] public required FlowType flowType { get; set; }
    [Parameter] public required string? forgotPasswordUrl { get; set; }
}