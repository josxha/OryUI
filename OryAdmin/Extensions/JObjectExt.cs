using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace OryAdmin.Extensions;

public static class JObjectExt
{
    public static void UpdateValueWithSchema(this JObject json, JSchema schema, List<string> schemaPathSections,
        object? value)
    {
        JObject? innerJson = null;
        JObject obj; // prevents variable name shadowing
        foreach (var path in schemaPathSections[..^1])
        {
            obj = innerJson ?? json;
            if (!obj.ContainsKey(path)) obj.Add(path, new JObject());
            innerJson = (JObject?)obj.GetValue(path);
        }

        // handle last path
        var lastPath = schemaPathSections.Last();
        obj = innerJson ?? json;
        obj[lastPath] = new JValue(value);
    }
}