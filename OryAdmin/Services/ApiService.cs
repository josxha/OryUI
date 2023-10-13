using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;

namespace KratosAdmin.Services;

public class ApiService(EnvService env)
{
    public readonly IdentityApi IdentityApi = new(new Configuration
    {
        BasePath = env.KratosAdminUrl
    });
}