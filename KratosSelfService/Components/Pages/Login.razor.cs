using System.Web;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Components.Pages;

// https://www.ory.sh/docs/kratos/self-service/flows/user-login
public partial class Login
{
    private string? _errorMessage;

    [Inject] private ApiService ApiService { get; set; } = default!;
    [SupplyParameterFromForm] private LoginForm? FormData { get; set; }

    [SupplyParameterFromQuery(Name = "flow")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private string? FlowId { get; set; }

    private KratosLoginFlow? Flow { get; set; }

    protected override async Task OnInitializedAsync()
    {
        FormData ??= new LoginForm();
        if (FlowId == null)
        {
            InitiateFlow();
            return;
        }

        var headers = HttpContextAccessor.HttpContext?.Request.GetTypedHeaders();
        var cookies = headers?.Cookie;
        var csrfToken =
            cookies?.First(cookie => cookie.Name.StartsWith("csrf_token", StringComparison.InvariantCulture));
        try
        {
            Flow = await ApiService.FrontendApi.GetLoginFlowAsync(FlowId, csrfToken?.ToString());
        }
        catch (ApiException exception)
        {
            // TODO not working
            Logger.LogError(exception.Message);
            Navigation.NavigateTo("login");
            return;
        }


        // the login requires that the user verifies their email address before logging in
        if (Flow.Ui.Messages?.Any(text => text.Id == 4000010) ?? false)
            // we will create a new verification flow and redirect the user to the verification page
            await RedirectToVerificationFlow(Flow);
    }

    private async Task RedirectToVerificationFlow(KratosLoginFlow loginFlow)
    {
        try
        {
            var response =
                await ApiService.FrontendApi.CreateBrowserVerificationFlowWithHttpInfoAsync(loginFlow.ReturnTo);
            var verificationFlow = response.Data;
            // we need the csrf cookie from the verification flow
            HttpContextAccessor.HttpContext?.Response.Headers.Add(HeaderNames.SetCookie,
                response.Headers[HeaderNames.SetCookie].ToString());
            // encode the verification flow id in the query parameters
            var parameters = $"flow={verificationFlow.Id}&message={loginFlow.Ui.Messages}";

            var baseUrl = new Uri(HttpContextAccessor.HttpContext!.Request.Path).Segments;

            //Navigation.NavigateTo(baseUrl);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception.Message);
            Navigation.NavigateTo("login");
        }
    }

    private void InitiateFlow()
    {
        const string returnTo = "http://127.0.0.1:5110";
        var uri = $"http://127.0.0.1:4433/self-service/login/browser?" +
                  $"aal=&" +
                  $"refresh=&" +
                  $"return_to={HttpUtility.HtmlEncode(returnTo)}";
        Navigation.NavigateTo(uri);
    }
}