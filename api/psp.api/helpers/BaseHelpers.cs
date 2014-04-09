using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace psp.api.helpers
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

        #region Email Methods

        protected string EmailHTMLStart()
        {
            string cssstyle = @".maincontainer
            {
                width: 740px;
                float: left;
                clear:both;
                margin-bottom: 10px;
            }
            .maincontainer h2
            {
                font: 18px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;
                font-weight: bold;
                color: #D53476;
                padding-left: 5px;
                margin-bottom: 0px;
            }

            .maincontent
            {
                width: 100% ;
                padding: 5px 5px 5px 5px;
                border-top: solid 1px #eeeeee;
                border-right: solid 1px #cccccc;
                border-bottom: solid 1px #cccccc;
                border-left: solid 1px #eeeeee;
                background-color: #FdFdFd;
                font: 12px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;     
            }
            
            body
            {
                font: 12px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;
            }

            .maincontent span
            {
                color: #4B86D7;
                font-weight: bold;    
            }
            .style4 {
	            color: #ED217D;
	            font-weight: bold;
            }
            .style5 {color: #999999}
            a:hover
            {
                color: #D53476;
                text-decoration: underline;
                font-weight: bold;
                font: 11px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;
            }

            a
            {
                color: #0067B8;
                text-decoration: underline;
                font-weight: bold;    
                font: 11px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;
            }
                        
            ";
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<title> PrimeShine Automatic Email </title>");
            sb.Append("<link rel=\"stylesheet\" href=\"http://register.primeshine.com/css/styles.css\" type=\"text/css\" />");
            sb.Append("<style>");
            sb.Append(cssstyle);
            sb.Append("</style>");
            sb.Append("<script type='text/javascript' src='http://register.primeshine.com/public/scripts/typeface.js'></script><script type='text/javascript' src='http://register.primeshine.com/public/scripts/free_3_of_9_regular.typeface.js'></script>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<div id='wrapper'>");
            sb.Append("<div id='banner'>");
            sb.Append("</div>");
            sb.Append("<div class='contentplaceholder'>");
            return sb.ToString();
        }


        protected string EmailHTMLEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        //        protected string BirthdayEmailHTMLStart(DataRow row)
        //        {
        //            string cssstyle = @".maincontainer
        //            {
        //                width: 740px;
        //                float: left;
        //                clear:both;
        //                margin-bottom: 10px;
        //            }
        //            .maincontainer h2
        //            {
        //                font: 18px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;
        //                font-weight: bold;
        //                color: #D53476;
        //                padding-left: 5px;
        //                margin-bottom: 0px;
        //            }
        //
        //            .maincontent
        //            {
        //                width: 100% ;
        //                padding: 5px 5px 5px 5px;
        //                border-top: solid 1px #eeeeee;
        //                border-right: solid 1px #cccccc;
        //                border-bottom: solid 1px #cccccc;
        //                border-left: solid 1px #eeeeee;
        //                background-color: #FdFdFd;
        //                font: 12px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;     
        //            }
        //            
        //            body
        //            {
        //                font: 12px 'Trebuchet MS', 'Tahoma', 'Verdana', sans-serif;
        //            }
        //
        //            .maincontent span
        //            {
        //                color: #4B86D7;
        //                font-weight: bold;    
        //            }
        //            .style4 {
        //	            color: #ED217D;
        //	            font-weight: bold;
        //            }
        //            .style5 {color: #999999}
        //            a:hover
        //            {
        //                color: #D53476;
        //                text-decoration: underline;
        //                font-weight: bold;
        //                font: 14px 'Arial', 'Helvetica', sans-serif;
        //            }
        //
        //            a
        //            {
        //                color: #0067B8;
        //                text-decoration: underline;
        //                font-weight: bold;    
        //                font: 14px 'Arial', 'Helvetica', sans-serif;
        //            }
        //                        
        //            ";
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
        //            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        //            sb.Append("<head>");
        //            sb.Append("<title> PrimeShine Automatic Email </title>");
        //            sb.Append("<link rel=\"stylesheet\" href=\"http://register.primeshine.com/css/styles.css\" type=\"text/css\" />");
        //            sb.Append("<style>");
        //            sb.Append(cssstyle);
        //            sb.Append("</style>");
        //            sb.Append("<script type='text/javascript' src='http://register.primeshine.com/public/scripts/typeface.js'></script><script type='text/javascript' src='http://register.primeshine.com/public/scripts/free_3_of_9_regular.typeface.js'></script>");
        //            sb.Append("</head>");
        //            sb.Append("<body>");
        //            sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='700px'><tbody><tr><td colspan='3' height='132' width='700'><img src='http://www.neverboring.com/eBlast/PSX/psx-birthday-header.jpg' /></td></tr><tr><td height='181' width='188'><img src='http://www.neverboring.com/eBlast/PSX/psx-birthday-waves-top.jpg' /></td><td colspan='2' width='512' height='181'> <p style='font-family: Arial,Helvetica,sans-serif; color: rgb(0, 0, 0); font-size: 14px; font-weight:bold'>Happy Birthday from Prime Shine Car Wash. After making a wish and<br/> blowing out your candles, confirm your address below and we'll mail <br/>your     <span class='style4'>FREE BIRTHDAY WASH</span> coupon to your home.</p>    <br/>    <p style='font-family: Arial,Helvetica,sans-serif; color: rgb(0, 0, 0); font-size: 14px; font-weight:bold;'>&bull; If the information below is correct, click here to <a href='");
        //            sb.Append(ConfigurationSettings.AppSettings["VERIFYADDRESSURL"]);
        //            sb.Append("?correct=y&clientid=");
        //            sb.Append(row["clientid"].ToString());
        //            sb.Append("'>verify your address.</a>");
        //            sb.Append("<p style='font-family: Arial,Helvetica,sans-serif; color: rgb(0, 0, 0); font-size: 14px; font-weight:bold;'>&bull; If you need to make corrections, click here to <a href='");
        //            sb.Append(ConfigurationSettings.AppSettings["VERIFYADDRESSURL"]);
        //            sb.Append("?correct=n&clientid=");
        //            sb.Append(row["clientid"].ToString());
        //            sb.Append("'>update your address.</a></td></tr><tr><td height='232' width='188'><img src='http://www.neverboring.com/eBlast/PSX/psx-birthday-waves-bottom.jpg' /></td><td height='232' width='269'><img src='http://www.neverboring.com/eBlast/PSX/psx-birthday-cake-right.jpg' /></td><td height='232' width='243' valign='top'><p style='font-family: Arial,Helvetica,sans-serif; color: rgb(0, 0, 0); font-size: 14px; vertical-align:top; font-weight:bold;'>");
        //            return sb.ToString();
        //        }


        //        protected string BirthdayEmailHTMLEnd(DataRow row)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("</p></td></tr><tr><td colspan='3' height='57' width='700'><img src='http://www.neverboring.com/eBlast/PSX/psx-birthday-coupon.jpg' /></td></tr></tr></tbody></table>");
        //            sb.Append("</body>");
        //            sb.Append("</html>");
        //            return sb.ToString();
        //        }

        #endregion

    }
}