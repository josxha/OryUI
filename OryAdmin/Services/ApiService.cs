using Ory.Hydra.Client.Api;
using Ory.Hydra.Client.Client;
using Ory.Keto.Client.Api;
using Ory.Kratos.Client.Api;
using MetadataApi = Ory.Hydra.Client.Api.MetadataApi;

namespace KratosAdmin.Services;

public class ApiService(EnvService env)
{
    // ory hydra
    public readonly MetadataApi HydraMetadata = new(new Configuration
    {
        BasePath = env.HydraAdminUrl
    });

    public readonly WellknownApi HydraWellknown = new(new Configuration
    {
        BasePath = env.HydraAdminUrl
    });

    // ory keto
    public readonly Ory.Keto.Client.Api.MetadataApi KetoMetadata = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoWriteUrl
    });

    public readonly PermissionApi KetoPermission = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoReadUrl
    });

    public readonly RelationshipApi KetoRelationship = new(new Ory.Keto.Client.Client.Configuration
    {
        BasePath = env.KetoWriteUrl
    });

    // ory kratos
    public readonly IdentityApi KratosIdentity = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.KratosAdminUrl
    });

    public readonly Ory.Kratos.Client.Api.MetadataApi KratosMetadata = new(new Ory.Kratos.Client.Client.Configuration
    {
        BasePath = env.KratosAdminUrl
    });
}