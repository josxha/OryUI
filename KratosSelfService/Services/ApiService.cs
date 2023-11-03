using KratosSelfService.Extensions;
using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;

namespace KratosSelfService.Services;

public class ApiService(EnvService env)
{
    public readonly FrontendApi Frontend = new(new Configuration
    {
        BasePath = env.KratosPublicUrl
    });

    public string GetUrlForBrowserFlow(string flow, Dictionary<string, string?>? query = null)
    {
        var queryString = query?.EncodeQueryString();
        var baseUrl = env.KratosBrowserUrl ?? env.KratosPublicUrl;
        return $"{baseUrl.RemoveTrailingSlash()}/self-service/{flow}/browser?{queryString}";
    }
}