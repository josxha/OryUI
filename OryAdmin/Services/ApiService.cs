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
    public readonly MetadataApi HydraMetadata = new(new Configuration
    {
        BasePath = env.HydraAdminUrl
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

    public readonly PermissionApi KetoPermission = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoWriteUrl
    });

    public readonly RelationshipApi KetoRelationship = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoReadUrl
    });

    // ory kratos
    public readonly IdentityApi KratosIdentity = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.OryAdminUrl
    });

    public readonly Ory.Kratos.Client.Api.MetadataApi KratosMetadata = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.OryAdminUrl
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