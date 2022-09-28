using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace System;

public static class ArgumentNullExceptionConditional
{
    public static T Null<T>(this IInnerGuard _,
        T? src,
        [CallerArgumentExpression("src")] string paramName = "")
    {
        if (src is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return src;
    }
}