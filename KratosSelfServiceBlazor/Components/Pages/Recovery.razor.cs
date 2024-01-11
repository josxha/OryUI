using KratosSelfServiceBlazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Recovery
{
    [SupplyParameterFromQuery(Name = "flow")]
    private Guid? flowId { get; set; }

    [SupplyParameterFromQuery(Name = "return_to")]
    private string? returnTo { get; set; }

    private bool _isLoading = true;
    private KratosRecoveryFlow _flow = default!;
    private string _loginUrl = default!;

    protected override async Task OnInitializedAsync()
    {
        if (flowId == null)
        {
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            nav.NavigateTo(api.GetUrlForBrowserFlow("recovery", new Dictionary<string, string?>
            {
                ["return_to"] = returnTo
            }));
        }

        try
        {
            _flow = await api.Frontend.GetRecoveryFlowAsync(flowId.ToString(),
                accessor.HttpContext!.Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            nav.NavigateTo(api.GetUrlForBrowserFlow("recovery", new Dictionary<string, string?>
            {
                ["return_to"] = returnTo
            }));
            return;
        }

        _loginUrl = api.GetUrlForBrowserFlow("login", new Dictionary<string, string?>
        {
            ["return_to"] = returnTo
        });
        _isLoading = false;
    }
}