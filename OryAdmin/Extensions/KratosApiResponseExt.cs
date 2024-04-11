using Ory.Kratos.Client.Client;
using OryAdmin.Utils;

namespace OryAdmin.Extensions;

public static class KratosApiResponseExt
{
    public static PaginationTokens PaginationTokens(this Multimap<string, string> headers)
    {
        // </admin/identities?page_size=1&page_token=00000000-0000-0000-0000-000000000000>; rel="first"
        // </admin/identities?page_size=1&page_token=1302cdef-30e6-490c-abec-0b6d3406158b>; rel="next"
        string? first = null;
        string? next = null;
        foreach (var entry in headers["Link"])
        {
            if (string.IsNullOrWhiteSpace(entry)) continue;
            var sections = entry.Split("; ");
            var uriRaw = sections[0].Substring(1, sections[0].Length - 2);
            switch (sections[1])
            {
                case "rel=\"first\"":
                    first = uriRaw;
                    break;
                case "rel=\"next\"":
                    next = uriRaw;
                    break;
            }
        }

        return new PaginationTokens(first, next);
    }
}