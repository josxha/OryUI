using Ory.Hydra.Client.Api;
using Ory.Hydra.Client.Client;
using Ory.Keto.Client.Api;
using Ory.Kratos.Client.Api;
using Ory.Oathkeeper.Client.Api;
using MetadataApi = Ory.Hydra.Client.Api.MetadataApi;

namespace OryAdmin.Services;

public class ApiService(EnvService env)
{
    // ory hydra
    public readonly JwkApi HydraJwk = new(new Configuration
    {
        BasePath = env.HydraAdminUrl
    });

    public readonly MetadataApi HydraMetadata = new(new Configuration
    {
        BasePath = env.HydraAdminUrl
    });

    public readonly OAuth2Api HydraOAuth2 = new(new Configuration
    {
        BasePath = env.HydraAdminUrl
    });

    public readonly OidcApi HydraOidc = new(new Configuration
    {
        BasePath = env.HydraPublicUrl
    });

    public readonly WellknownApi HydraWellknown = new(new Configuration
    {
        BasePath = env.HydraPublicUrl
    });

    // ory keto
    public readonly Ory.Keto.Client.Api.MetadataApi KetoMetadata = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoWriteUrl
    });

    public readonly PermissionApi KetoPermissionWrite = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoWriteUrl
    });

    public readonly PermissionApi KetoPermissionRead = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoReadUrl
    });

    public readonly RelationshipApi KetoRelationshipRead = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoReadUrl
    });

    public readonly RelationshipApi KetoRelationshipWrite = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoWriteUrl
    });

    // ory kratos
    public readonly CourierApi KratosCourier = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.KratosAdminUrl
    });

    public readonly FrontendApi KratosFrontend = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.KratosPublicUrl
    });

    public readonly IdentityApi KratosIdentity = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.KratosAdminUrl
    });

    public readonly Ory.Kratos.Client.Api.MetadataApi KratosMetadata = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.KratosAdminUrl
    });

    // ory oathkeeper
    public readonly ApiApi OathKeeperApi = new(new Ory.Oathkeeper.Client.Client.Configuration
    {
        BasePath = env.OathKeeperApiUrl
    });

    public readonly HealthApi OathKeeperHealth = new(new Ory.Oathkeeper.Client.Client.Configuration
    {
        BasePath = env.OathKeeperApiUrl
    });

    public readonly VersionApi OathKeeperVersion = new(new Ory.Oathkeeper.Client.Client.Configuration
    {
        BasePath = env.OathKeeperApiUrl
    });
}