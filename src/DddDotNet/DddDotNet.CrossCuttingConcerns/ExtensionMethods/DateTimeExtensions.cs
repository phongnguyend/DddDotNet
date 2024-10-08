using System;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods;

public static class DateTimeExtensions
{
    public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public static DateTime GetLastDayOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
    }
}
