using Ory.Kratos.Client.Api;

namespace KratosAdmin.Services;

public class ApiService
{
    public readonly IdentityApi IdentityApi;

    public ApiService(EnvService envService)
    {
        var adminConfig = envService.KratosAdminConfig();
        IdentityApi = new IdentityApi(adminConfig);
    }
}