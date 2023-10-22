using Ory.Kratos.Client.Model;

namespace KratosSelfService.Extensions;

public static class HttpContextExt
{
    public static KratosSession? GetSession(this HttpContext httpContext)
    {
        return (KratosSession?)httpContext.Items[typeof(KratosSession)];
    }
}