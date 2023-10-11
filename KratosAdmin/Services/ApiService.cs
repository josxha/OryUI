using Ory.Client.Api;
using Ory.Client.Client;

namespace KratosAdmin.Services;

public class ApiService
{
    private readonly Configuration _adminConfig;
    public readonly IdentityApi IdentityApi;

    public ApiService(EnvService envService)
    {
        _adminConfig = new Configuration
        {
            BasePath = envService.KratosAdminUrl
        };
        IdentityApi = new IdentityApi(_adminConfig);
    }
}