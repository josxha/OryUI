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
}