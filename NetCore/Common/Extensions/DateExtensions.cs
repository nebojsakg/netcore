using System;

namespace Common.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        /// Returns range of dates from (date - numOfDays) to date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="numOfDays"></param>
        /// <returns></returns>
        public static DateRange GetLastNDaysRange(this DateTime date, int numOfDays)
        {
            return new DateRange(date.Subtract(new TimeSpan(numOfDays, 0, 0, 0)), date);
        }

        /// <summary>
        /// Returns month range for given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange GetMonthRange(this DateTime date)
        {
            return new DateRange(new DateTime(date.Year, date.Month, 1), date);
        }

        /// <summary>
        /// Return week range for given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange GetWeekRange(this DateTime date)
        {
            return new DateRange(date.AddDays(-(int)date.DayOfWeek), date);
        }
    }

    public class DateRange
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public DateRange(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
