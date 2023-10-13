using System.Web;
using KratosSelfService.Extensions;
using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;

namespace KratosSelfService.Services;

public class ApiService(EnvService env)
{
    public readonly FrontendApi FrontendApi = new()
    {
        Configuration = new Configuration
        {
            BasePath = env.KratosPublicUrl
        },
        Client = new ApiClient("http://127.0.0.1:4433"),
        AsynchronousClient = new ApiClient("http://127.0.0.1:4433")
    };

    public string GetUrlForFlow(string flow, Dictionary<string, string>? query = null)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        if (query != null)
            foreach (var (key, value) in query)
                queryString.Add(key, value);

        var baseUrl = env.KratosBrowserUrl ?? env.KratosPublicUrl;
        return $"{baseUrl.RemoveTrailingSlash()}/self-service/{flow}/browser?{queryString}";
    }
}