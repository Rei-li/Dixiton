using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Dixiton.Common.Helpers
{
    public static class DateHelper
    {
        private const string MonthFormat = "MM";
        private const string MonthYearFormatPattern = "MM{0}yyyy";
        private const string DateInvariantPatternRu = "dd.MM.yyyy";
        private const string DateInvariantPatternEn = "MM/dd/yyyy";
        private const string MonthYearPatternRu = "MM.yyyy";
        private const string MonthYearPatternEn = "MM/yyyy";

        private static string MonthYearFormat
        {
            get
            {
                var separator = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator;
                var pattern = string.Format(MonthYearFormatPattern, separator);
                return pattern;
            }
        }
        public static string DateFormat
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator == "/")
                    return DateInvariantPatternEn;
                return DateInvariantPatternRu;
            }
        }

        /// <summary>
        /// Method for getting first day in month
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>First day in month</returns>
        public static DateTime GetFirstDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Method for getting last day in month
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Last day in month</returns>
        public static DateTime GetLastDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Method for getting previous month
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Previous mouth</returns>
        public static DateTime GetPreviousMonth(this DateTime date)
        {
            return date.AddMonths(-1);
        }

        public static DateTime GetNextMonth(this DateTime date)
        {
            return date.AddMonths(1);
        }

        /// <summary>
        /// Method for getting previous year
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Previous year</returns>
        public static DateTime GetPreviousYear(this DateTime date)
        {
            return date.AddYears(-1);
        }

        /// <summary>
        /// Returns first and last dates of the month specified in date param
        /// </summary>
        /// <param name="date">Date to get month from</param>
        /// <returns>Pair of first and last days of month</returns>
        public static Tuple<DateTime, DateTime> GetMonthStartAndEnd(this DateTime date)
        {
            return new Tuple<DateTime, DateTime>(date.GetFirstDay(), date.GetLastDay());
        }


        public static string TryParseInvariant(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            return dateTime.Value.ToString(DateInvariantPatternRu);
        }
        
        public static string ParseDate(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            
            return dateTime.Value.ToString(DateFormat);
        }
        public static DateTime? ParseDate(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
                return null;
            
            dateTime = dateTime.Split(' ')[0];

            DateTime result;
            if (DateTime.TryParseExact(dateTime,
                new[] {DateInvariantPatternRu, DateInvariantPatternEn, MonthYearPatternRu, MonthYearPatternEn},
                CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            return null;
        }

        public static DateTime? ParseDateTimeOrMonth(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
                return null;
            if (dateTime.Length == 7)
                return ParseMonthDate(dateTime);

            return ParseDate(dateTime);
        }

        #region Month

        public static bool IsValidMonthDate(string dateString)
        {
            DateTime date;

            return TryParseMonthDate(dateString, out date);
        }

        public static bool TryParseMonthDate(string dateString, out DateTime date)
        {
            bool isValidFormat = false;
            date = DateTime.MinValue;

            if (!string.IsNullOrEmpty(dateString))
            {
                if (DateTime.TryParseExact(dateString, new[] { MonthFormat, MonthYearFormat },
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    isValidFormat = true;
                }
            }

            return isValidFormat;
        }

        public static DateTime ParseMonthDate(string dateString)
        {
            return DateTime.ParseExact(dateString, new[] { MonthFormat, MonthYearFormat }, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string ToMonthYearString(this DateTime date)
        {
            return date.ToString(MonthYearFormat);
        }

        public static string ToMonthYearString(DateTime? date)
        {
            if (date == null)
                return string.Empty;
            return date.Value.ToString(MonthYearFormat);
        }
        #endregion Month



        #region Number of working days per month
      
        public static int GetNumberOfWorkingDay(DateTime dateTime)
        {
            var total = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var firstDay = dateTime.GetFirstDay();
            var lastDay = dateTime.GetLastDay();
            var d1 = (6 + (int)firstDay.DayOfWeek) % 7;
            var d2 = (6 + (int)lastDay.DayOfWeek) % 7;

            var w1 = total + d1 + (6 - d2);
            var weeks = w1 / 7;
            var d4 = 4 - d2;
            var wd = w1 - weeks * 2 - (d1 < 6 ? d1 : 5) - (d4 > 0 ? d4 : 0);
            return wd;
        }

        public static int GetAdditionNumberOfFest(DateTime dateTime)
        {
            var days = 0;
            var dic = GetFestDictionary();
            if (dic.ContainsKey(dateTime.Month))
            {
                foreach (var item in dic[dateTime.Month])
                {
                    if (!IsDayOff(dateTime.Year, dateTime.Month, item))
                        days++;
                }
            }
            return days;
        }

        private static Dictionary<int, int[]> GetFestDictionary()
        {
            //по умолчанию используем словарь с нашими праздниками.
            return _feastsBlr;
        }

        //http://www.tamby.info/rw/calendar.htm
        private static readonly Dictionary<int, int[]> _feastsBlr = new Dictionary<int, int[]>
        {
            {1, new []{1,7}},   //Новый год | православное Рождество
            {3, new []{8}},     //День женщин
            {5, new []{1,9}},   //Праздник труда | День Победы
            {7, new []{3}},     //День Республики
            {11, new []{7}},    // День Октябрьской революции
            {12, new []{25}},   // католическое Рождество
        };

        private static bool IsDayOff(int year, int month, int day)
        {
            var dateTime = new DateTime(year, month, day);
            var dayOfWeek = dateTime.DayOfWeek;
            return (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Wednesday);
        }

        #endregion Number of working days per month
    }
}
