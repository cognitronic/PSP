using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psp.core.helpers
{
    public class DateUtilities
    {
        public static List<DateTime> GetDateByWeek(DateTime startDate, int weekNumOfMonth, int dayOfWeek, int yearsToQuery)
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime dt = new DateTime(startDate.Year, startDate.Month, 1);

            for (int i = 0; i < yearsToQuery; i++)
            {
                // loop until we find our first match day of the week
                while ((int)dt.DayOfWeek != dayOfWeek)
                {
                    dt = dt.AddDays(1);
                }
                // Complete the gap to the nth week
                dt = dt.AddDays((weekNumOfMonth - 1) * 7);
                if (dt.Month == startDate.Month)
                {
                    dates.Add(dt);
                }
                dt = new DateTime(dt.Year -1, startDate.Month, 1);
            }
            return dates;
        }

        public static int GetWeekOfMonthByDate(DateTime date)
        {
            return (date.Day - 1) / 7 + 1;
        }

        public static Dictionary<string, DateTime> Today()
        {
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", DateTime.Now);
            dict.Add("EndDate", DateTime.Now);
            return dict;
        }

        public static Dictionary<string, DateTime> Yesterday()
        {
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", DateTime.Now.AddDays(-1));
            dict.Add("EndDate", DateTime.Now.AddDays(-1));
            return dict;
        }

        public static Dictionary<string, DateTime> ThisWeek()
        {
            int days = DateTime.Now.DayOfWeek - DayOfWeek.Sunday;
            DateTime startDate = DateTime.Now.AddDays(-(days));
            DateTime endDate = startDate.AddDays(6);
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> LastWeek()
        {
            int days = DateTime.Now.AddDays(-7).DayOfWeek - DayOfWeek.Sunday;
            DateTime startDate = DateTime.Now.AddDays(-(days + 7));
            DateTime endDate = startDate.AddDays(6);
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> ThisMonth()
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var startDate = month;
            var endDate = month.AddMonths(1).AddDays(-1);
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> LastMonth()
        {
            var today = DateTime.Today.AddMonths(-1);
            var month = new DateTime(today.Year, today.Month, 1);
            var startDate = month;
            var endDate = month.AddMonths(1).AddDays(-1);
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> ThisQuarter()
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            DateTime startDate;
            DateTime endDate;
            if(today.Month <= 3)
            {
                startDate = new DateTime(today.Year, 1, 1);
                endDate = new DateTime(today.Year, 4, 1).AddDays(-1);
            }
            else if (today.Month >= 4 && today.Month <= 6)
            {
                startDate = new DateTime(today.Year, 4, 1);
                endDate = new DateTime(today.Year, 7, 1).AddDays(-1);
            }
            else if (today.Month >= 7 && today.Month <= 9)
            {
                startDate = new DateTime(today.Year, 7, 1);
                endDate = new DateTime(today.Year, 10, 1).AddDays(-1);
            }
            else
            {
                startDate = new DateTime(today.Year, 10, 1);
                endDate = new DateTime(today.AddYears(1).Year, 1, 1).AddDays(-1);
            }

            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> LastQuarter()
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            DateTime startDate;
            DateTime endDate;
            if (today.Month <= 3)
            {
                startDate = new DateTime(today.AddYears(1).Year, 10, 1);
                endDate = new DateTime(today.Year, 1, 1).AddDays(-1);
            }
            else if (today.Month >= 4 && today.Month <= 6)
            {
                startDate = new DateTime(today.Year, 1, 1);
                endDate = new DateTime(today.Year, 3, 1).AddDays(-1);
            }
            else if (today.Month >= 7 && today.Month <= 9)
            {
                startDate = new DateTime(today.Year, 4, 1);
                endDate = new DateTime(today.Year, 6, 1).AddDays(-1);
            }
            else
            {
                startDate = new DateTime(today.Year, 7, 1);
                endDate = new DateTime(today.Year, 9, 1).AddDays(-1);
            }

            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> YearToDate()
        {
            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, 1, 1);
            var endDate = today;
            var dict = new Dictionary<string, DateTime>();
            dict.Add("StartDate", startDate);
            dict.Add("EndDate", endDate);
            return dict;
        }

        public static Dictionary<string, DateTime> GetRangesByName(string name)
        {
            if (name.Contains("CUSTOM_"))
            {
                name = name.Replace("CUSTOM_", "");
                var dates = name.Split('_');
                var dict = new Dictionary<string, DateTime>();
                if(dates.Length == 2)
                {
                    dict.Add("StartDate", DateTime.Parse(dates[0]));
                    dict.Add("EndDate", DateTime.Parse(dates[1]));
                }
                else
                {
                    dict.Add("StartDate", DateTime.Now);
                    dict.Add("EndDate", DateTime.Now);
                }
                return dict;
            }
            switch (name)
            {
                case "Today":
                case"today":
                    return Today();
                case "Yesterday":
                case"yesterday":
                    return Yesterday();
                case "ThisWeek":
                case "thisweek":
                    return ThisWeek();
                case "LastWeek":
                case "lastweek":
                    return LastWeek();
                case "ThisMonth":
                case "thismonth":
                    return ThisMonth();
                case "LastMonth":
                case "lastmonth":
                    return LastMonth();
                case "ThisQuarter":
                case "thisquarter":
                    return ThisQuarter();
                case "LastQuarter":
                case "lastquarter":
                    return LastQuarter();
                case "YearToDate":
                case "yeartodate":
                    return YearToDate();
            }
            return null;
        }

        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }
            return null;
        }
    }
}
