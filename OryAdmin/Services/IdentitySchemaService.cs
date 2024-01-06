using Newtonsoft.Json.Schema;

namespace OryAdmin.Services;

public class IdentitySchemaService(ApiService apiService)
{
    private readonly Dictionary<string, JSchema> _schemaCache = new();

    public async Task<JSchema> FetchById(string schemaId)
    {
        // check if schema object is cached
        if (_schemaCache.TryGetValue(schemaId, out var schema))
            return schema;
        // request and cache new schema object
        var newSchema = await apiService.KratosIdentity.GetIdentitySchemaAsync(schemaId);
        _schemaCache[schemaId] = JSchema.Parse(newSchema.ToString()!);
        return _schemaCache[schemaId];
    }

    public async Task<List<string>> ListIds()
    {
        var schemas = await apiService.KratosIdentity.ListIdentitySchemasAsync();
        foreach (var container in schemas) _schemaCache[container.Id] = JSchema.Parse(container.Schema.ToString()!);
        return schemas.Select(container => container.Id).ToList();
    }

    public async Task<Dictionary<List<string>, JSchema>> GetTraits(string schemaId)
    {
        var schema = await FetchById(schemaId);
        return GetTraits(schema);
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