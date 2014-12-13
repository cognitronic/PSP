using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using System.Configuration;

namespace psp.api.helpers
{
    public class NotificationTemplates
    {
        
        public static StringBuilder StandardNotificationHeader(string title, string date){
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
            sb.Append("<!DOCTYPE html>");
            sb.Append("<html><head>");
            sb.Append("<title> Prime Shine Automatic Email </title>");

            sb.Append("<style>");
            sb.Append(cssstyle);
            sb.Append("</style>");
            sb.Append("<script type='text/javascript' src='http://register.primeshine.com/public/scripts/typeface.js'></script><script type='text/javascript' src='http://register.primeshine.com/public/scripts/free_3_of_9_regular.typeface.js'></script>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<div style='background-color: #428BCA; border-color: #63A1D6; height: 50px; color: #ffffff; padding-left: 5px; padding-top:5px;' >");
            sb.Append("<h3>");
            sb.Append(title);
            sb.Append(" - ");
            sb.Append(date);
            sb.Append("</h3></div>");
            sb.Append("<div id='wrapper'>");
            sb.Append("<div id='banner'>");
            sb.Append("</div>");
            sb.Append("<div class='contentplaceholder'>");
            return sb;
        }

        public static string StandardNotificationFooter(StringBuilder sb)
        {
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }

        public static string BirthdayEmailHTMLStart(string clientID)
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
                background-color: #ffffff;
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
                font: 14px 'Arial', 'Helvetica', sans-serif;
            }

            a
            {
                color: #0067B8;
                text-decoration: underline;
                font-weight: bold;    
                font: 14px 'Arial', 'Helvetica', sans-serif;
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

            var cc = new CouponCodeRepository().GetByCouponByStatus("birthday", false).First<CouponCode>(); //new CouponCodeServices().GetCouponCodeByCouponID(ConfigurationManager.AppSettings["BIRTHDAYEMAILCOUPONID"]);
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<a href='");
            sb.Append(ConfigurationManager.AppSettings["BIRTHDAYCOUPONURL"]);
            sb.Append("?cid=");
            sb.Append(cc.coupon);
            sb.Append("&c=");
            sb.Append(cc.codetext);
            sb.Append("'><img src='");
            sb.Append(ConfigurationManager.AppSettings["BIRTHDAYCOUPONNOTIFICATIONIMAGE"]);
            sb.Append("' alt='Happy Birthday From Prime Shine!' /></a>");
            cc.isassigned = true;
            new CouponCodeRepository().Save(cc);
            return sb.ToString();
        }

        public static string BirthdayEmailHTMLEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</body>");
            sb.Append("</html>");
            return sb.ToString();
        }
    }
}