using System.Web;
using OryAdmin.Utils;

namespace OryAdmin.Extensions;

public static class StringExt
{
    public static string ToStringOrDash(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? "-" : value;
    }

    public static List<string> SplitByLine(this string value)
    {
        return value
            .Split("\n")
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();
    }
    
    
    public static PaginationTokens PaginationTokens(this string linkHeader)
    {
        // </admin/identities?page_size=1&page_token=00000000-0000-0000-0000-000000000000>; rel="first"
        // </admin/identities?page_size=1&page_token=1302cdef-30e6-490c-abec-0b6d3406158b>; rel="next"
        string? first = null;
        string? next = null;
        foreach (var entry in linkHeader.Split(",") ?? [])
        {
            if (string.IsNullOrWhiteSpace(entry)) continue;
            var sections = entry.Split("; ");
            var uriRaw = sections[0].Substring(1, sections[0].Length - 2);
            var queryString = uriRaw.Split("?").Last();
            var queryParams = HttpUtility.ParseQueryString($"?{queryString}");
            var pageToken = queryParams.Get("page_token");
            switch (sections[1])
            {
                case "rel=\"first\"":
                    first = pageToken;
                    break;
                case "rel=\"next\"":
                    next = pageToken;
                    break;
            }
        }

        return new PaginationTokens(first, next);
    }
}