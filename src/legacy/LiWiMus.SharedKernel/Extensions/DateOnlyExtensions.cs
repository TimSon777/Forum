namespace LiWiMus.SharedKernel.Extensions;

public static class DateOnlyExtensions
{
    public static DateOnly Now => DateOnly.FromDateTime(DateTime.Now);
}