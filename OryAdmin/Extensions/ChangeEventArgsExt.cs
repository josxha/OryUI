using Microsoft.AspNetCore.Components;

namespace OryAdmin.Extensions;

public static class ChangeEventArgsExt
{
    public static List<string> MultilineToList(this ChangeEventArgs args)
    {
        return args.Value?.ToString()?.SplitByLine() ?? [];
    }
}