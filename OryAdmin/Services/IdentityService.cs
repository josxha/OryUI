using Newtonsoft.Json.Linq;
using Ory.Kratos.Client.Model;

namespace OryAdmin.Services;

public class IdentityService(ApiService apiService)
{
    public async Task<List<KratosIdentity>> ListIdentities()
    {
        return await apiService.KratosIdentity.ListIdentitiesAsync();
    }

    public static string? GetTraitValueFromPath(JObject traits, List<string> path)
    {
        var jToken = traits[path.First()];
        foreach (var pathSection in path.Skip(1)) jToken = jToken?[pathSection];
        return jToken?.ToString();
    }
}