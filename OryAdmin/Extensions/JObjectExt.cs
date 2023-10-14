using Newtonsoft.Json.Linq;
using OryAdmin.Models;

namespace OryAdmin.Extensions;

public static class JObjectExt
{
    public static void UpdateValueWithSchema(this JObject json, TraitsSchemaData schema, object? value)
    {
        JObject? innerJson = null;
        JObject obj; // prevents variable name shadowing
        foreach (var path in schema.Path[..^1])
        {
            obj = innerJson ?? json;
            if (!obj.ContainsKey(path)) obj.Add(path, new JObject());
            innerJson = (JObject?)obj.GetValue(path);
        }

        // handle last path
        var lastPath = schema.Path.Last();
        obj = innerJson ?? json;
        obj[lastPath] = new JValue(value);
    }
}