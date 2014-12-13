using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using psp.core.domain;
using psp.api.Controllers;
using System.Text;
using psp.api.helpers;
using psp.api.Reports.domain;
using MongoDB.Bson;
using psp.repository.mongo.Repositories;

namespace psp.api.Reports
{
    public class RewashReport
    {
        public string BuildRewashData(string gsrDate)
        {
            StringBuilder sb = new StringBuilder();
            bool bHasData = false;
            sb.Append("<table style='width: 100%;'>");
            var sites = new SiteRepository().GetAll();
            foreach(var site in sites)
            {
                try
                {
                    var sitewatchData = new SiteWatch().GetSiteWatchRewashData(site.sitewatchid.ToString(), gsrDate);
                    if(sitewatchData != null && sitewatchData.Count > 0)
                    {
                        decimal total = 0;
                        decimal carswashed = 0;
                        decimal retval = 0;
                        var washlinkData = new WashLink().WashLinkWashTotalsBySiteDate(site, DateTime.Parse(gsrDate), "", "");
                        if(washlinkData != null)
                        {
                            carswashed = washlinkData.primeshinewash +
                                washlinkData.protexwash +
                                washlinkData.premierwash;
                            foreach(var item in sitewatchData)
                            {
                                if(!string.IsNullOrEmpty(item.total))
                                {
                                    total += Convert.ToInt16(item.total);
                                }
                            }
                            if(carswashed > 0)
                            {
                                retval = ((total / carswashed) * 100);
                                if (Math.Round(retval) >= 2)
                                {
                                    sb.Append("<tr><td>");
                                    sb.Append(site.location);
                                    sb.Append(" #");
                                    sb.Append(site.sitewatchid);
                                    sb.Append(" </td><td>Rewash#: <font color='#D53476'>");
                                    sb.Append(total.ToString());
                                    sb.Append("</font></td><td>Cars: <font color='#D53476'>");
                                    sb.Append(carswashed.ToString());
                                    sb.Append("</font></td><td>Percentage: <font color='#D53476'>");
                                    sb.Append(Math.Round(retval).ToString());
                                    sb.Append("%</font></td></tr>");
                                    bHasData = true;
                                }
                            }
                        }
                    }
                }
                catch(Exception exc)
                {
                    AuditService.SaveLog(new AuditLog
                    {
                        auditDate = DateTime.Now,
                        message = "Could not complete rewash report for " + site.name + ".  The error thrown is: " + exc.Message,
                        type = psp.core.ResourceStrings.Audit_Notification,
                        name = "Rewash Notification"
                    });
                }
            }
            sb.Append("</table>");
            if (bHasData)
            {
                return sb.ToString();
            }
            else
            {
                return "false";
            }
        }

        public NotificationMessage BuildNotifications(string date)
        {
            string sRewashData = BuildRewashData(date);
            if (!sRewashData.Equals("false"))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(NotificationTemplates.StandardNotificationHeader("Rewash Alert", date));
                    sb.Append("<div class=");
                    sb.Append("maincontainer");
                    sb.Append(">");
                    sb.Append("<h2><span>PrimeShine Daily Rewash Alerts For: ");
                    sb.Append(date);
                    sb.Append("</span></h2>");
                    sb.Append("<div class=");
                    sb.Append("maincontent");
                    sb.Append(">");
                    sb.Append(sRewashData);
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("</div>");

                    var notification = new NotificationsController().GetByName("Rewash_Alert");
                    var message = new NotificationMessage();
                    foreach (var email in notification.recipients)
                    {
                        message.ToEmails.Add(email);
                    }
                    foreach (var bcc in notification.bccemails)
                    {
                        message.Bccs.Add(bcc);
                    }
                    message.FromEmail = notification.fromemail;
                    message.Subject = notification.subject.Replace("!!date!!", date);
                    message.MessageBody = NotificationTemplates.StandardNotificationFooter(sb);
                    return message;

                }
                catch (Exception e)
                {
                    AuditService.SaveLog(new AuditLog
                    {
                        auditDate = DateTime.Now,
                        message = "Could not construct rewash notification.  The error thrown is: " + e.Message,
                        type = psp.core.ResourceStrings.Audit_Notification,
                        name = "Rewash Notification"
                    });
                    return null;
                }
            }
            return null;
        }
    }
}