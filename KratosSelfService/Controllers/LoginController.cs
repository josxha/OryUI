using System.Web;
using KratosSelfService.Models;
using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

// https://www.ory.sh/docs/kratos/self-service/flows/user-login
public class LoginController(ILogger<LoginController> logger, ApiService api) : Controller
{
    [HttpGet("login")]
    public async Task<IActionResult> Index([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            // initiate flow
            const string returnTo = "http://127.0.0.1:5110";
            var url = $"http://127.0.0.1:4433/self-service/login/browser?" +
                      $"aal=&" +
                      $"refresh=&" +
                      $"return_to={HttpUtility.HtmlEncode(returnTo)}";
            return Redirect(url);
        }

        var headers = Request.GetTypedHeaders();
        var cookies = headers.Cookie;
        var csrfToken =
            cookies.First(cookie => cookie.Name.StartsWith("csrf_token", StringComparison.InvariantCulture));
        KratosLoginFlow flow;
        try
        {
            flow = await api.FrontendApi.GetLoginFlowAsync(flowId, csrfToken?.ToString());
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart login flow
            return Redirect("login");
        }


        // the login requires that the user verifies their email address before logging in
        if (flow.Ui.Messages?.Any(text => text.Id == 4000010) ?? false)
            // we will create a new verification flow and redirect the user to the verification page
            try
            {
                var response = await api.FrontendApi
                    .CreateBrowserVerificationFlowWithHttpInfoAsync(flow.ReturnTo);
                var verificationFlow = response.Data;
                // we need the csrf cookie from the verification flow
                Response.Headers.Add(HeaderNames.SetCookie, response.Headers[HeaderNames.SetCookie].ToString());
                // encode the verification flow id in the query parameters
                var parameters = $"flow={verificationFlow.Id}&message={flow.Ui.Messages}";

                var baseUrl = new Uri(Request.Path).Segments;
                //Navigation.NavigateTo(baseUrl);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                // restart login flow
                return Redirect("login");
            }

        var model = new LoginModel(flowId, flow);
        return View("Index", model);
    }
}