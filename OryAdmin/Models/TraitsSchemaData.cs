using Newtonsoft.Json.Schema;

namespace KratosAdmin.Models;

public class TraitsSchemaData(JSchema schema, List<string> path)
{
    public readonly JSchema Schema = schema;
    public List<string> Path = path;

    public override string ToString()
    {
        return Schema.ToString();
    }
}