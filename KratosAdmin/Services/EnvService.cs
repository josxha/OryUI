using Ory.Client.Client;

namespace KratosAdmin.Services;

public class EnvService
{
    public readonly string KratosAdminUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4434";

    public readonly string KratosPublicUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4433";

    public Configuration KratosAdminConfig()
    {
        return new Configuration
        {
            BasePath = KratosAdminUrl
        };
    }
}