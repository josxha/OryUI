using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Verification
{
    [SupplyParameterFromQuery(Name = "flow")]
    private Guid? flowId { get; set; }

    [SupplyParameterFromQuery(Name = "return_to")]
    string? returnTo { get; set; }

    [SupplyParameterFromQuery(Name = "message")]
    string? jsonMessages { get; set; }

    private KratosVerificationFlow _flow = default!;
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        if (flowId == null)
        {
            // initiate flow
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            nav.NavigateTo(api.GetUrlForBrowserFlow("verification", new Dictionary<string, string?>
            {
                ["return_to"] = returnTo
            }));
            return;
        }

        KratosVerificationFlow flow;
        try
        {
            flow = await api.Frontend.GetVerificationFlowAsync(flowId.ToString(),
                accessor.HttpContext!.Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not retrieve flow for flow ID: {Message}", exception.Message);
            nav.NavigateTo(api.GetUrlForBrowserFlow("verification", new Dictionary<string, string?>
            {
                ["return_to"] = returnTo
            }));
            return;
        }

        // check for custom messages in the query string
        if (!string.IsNullOrWhiteSpace(jsonMessages))
            try
            {
                var messages = JsonConvert.DeserializeObject<List<KratosUiText>>(jsonMessages);
                if (messages != null) flow.Ui.Messages.AddRange(messages);
            }
            catch (Exception exception)
            {
                logger.LogError("Could not parse UiText Message. Message: {Message}, {Json}", exception.Message,
                    jsonMessages);
            }

        _isLoading = false;
    }
}