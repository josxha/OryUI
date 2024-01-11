using Microsoft.AspNetCore.Components;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Logout
{
    [SupplyParameterFromQuery(Name = "challenge")]
    private string? logoutChallenge { get; set; }
}