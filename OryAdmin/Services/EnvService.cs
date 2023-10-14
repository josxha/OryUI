using KratosAdmin.Models;

namespace KratosAdmin.Services;

public class EnvService
{
    public readonly OryService[] EnabledServices =
        (Environment.GetEnvironmentVariable("SERVICES") ?? "kratos,hydra,oathkeeper,keto")
        .Split(",")
        .Select(s => s.Trim())
        .Select(s => s switch
        {
            "kratos" => OryService.Kratos,
            "hydra" => OryService.Hydra,
            "oathkeeper" => OryService.OathKeeper,
            "keto" => OryService.Keto,
            _ => throw new ArgumentOutOfRangeException(nameof(s), s,
                "The SERVICES environment variable contains an invalid service name.")
        })
        .ToArray();

    public readonly string HydraAdminUrl =
        Environment.GetEnvironmentVariable("HYDRA_ADMIN_URL") ?? "http://127.0.0.1:4445";

    public readonly string HydraPublicUrl =
        Environment.GetEnvironmentVariable("HYDRA_PUBLIC_URL") ?? "http://127.0.0.1:4444";

    public readonly string KetoReadUrl =
        Environment.GetEnvironmentVariable("KETO_READ_URL") ?? "http://127.0.0.1:4466";

    public readonly string KetoWriteUrl =
        Environment.GetEnvironmentVariable("KETO_WRITE_URL") ?? "http://127.0.0.1:4467";

    public readonly string KratosAdminUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4434";

    public readonly string OathKeeperApiUrl =
        Environment.GetEnvironmentVariable("OATHKEEPER_API_URL") ?? "http://127.0.0.1:4456";

    public bool ServiceEnabled(OryService service)
    {
        return EnabledServices.Contains(service);
    }
}