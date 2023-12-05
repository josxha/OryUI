namespace KratosSelfService.Services;

public class EnvService
{
    /// <summary>
    ///     ORY_SDK_URL or HYDRA_ADMIN_URL (optional): The URL where Ory Hydra's Public API is located at.
    ///     If this app and Ory Hydra are running in the same private network, this should be the private
    ///     network address (e.g. hydra-admin.svc.cluster.local)
    /// </summary>
    public readonly string? HydraAdminUrl =
        Environment.GetEnvironmentVariable("HYDRA_ADMIN_URL");

    public readonly string? HydraCsrfCookieName =
        Environment.GetEnvironmentVariable("CSRF_COOKIE_NAME");

    public readonly string? HydraCsrfCookieSecret =
        Environment.GetEnvironmentVariable("CSRF_COOKIE_SECRET");

    public readonly int HydraRememberConsentSessionForSeconds =
        int.Parse(Environment.GetEnvironmentVariable("REMEMBER_CONSENT_SESSION_FOR_SECONDS") ?? "3600");

    public readonly string? HydraTrustedClientIds =
        Environment.GetEnvironmentVariable("TRUSTED_CLIENT_IDS");

    /// <summary>
    ///     KRATOS_BROWSER_URL (optional) The browser accessible URL where ORY Kratos's public API is located,
    ///     only needed if it differs from KRATOS_PUBLIC_URL
    /// </summary>
    public readonly string? KratosBrowserUrl =
        Environment.GetEnvironmentVariable("KRATOS_BROWSER_URL");

    /// <summary>
    ///     ORY_SDK_URL or KRATOS_PUBLIC_URL (required): The URL where ORY Kratos's Public API is located at.
    ///     If this app and ORY Kratos are running in the same private network, this should be the private
    ///     network address (e.g. kratos-public.svc.cluster.local).
    /// </summary>
    public readonly string KratosPublicUrl =
        Environment.GetEnvironmentVariable("KRATOS_PUBLIC_URL") ?? "http://127.0.0.1:4433";
}