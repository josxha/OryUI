using System.Web;
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

    public string GetUrlForFlow(string flow, Dictionary<string, string>? query = null)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        if (query != null)
            foreach (var (key, value) in query)
                queryString.Add(key, value);

        var baseUrl = env.KratosBrowserUrl ?? env.KratosPublicUrl;
        return $"{baseUrl.RemoveTrailingSlash()}/self-service/{flow}/browser?{queryString}";
    }

    public string GetSettingsUrl(string flowId)
    {
        var baseUrl = env.KratosBrowserUrl ?? env.KratosPublicUrl;
        return $"{baseUrl.RemoveTrailingSlash()}/self-service/settings?flow={flowId}";
    }
}