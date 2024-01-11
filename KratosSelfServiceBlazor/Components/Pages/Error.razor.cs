using KratosSelfServiceBlazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Error
{
    [SupplyParameterFromQuery(Name = "id")]
    private Guid? flowId { get; set; }

    private bool _isLoading = true;

    private JObject _error = default!;

    protected override async Task OnInitializedAsync()
    {
        var errorFlow = await api.Frontend.GetFlowErrorAsync(flowId.ToString());
        _error = (JObject)errorFlow.Error;
        logger.LogError(_error.ToString());
        _isLoading = false;
    }
}