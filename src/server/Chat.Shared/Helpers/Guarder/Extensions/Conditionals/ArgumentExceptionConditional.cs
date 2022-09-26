using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace System;

public static class ArgumentExceptionConditional
{
    public static T NotInRange<T>(this IInnerGuard _, T? src, T left, T right, 
        [CallerArgumentExpression("src")] string paramName = "")
        where T : IComparable
    {
        Guard.Against.Null(src);
        
        if (!src!.Between(left, right))
        {
            Guard.Throw.ArgumentException($"{paramName} must be in range: {left} - {right}");
        }
        
        return src!;
    }
}