using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace System;

public static class Result
{
    public static Result<T> WithValue<T>(T value)
    {
        return Result<T>.WithValue(value);
    }
}

public sealed class Result<T>
{
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool Succeeded { get; private init; }

    public T? Value { get; private init; }
    
    public string? Error { get; private init; }

    private Result()
    { }

    public static Result<T> WithValue(T value)
    {
        return new Result<T>
        {
            Value = value,
            Succeeded = true
        };
    }

    public static Result<T> WithError(string error)
    {
        return new Result<T>
        {
            Succeeded = false,
            Error = error
        };
    }
}