using Newtonsoft.Json.Schema;

namespace KratosSelfServiceBlazor.Services;

public class IdentitySchemaService
{
    private readonly HttpClient _httpClient = new();
    private readonly Dictionary<string, JSchema> _schemaCache = new();

    public async Task<JSchema> FetchSchema(string schemaId, string schemaUri)
    {
        // check if schema object is cached
        if (_schemaCache.TryGetValue(schemaId, out var schema))
            return schema;
        var response = await _httpClient.GetStringAsync(schemaUri);
        // request and cache new schema object
        _schemaCache[schemaId] = JSchema.Parse(response);
        return _schemaCache[schemaId];
    }

    public static Dictionary<List<string>, JSchema> GetTraits(JSchema schema)
    {
        var traits = schema.Properties["traits"].Properties;
        return FlattenTraits(traits, []);
    }

    private static Dictionary<List<string>, JSchema> FlattenTraits(IDictionary<string, JSchema> traits,
        IReadOnlyCollection<string> pathSections)
    {
        var map = new Dictionary<List<string>, JSchema>();
        foreach (var (traitKey, trait) in traits)
        {
            var newPathSections = new List<string>(pathSections) { traitKey };
            switch (trait.Type)
            {
                case JSchemaType.Object:
                    foreach (var entry in FlattenTraits(trait.Properties, newPathSections))
                        map[entry.Key] = entry.Value;
                    break;
                case JSchemaType.Array:
                    // TODO support arrays
                    break;
                default: // string, etc.
                    map[newPathSections] = trait;
                    break;
            }
        }

        return map;
    }
}