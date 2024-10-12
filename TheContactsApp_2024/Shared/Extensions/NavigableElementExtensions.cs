using Microsoft.Maui.Controls;

namespace Shared.Extensions;

public static class NavigableElementExtensions
{
    public static void SetStylesToError(this NavigableElement element, ResourceDictionary resource,
        string errorStyleName = "EntryError")
    {
        if (resource.TryGetValue(errorStyleName, out var styleResource) && styleResource is Style style)
        {
            element.Style = style;
        }
    }

    public static void SetStylesToValid(this NavigableElement element, ResourceDictionary resource,
        string validStyleName = "EntryValid")
    {
        if (resource.TryGetValue(validStyleName, out var styleResource) && styleResource is Style style)
        {
            element.Style = style;
        }
    }
}