using Newtonsoft.Json.Linq;

namespace OryAdmin.Extensions;

public static class TraitsExt
{
    public static string? GetTraitValueFromPath(this JObject traits, List<string> path)
    {
        var jToken = traits[path.First()];
        foreach (var pathSection in path.Skip(1)) jToken = jToken?[pathSection];
        return jToken?.ToString();
    }
}