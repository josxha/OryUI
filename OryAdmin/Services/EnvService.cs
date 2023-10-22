namespace OryAdmin.Services;

public class EnvService
{
    public readonly bool EnabledHydra;
    public readonly bool EnabledKeto;
    public readonly bool EnabledKratos;
    public readonly bool EnabledOathkeeper;

    // ory hydra
    public readonly string HydraAdminUrl =
        Environment.GetEnvironmentVariable("HYDRA_ADMIN_URL") ?? "http://127.0.0.1:4445";

    public readonly string HydraPublicUrl =
        Environment.GetEnvironmentVariable("HYDRA_PUBLIC_URL") ?? "http://127.0.0.1:4444";

    // ory keto
    public readonly string KetoReadUrl =
        Environment.GetEnvironmentVariable("KETO_READ_URL") ?? "http://127.0.0.1:4466";

    public readonly string KetoWriteUrl =
        Environment.GetEnvironmentVariable("KETO_WRITE_URL") ?? "http://127.0.0.1:4467";

    // ory kratos
    public readonly string KratosAdminUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4434";

    public readonly string KratosPublicUrl =
        Environment.GetEnvironmentVariable("KRATOS_PUBLIC_URL") ?? "http://127.0.0.1:4433";

    // ory oathkeeper
    public readonly string OathKeeperApiUrl =
        Environment.GetEnvironmentVariable("OATHKEEPER_API_URL") ?? "http://127.0.0.1:4456";

    public EnvService()
    {
        var rawServices = Environment.GetEnvironmentVariable("ENABLED_SERVICES") ?? "kratos,hydra,oathkeeper,keto";
        foreach (var item in rawServices.Split(","))
        {
            var service = item.Trim();
            switch (service)
            {
                case "kratos":
                    EnabledKratos = true;
                    break;
                case "hydra":
                    EnabledHydra = true;
                    break;
                case "oathkeeper":
                    EnabledOathkeeper = true;
                    break;
                case "keto":
                    EnabledKeto = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(service), service,
                        "The SERVICES environment variable contains an invalid service name.");
            }
        }
    }
}