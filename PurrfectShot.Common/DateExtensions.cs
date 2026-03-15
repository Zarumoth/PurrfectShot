using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Common
{
    public static class DateFormatHelpers
    {
        private static readonly CultureInfo BgCulture = new CultureInfo("bg-BG");

        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string ToBulgarianDateString(this DateTime date)
        {
            string month = BgCulture.DateTimeFormat.GetMonthName(date.Month).ToTitleCase();
            return $"{date.Day:D2} {month} {date.Year}";
        }

        public static string ToBulgarianMonthName(this int monthNumber)
        {
            if (monthNumber < 1 || monthNumber > 12) return string.Empty;
            return BgCulture.DateTimeFormat.GetMonthName(monthNumber).ToTitleCase();
        }
    }
}
