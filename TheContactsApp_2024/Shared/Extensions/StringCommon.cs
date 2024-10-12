namespace Shared.Extensions;

public static class StringCommon {
    public static bool IsNullOrEmptyOrWhiteSpace(string[] texts)
    {
        var length = texts.Length;
        if (length <= 0)
        {
            return true;
        }

        for (var i = 0; i < length; i++)
        {
            if (string.IsNullOrEmpty(texts[i]) || string.IsNullOrWhiteSpace(texts[i]))
            {
                return true;
            }
        }
        
        return false;
    }
}