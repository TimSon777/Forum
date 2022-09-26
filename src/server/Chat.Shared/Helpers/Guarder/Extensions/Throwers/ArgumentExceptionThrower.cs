using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace System;

public static class ArgumentExceptionThrower
{
    public static void ArgumentException<T>(this IThrowerGuard _,
        T? src, 
        string? message = null,
        [CallerArgumentExpression("src")] string paramName = "")
    {
        if (src is null)
        {
            throw new ArgumentNullException(paramName);
        }

        throw new ArgumentException(message, paramName);
    }
}