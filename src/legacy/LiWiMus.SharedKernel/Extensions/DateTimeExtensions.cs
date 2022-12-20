namespace LiWiMus.SharedKernel.Extensions;

public static class DateTimeExtensions
{
    public static DateTime PlusYears(this DateTime dateTime, int year = 3) => dateTime + TimeSpan.FromDays(year);
}