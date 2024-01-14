using Microsoft.AspNetCore.Components;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Registration
{
    [SupplyParameterFromQuery(Name = "flow")]
    private Guid? flowId { get; set; }

    [SupplyParameterFromQuery(Name = "return_to")]
    private string? returnTo { get; set; }

    [SupplyParameterFromQuery(Name = "after_verification_return_to")]
    private string? afterVerificationReturnTo { get; set; }

    [SupplyParameterFromQuery(Name = "login_challenge")]
    private string? loginChallenge { get; set; }

    [SupplyParameterFromQuery] private string? organization { get; set; }

    private KratosRegistrationFlow _flow;
    private string _loginUrl;

    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("OnInitializedAsync");
        if (loginChallenge != null)
            logger.LogDebug("login_challenge found in URL query: {Challenge}", loginChallenge);
        else
            logger.LogDebug("no login_challenge found in URL query.");

        if (flowId == null)
        {
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            // initiate flow
            var parameters = new Dictionary<string, string?>();
            if (returnTo != null) parameters["return_to"] = returnTo;
            if (organization != null) parameters["organization"] = organization;
            if (afterVerificationReturnTo != null)
                parameters["after_verification_return_to"] = afterVerificationReturnTo;
            if (loginChallenge != null) parameters["login_challenge"] = loginChallenge;
            nav.NavigateTo(api.GetUrlForBrowserFlow("registration", parameters));
            return;
        }

        try
        {
            _flow = await api.Frontend.GetRegistrationFlowAsync(flowId.ToString(),
                accessor.HttpContext!.Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            var parameters = new Dictionary<string, string?>();
            if (returnTo != null) parameters["return_to"] = returnTo;
            if (organization != null) parameters["organization"] = organization;
            if (afterVerificationReturnTo != null)
                parameters["after_verification_return_to"] = afterVerificationReturnTo;
            if (loginChallenge != null) parameters["login_challenge"] = loginChallenge;
            nav.NavigateTo(api.GetUrlForBrowserFlow("registration", parameters));
            return;
        }

        var loginParams = new Dictionary<string, string?>();
        if (returnTo != null) loginParams["return_to"] = returnTo;
        if (loginChallenge != null) loginParams["login_challenge"] = loginChallenge;
        _loginUrl = api.GetUrlForBrowserFlow("login", loginParams);

        _isLoading = false;
    }
}