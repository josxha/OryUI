using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;

namespace KratosSelfService.Services;

public class ApiService
{
    private readonly Configuration _Config;
    public readonly FrontendApi FrontendApi;

    public ApiService(EnvService envService)
    {
        _Config = new Configuration
        {
            BasePath = "http://127.0.0.1:4433"
        };
        FrontendApi = new FrontendApi
        {
            Configuration = _Config,
            Client = new ApiClient("http://127.0.0.1:4433"),
            AsynchronousClient = new ApiClient("http://127.0.0.1:4433")
        };
    }
}