// ReSharper disable once CheckNamespace
namespace System;

// ReSharper disable once InconsistentNaming
public static class IComparableExtensions
{
    public static bool Between(this IComparable src, IComparable left, IComparable right)
    {
        return src.CompareTo(left) >= 0 && src.CompareTo(right) <= 0;
    }
}