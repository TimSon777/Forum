// ReSharper disable once CheckNamespace
namespace System;

public static class Guard
{
    public static IInnerGuard Against => new InnerGuard();
    public static IThrowerGuard Throw => new ThrowerGuard();
}