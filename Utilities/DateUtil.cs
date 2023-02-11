using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class DateUtil
    {
        private static PersianCalendar PC = new PersianCalendar();
        public static DateTime ToGregorian(int year, int monthNo, int day, string? monthStr = null)
        {
            var month = monthNo > 0
                ? monthNo
                : !string.IsNullOrEmpty(monthStr) 
                ? MonthNameToNumber(monthStr) 
                : throw new ArgumentException("");

            return PC.ToDateTime(year: year, month: month, day: day, 12, 0, 0, 0);
        }

        public static DateTime AddPersianMonths(this DateTime date ,int month)
        {
            return PC.AddMonths(date, month);
        }

        private static int MonthNameToNumber(string monthStr)
        {
            var dic = new Dictionary<string, int>()
            {
                {"فروردین", 1 },
                {"اردیبهشت", 2 },
                {"خرداد", 3 },
                {"تیر", 4 },
                {"مرداد", 5 },
                {"شهریور", 6 },
                {"مهر", 7 },
                {"آبان", 8 },
                {"آذر", 9 },
                {"دی", 10 },
                {"بهمن", 11 },
                {"اسفند", 12 },
            };
            return dic[monthStr];
        }

    }
}
