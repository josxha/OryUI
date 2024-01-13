using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

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