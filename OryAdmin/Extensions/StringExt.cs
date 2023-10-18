namespace OryAdmin.Extensions;

public static class StringExt
{
    public static string ToStringOrDash(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? "-" : value;
    }
}