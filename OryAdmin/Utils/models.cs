namespace OryAdmin.Utils;

public class PaginationTokens(string? first, string? next)
{
    public string? First { get; init; } = first;
    public string? Next { get; init; } = next;

    public override string ToString()
    {
        return $"PaginationTokens(First: {First}, Next: {Next})";
    }
}