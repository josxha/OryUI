namespace KratosAdmin.Services;

public class EnvService
{
    public readonly string HydraAdminUrl =
        Environment.GetEnvironmentVariable("HYDRA_ADMIN_URL") ?? "http://127.0.0.1:4445";

    public readonly string KetoReadUrl =
        Environment.GetEnvironmentVariable("KETO_READ_URL") ?? "http://127.0.0.1:4466";

    public readonly string KetoWriteUrl =
        Environment.GetEnvironmentVariable("KETO_WRITE_URL") ?? "http://127.0.0.1:4467";

    public readonly string KratosAdminUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4434";
}