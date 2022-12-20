namespace LiWiMus.SharedKernel.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string src) => string.IsNullOrEmpty(src);
    public static int ToInt(this string src) => int.Parse(src);
}