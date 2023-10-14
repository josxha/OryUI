using KratosAdmin.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace KratosAdmin.Services;

public class IdentitySchemaService(ApiService apiService)
{
    private readonly Dictionary<string, JObject> _schemaCache = new();

    public async Task<JObject> GetById(string schemaId)
    {
        // check if schema object is cached
        if (_schemaCache.TryGetValue(schemaId, out var schema))
            return schema;
        // request and cache new schema object
        var newSchema = await apiService.KratosIdentity.GetIdentitySchemaAsync(schemaId);
        _schemaCache[schemaId] = (JObject)newSchema;
        return _schemaCache[schemaId];
    }

    public async Task<List<string>> ListIds()
    {
        var schemas = await apiService.KratosIdentity.ListIdentitySchemasAsync();
        foreach (var container in schemas) _schemaCache[container.Id] = (JObject)container.Schema;
        return schemas.Select(container => container.Id).ToList();
    }

    public async Task<List<TraitsSchemaData>> GetTraitSchemas(string schemaId)
    {
        var schemaObject = await GetById(schemaId);
        var schema = JSchema.Parse(schemaObject.ToString());
        var traits = schema.Properties["traits"].Properties;
        return FlattenTraits(traits, new List<string>());
    }

    private static List<TraitsSchemaData> FlattenTraits(IDictionary<string, JSchema> traits, List<string> path)
    {
        var list = new List<TraitsSchemaData>();
        foreach (var (traitKey, trait) in traits)
        {
            var newPath = new List<string>(path) { traitKey };
            switch (trait.Type)
            {
                case JSchemaType.String:
                    list.Add(new TraitsSchemaData(trait, newPath));
                    break;
                case JSchemaType.Object:
                    list.AddRange(FlattenTraits(trait.Properties, newPath));
                    break;
                case JSchemaType.Array:
                    // TODO support arrays
                    break;
                default:
                    list.Add(new TraitsSchemaData(trait, newPath));
                    break;
            }
        }

        return list;
    }
}