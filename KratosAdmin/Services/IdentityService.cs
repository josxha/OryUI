using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Model;

namespace KratosAdmin.Services;

public class IdentityService(ApiService apiService)
{
    public async Task<List<KratosIdentity>> ListIdentities()
    {
        return await apiService.IdentityApi.ListIdentitiesAsync();
    }

    public static string? GetTraitValueFromPath(KratosIdentity identity, List<string> path)
    {
        var traits = (JObject)identity.Traits;
        var jToken = traits[path.First()];
        foreach (var pathSection in path.Skip(1)) jToken = jToken?[pathSection];
        return jToken?.ToString();
    }
}