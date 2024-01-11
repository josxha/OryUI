using KratosSelfServiceBlazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Components.Pages;

public partial class Login
{
    [SupplyParameterFromQuery(Name = "flow")]
    private Guid? flowId { get; set; }

    [SupplyParameterFromQuery] private string? aal { get; set; }

    [SupplyParameterFromQuery] private string? refresh { get; set; }

    [SupplyParameterFromQuery(Name = "return_to")]
    private string? returnTo { get; set; }

    [SupplyParameterFromQuery] private string? organization { get; set; }

    [SupplyParameterFromQuery(Name = "login_challenge")]
    private string? loginChallenge { get; set; }

    private bool _isLoading = true;

    private KratosLoginFlow _flow = default!;
    private string? _forgotPasswordUrl;
    private string _signupUrl = default!;
    private string? _logoutUrl;
    private bool _is2Fa;

    protected override async Task OnInitializedAsync()
    {
        // oauth2 login challenge
        if (!string.IsNullOrWhiteSpace(loginChallenge))
            logger.LogDebug($"login_challenge found in URL query: {loginChallenge}");

        if (flowId == null)
        {
            logger.LogDebug("No flow ID found in URL query initializing login flow");
            // workaround: if the user is in the 2fa flow, the user would get redirected infinitely,
            // therefore log the user out first
            if (accessor.HttpContext!.Request.Headers.Cookie.Any(s => s?.Contains("ory_kratos_session=") ?? false))
            {
                nav.NavigateTo("logout");
                return;
            }

            // initiate flow
            nav.NavigateTo(GetInitFlowUrl());
            return;
        }

        try
        {
            _flow = await api.Frontend.GetLoginFlowAsync(flowId.ToString(),
                accessor.HttpContext!.Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError("Error while getting the login flow, starting new flow. {Message}", exception.Message);
            // restart flow
            nav.NavigateTo(GetInitFlowUrl());
            return;
        }

        if (_flow.Ui.Messages?.Any(text => text.Id == 4000010) ?? false)
        {
            // the login requires that the user verifies their email address before logging in
            // we will create a new verification flow and redirect the user to the verification page
            nav.NavigateTo(await GetVerificationFlowUrl(_flow));
            return;
        }

        // Render the data using a view:
        var initRegistrationQuery = new Dictionary<string, string?>
        {
            ["return_to"] = _flow.ReturnTo
        };
        if (_flow.Oauth2LoginRequest?.Challenge != null)
            initRegistrationQuery["login_challenge"] = _flow.Oauth2LoginChallenge;

        _signupUrl = api.GetUrlForBrowserFlow("registration", initRegistrationQuery);
        if (!_flow.Refresh)
            _forgotPasswordUrl = api.GetUrlForBrowserFlow("recovery", new Dictionary<string, string?>
            {
                ["return_to"] = _flow.ReturnTo
            });

        if (_flow.RequestedAal == KratosAuthenticatorAssuranceLevel.Aal2 || _flow.Refresh)
            _logoutUrl = await GetLogoutUrl(_flow);

        _is2Fa = _flow.Ui.Nodes?.Any(node =>
            node.Type == KratosUiNode.TypeEnum.Input &&
            node.Attributes.GetKratosUiNodeInputAttributes().Name == "lookup_secret") ?? false;

        _isLoading = false;
    }

    private async Task<string?> GetLogoutUrl(KratosLoginFlow flow)
    {
        // It is probably a bit strange to have a logout URL here, however this screen
        // is also used for 2FA flows. If something goes wrong there, we probably want
        // to give the user the option to sign out!
        try
        {
            var logoutFlow =
                await api.Frontend.CreateBrowserLogoutFlowAsync(accessor.HttpContext!.Request.Headers.Cookie,
                    flow.ReturnTo);
            return logoutFlow.LogoutUrl;
        }
        catch (Exception exception)
        {
            logger.LogError("Unable to create logout URL. {Message}", exception.Message);
            return null;
        }
    }

    private string GetInitFlowUrl()
    {
        var query = new Dictionary<string, string?>
        {
            ["aal"] = aal ?? "",
            ["refresh"] = refresh ?? "",
            ["return_to"] = returnTo ?? "",
            ["organization"] = organization ?? ""
        };
        if (!string.IsNullOrWhiteSpace(loginChallenge)) query["login_challenge"] = loginChallenge;

        return api.GetUrlForBrowserFlow("login", query);
    }

    private async Task<string> GetVerificationFlowUrl(KratosLoginFlow flow)
    {
        try
        {
            var response = await api.Frontend
                .CreateBrowserVerificationFlowWithHttpInfoAsync(flow.ReturnTo);
            var verificationFlow = response.Data;
            // we need the csrf cookie from the verification flow
            accessor.HttpContext!.Response.Headers.Append(HeaderNames.SetCookie,
                response.Headers[HeaderNames.SetCookie].ToString());
            // encode the verification flow id in the query parameters
            var paramDict = new Dictionary<string, string?>
            {
                ["flow"] = verificationFlow.Id,
                ["message"] = flow.Ui.Messages.ToString()
            };
            var parameters = paramDict.EncodeQueryString();

            return $"{accessor.HttpContext!.Request.PathBase}/verification?{parameters}";
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return api.GetUrlForBrowserFlow("verification", new Dictionary<string, string?>
            {
                ["return_to"] = flow.ReturnTo
            });
        }
    }
}