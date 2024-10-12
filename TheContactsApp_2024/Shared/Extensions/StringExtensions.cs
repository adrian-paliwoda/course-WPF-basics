namespace Shared.Extensions;

public static class StringExtensions
{
    public static bool IsValid(this string entryText)
    {
        return !string.IsNullOrEmpty(entryText)
               && !string.IsNullOrWhiteSpace(entryText);
    }

    
}