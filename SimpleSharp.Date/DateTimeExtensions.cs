using System;

namespace SimpleSharp.Date
{
    public static class DateTimeExtensions
    {
        private const int DAYS_IN_WEEK = 7;
        public static DateTime StartDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            DateTime firstSundayOfTheYear = jan1.AddDays(DayOfWeek.Sunday - jan1.DayOfWeek);

            var weekNum = weekOfYear - 1;
            return firstSundayOfTheYear.AddDays(weekNum * DAYS_IN_WEEK);
        }

        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
                diff += 7;
            return date.Date.AddDays(-1 * diff);
        }

        public static DateTime EndDateOfWeek(int year, int weekOfYear)
        {
            return StartDateOfWeek(year, weekOfYear).AddDays(DAYS_IN_WEEK - 1);
        }          

        public static DateTime ThisTimeYesterday(this DateTime dateTime)
        {
            return dateTime.Date.SubtractDays(1);
        }

        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
        }

        public static DateTime EndOfDay(this DateTime startOfDay)
        {
            return startOfDay.AddDays(1).SubtractMilliseconds(1);
        }

        public static DateRangeValue YesterdayUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var yesterday = Yesterday(offsetFromUTC, utcNow);
            var endOfYesterday = yesterday.EndOfDay();

            return new DateRangeValue(yesterday, endOfYesterday, offsetFromUTC);
        }

        public static DateRangeValue PastWeekUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var now = utcNow.HasValue ? utcNow.Value : DateTime.UtcNow;
            var nowInUserTime = now.Add(offsetFromUTC);

            var previousWeekStartDate = nowInUserTime.SubtractDays(7).Date.Add(offsetToUTC);
            var previousWeekEndDate = YesterdayUTC(offsetFromUTC, now).EndDate;

            return new DateRangeValue(previousWeekStartDate, previousWeekEndDate, offsetFromUTC);
        }

        public static DateRangeValue LastWeekUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var startOfLastWeek = GetStartOfWeek(1, offsetFromUTC, utcNow);
            var endOfLastWeek = GetEndOfWeek(1, offsetFromUTC, utcNow);
            return new DateRangeValue(startOfLastWeek, endOfLastWeek, offsetFromUTC);
        }

        public static DateRangeValue LastTwoWeeksUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var startOfLastWeek = GetStartOfWeek(2, offsetFromUTC, utcNow);
            var endOfLastWeek = GetEndOfWeek(1, offsetFromUTC, utcNow);
            return new DateRangeValue(startOfLastWeek, endOfLastWeek, offsetFromUTC);
        }

        public static DateRangeValue LastFourWeeksUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var startOfLastWeek = GetStartOfWeek(4, offsetFromUTC, utcNow);
            var endOfLastWeek = GetEndOfWeek(1, offsetFromUTC, utcNow);
            return new DateRangeValue(startOfLastWeek, endOfLastWeek, offsetFromUTC);
        }

        public static DateRangeValue MonthToDateUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var nowInUserTime = utcNow.HasValue ? utcNow.Value.Add(offsetFromUTC) : DateTime.UtcNow.Add(offsetFromUTC);
            
            var startOfMonthUtc = new DateTime(nowInUserTime.Year, nowInUserTime.Month, 1).Add(offsetToUTC);
            var endDate = nowInUserTime.Add(offsetToUTC);

            return new DateRangeValue(startOfMonthUtc, endDate, offsetFromUTC);
        }

        public static DateRangeValue LastMonthUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var nowInUserTime = utcNow.HasValue ? utcNow.Value.Add(offsetFromUTC) : DateTime.UtcNow.Add(offsetFromUTC);
            
            var startOfCurrentMonth = new DateTime(nowInUserTime.Year, nowInUserTime.Month, 1).Add(offsetToUTC);

            var startOfLastMonth = startOfCurrentMonth.SubtractMonths(1);
            var endOfLastMonth = startOfCurrentMonth.SubtractDays(1).EndOfDay();

            return new DateRangeValue(startOfLastMonth, endOfLastMonth, offsetFromUTC);
        }

        public static DateRangeValue LastYearUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var nowInUserTime = utcNow.HasValue ? utcNow.Value.Add(offsetFromUTC) : DateTime.UtcNow.Add(offsetFromUTC);
            
            var startOfCurrentYear = new DateTime(nowInUserTime.Year, 1, 1).Add(offsetToUTC);

            var startOfLastYear = startOfCurrentYear.SubtractYears(1);
            var endOfLastYear = startOfCurrentYear.SubtractDays(1).EndOfDay();

            return new DateRangeValue(startOfLastYear, endOfLastYear, offsetFromUTC);
        }

        public static DateRangeValue YearToDateUTC(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var nowInUserTime = utcNow.HasValue ? utcNow.Value.Add(offsetFromUTC) : DateTime.UtcNow.Add(offsetFromUTC);
            
            var startOfYearUtc = new DateTime(nowInUserTime.Year, 1, 1).Add(offsetToUTC);
            var endDate = nowInUserTime.Add(offsetToUTC);

            return new DateRangeValue(startOfYearUtc, endDate, offsetFromUTC);
        }

        private static DateTime Yesterday(TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();

            var nowInUserTime = utcNow.HasValue ? utcNow.Value.Add(offsetFromUTC) : DateTime.UtcNow.Add(offsetFromUTC);
            var yesterdayUTC = nowInUserTime.SubtractDays(1).Date.Add(offsetToUTC);

            return yesterdayUTC;
        }

        private static DateTime GetStartOfWeek(int weeksAgo, TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var todayInUserTime = utcNow.HasValue ? utcNow.Value.Date : DateTime.UtcNow.Add(offsetFromUTC).Date;

            var start = todayInUserTime.SubtractDays(7 * weeksAgo);
            while (start.DayOfWeek != DayOfWeek.Sunday)
            {
                start = start.SubtractDays(1);
            }
            return start.Add(offsetToUTC);
        }

        private static DateTime GetEndOfWeek(int weeksAgo, TimeSpan offsetFromUTC, DateTime? utcNow = null)
        {
            var offsetToUTC = offsetFromUTC.Negate();
            var todayInUserTime = utcNow.HasValue ? utcNow.Value.Date : DateTime.UtcNow.Add(offsetFromUTC).Date;

            var end = todayInUserTime.SubtractDays(7 * weeksAgo);
            while (end.DayOfWeek != DayOfWeek.Saturday)
            {
                end = end.AddDays(1);
            }
            return end.EndOfDay().Add(offsetToUTC);
        }

        public static int GetQuarter(this DateTime dateTime)
        {
            return ((dateTime.Month - 1) / 3) + 1;
        }

        public static DateTime SubtractMilliseconds(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");

            return date.AddMilliseconds(value * -1);
        }

        public static DateTime SubtractSeconds(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");

            return date.AddSeconds(value * -1);
        }

        public static DateTime SubtractMinutes(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");

            return date.AddMinutes(value * -1);
        }

        public static DateTime SubtractHours(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");

            return date.AddHours(value * -1);
        }

        public static DateTime SubtractDays(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");

            return date.AddDays(value * -1);
        }

        public static DateTime SubtractMonths(this DateTime date, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");

            return date.AddMonths(value * -1);
        }

        public static DateTime SubtractYears(this DateTime date, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", "value");
            
            return date.AddYears(value * -1);
        }
    }
}
