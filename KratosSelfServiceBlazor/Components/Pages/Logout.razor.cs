using KratosSelfServiceBlazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Logout
{
    [SupplyParameterFromQuery(Name = "challenge")]
    private string? logoutChallenge { get; set; }
}