using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psp.core.datahelpers
{
    public class BaseHelpers
    {
        protected DateTime GetLastDayOfMonth(DateTime dtDate)
        {
            // set return value to the last day of the month
            // for any date passed in to the method
            // create a datetime variable set to the passed in date
            DateTime dtTo = dtDate;
            // overshoot the date by a month
            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));
            // return the last day of the month
            return dtTo;
        }

        protected bool IsTodayMonday()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                return true;
            }
            return false;
        }

    }
}
