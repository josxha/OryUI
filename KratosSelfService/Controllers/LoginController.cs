using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class LoginController(ILogger<LoginController> logger, ApiService api) : Controller
{
    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            // initiate flow
            var url = api.GetUrlForFlow("login");
            return Redirect(url);
        }

        KratosLoginFlow flow;
        try
        {
            flow = await api.Frontend.GetLoginFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            return Redirect("login");
        }

        if (flow.Ui.Messages?.Any(text => text.Id == 4000010) ?? false)
            // the login requires that the user verifies their email address before logging in
            // we will create a new verification flow and redirect the user to the verification page
            try
            {
                var response = await api.Frontend
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
                // restart flow
                return Redirect("login");
            }

        return View("Login", flow);
    }
}