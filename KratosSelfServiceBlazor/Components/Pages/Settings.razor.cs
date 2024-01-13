using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Settings
{
    [SupplyParameterFromQuery(Name = "flow")]
    private Guid? flowId { get; set; }

    [SupplyParameterFromQuery(Name = "return_to")]
    private string? returnTo { get; set; }

    private bool _isLoading = true;
    private KratosSettingsFlow _flow = default!;

    private IGrouping<KratosUiNode.GroupEnum, KratosUiNode>[] _groups = default!;

    private KratosUiNode[] _defaultNodes = default!;

    protected override async Task OnInitializedAsync()
    {
        if (flowId == null)
        {
            // init new flow
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            var newFlow = await api.Frontend
                .CreateBrowserSettingsFlowAsync(cookie: accessor.HttpContext!.Request.Headers.Cookie,
                    returnTo: returnTo);
            nav.NavigateTo($"/settings?flow={newFlow.Id}");
            return;
        }

        try
        {
            _flow = await api.Frontend.GetSettingsFlowAsync(flowId.ToString(),
                cookie: accessor.HttpContext!.Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            // create new flow
            logger.LogDebug("Exception while retrieving settings flow: {Message}", exception.Message);
            var newFlow = await api.Frontend
                .CreateBrowserSettingsFlowAsync(cookie: accessor.HttpContext!.Request.Headers.Cookie,
                    returnTo: returnTo);
            nav.NavigateTo($"/settings?flow={newFlow.Id}");
            return;
        }

        var allGroups = _flow.Ui.Nodes
            .GroupBy(node => node.Group).ToList();
        _defaultNodes = allGroups
            .First(group => group.Key == KratosUiNode.GroupEnum.Default)
            .ToArray();
        _groups = allGroups
            .Where(group => group.Key != KratosUiNode.GroupEnum.Default)
            .ToArray();
        _isLoading = false;
    }
}