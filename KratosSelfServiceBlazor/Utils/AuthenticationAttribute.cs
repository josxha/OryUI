using KratosSelfServiceBlazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ory.Kratos.Client.Client;
using Ory.Kratos.Client.Model;

namespace KratosSelfServiceBlazor.Utils;

[AttributeUsage(AttributeTargets.Method)]
public class AuthenticationAttribute : Attribute,
    IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<AuthenticationAttribute>>();
        var api = context.HttpContext.RequestServices.GetRequiredService<ApiService>();
        var cookie = context.HttpContext.Request.Headers.Cookie;
        KratosSession session;
        try
        {
            session = await api.Frontend.ToSessionAsync(cookie: cookie);
        }
        catch (ApiException exception)
        {
            logger.LogDebug("Could not get session: {Message}", exception.Message);
            context.Result = new UnauthorizedResult();
            return;
        }

        context.HttpContext.Items[typeof(KratosSession)] = session;
        await next();
    }
}