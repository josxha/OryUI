using System.Web;

namespace KratosSelfService.Extensions;

public static class StringDictExt
{
    public static string EncodeQueryString(this Dictionary<string, string?> query)
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        foreach (var (key, value) in query)
            queryString.Add(key, value ?? "");
        return queryString.ToString() ?? "";
    }
}