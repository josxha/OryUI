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

    public readonly string KratosAdminUrl =
        Environment.GetEnvironmentVariable("KRATOS_ADMIN_URL") ?? "http://127.0.0.1:4434";

    /// <summary>
    ///     REMEMBER_CONSENT_SESSION_FOR_SECONDS (optional): Sets the remember_for value of the accept consent
    ///     request in seconds. The default is 3600 seconds.
    /// </summary>
    public readonly int HydraRememberConsentSessionForSeconds =
        int.Parse(Environment.GetEnvironmentVariable("REMEMBER_CONSENT_SESSION_FOR_SECONDS") ?? "3600");

    /// <summary>
    ///     HYDRA_TRAITS_MAPPING (optional) Map kratos identity traits to oidc claims for the ID Token.
    ///     Example: email:email,name.first:given_name,name.last:familiy_name
    /// </summary>
    public readonly string? HydraTraitsMapping =
        Environment.GetEnvironmentVariable("HYDRA_TRAITS_MAPPING");

    /// <summary>
    ///     TRUSTED_CLIENT_IDS (optional): A list of trusted client ids.
    ///     They can be set to skip the consent screen.
    /// </summary>
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

    /// <summary>
    ///     The optional logo URI for the website
    /// </summary>
    public readonly string? WebsiteLogoUri =
        Environment.GetEnvironmentVariable("WEBSITE_LOGO_URI");

    /// <summary>
    ///     The optional logo URI for the website
    /// </summary>
    public readonly string? WebsiteTitle =
        Environment.GetEnvironmentVariable("WEBSITE_TITLE") ?? "OryUI";
}