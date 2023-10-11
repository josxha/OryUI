using Ory.Kratos.Client.Client;

namespace KratosSelfService;

public class EnvService
{
    public readonly string KratosPublicUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4433";

    public string BrowserUrl;

    public Configuration KratosAdminConfig()
    {
        return new Configuration
        {
            BasePath = KratosPublicUrl
        };
    }
}