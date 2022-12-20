using LiWiMus.SharedKernel.Exceptions;

namespace LiWiMus.SharedKernel.Extensions;

// ReSharper disable once InconsistentNaming
public static class IEnumerableExtensions
{
    public static void Foreach<T>(this IEnumerable<T> source, Action<T> selector)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (selector == null)
        {
            throw new ArgumentNullException(nameof(selector));
        }

        foreach (var e in source)
        {
            selector(e);
        }
    }

    public static string Join(this IEnumerable<string> source, string joinBy = " ", JoinSettings joinSettings = JoinSettings.None)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        
        ExceptionHelper.ThrowWhenArgumentNull(joinBy, nameof(joinBy));

        var src = joinSettings switch
        {
            JoinSettings.None => source,
            JoinSettings.IgnoreEmptyOrNull => source.Where(e => !e.IsNullOrEmpty()),
            _ => throw new ArgumentOutOfRangeException(nameof(joinSettings), joinSettings, null)
        };
        
        return string.Join(joinBy, src);
    }
}