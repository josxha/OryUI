using KratosSelfService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfService.Controllers;

public class EntranceController(ILogger<EntranceController> logger, ApiService api) : Controller
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

    [HttpGet("recovery")]
    public async Task<IActionResult> Recovery([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            var newFlow = await api.Frontend.CreateBrowserRecoveryFlowAsync();
            return Redirect($"recovery?flow={newFlow.Id}");
        }

        KratosRecoveryFlow flow;
        try
        {
            flow = await api.Frontend.GetRecoveryFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            return Redirect("recovery");
        }

        return View("Recovery", flow);
    }

    [HttpGet("registration")]
    public async Task<IActionResult> Registration([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            // initiate flow
            var url = api.GetUrlForFlow("registration");
            return Redirect(url);
        }

        KratosRegistrationFlow flow;
        try
        {
            flow = await api.Frontend.GetRegistrationFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            logger.LogError(exception.Message);
            // restart flow
            return Redirect("registration");
        }

        return View("Registration", flow);
    }

    [HttpGet("verification")]
    public async Task<IActionResult> Verification([FromQuery(Name = "flow")] string? flowId)
    {
        if (flowId == null)
        {
            // initiate flow
            var url = api.GetUrlForFlow("verification");
            return Redirect(url);
        }

        KratosVerificationFlow flow;
        try
        {
            flow = await api.Frontend.GetVerificationFlowAsync(flowId, Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }

        return View("Verification", flow);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        KratosLogoutFlow flow;
        try
        {
            flow = await api.Frontend.CreateBrowserLogoutFlowAsync(Request.Headers.Cookie);
        }
        catch (ApiException exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }

        return Redirect(flow.LogoutUrl);
    }
}