using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;

namespace KratosAdmin.Services;

public class ApiService(EnvService env)
{
    public readonly IdentityApi Identity = new(new Configuration
    {
        BasePath = env.KratosAdminUrl
    });
    public readonly MetadataApi Metadata = new(new Configuration
    {
        BasePath = env.KratosAdminUrl
    });
}