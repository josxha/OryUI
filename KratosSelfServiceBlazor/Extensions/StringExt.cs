namespace KratosSelfServiceBlazor.Extensions;

public static class StringExt
{
    /// <summary>
    ///     Removes a trailing slash from a string. Does nothing if the string doesn't has a trailing slash symbol.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string RemoveTrailingSlash(this string input)
    {
        return input.EndsWith('/') ? input[..^1] : input;
    }
}