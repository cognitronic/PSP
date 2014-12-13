using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using psp.api.Controllers;
using psp.api.Reports;
using System.Web.Http;
using psp.api.helpers;

namespace psp.notifications
{
    public class Program
    {
        public void RunVolumeReport()
        {
            var notification = new VolumeReport().BuildNotifications(new VolumeReport().BuildVolumeData(DateTime.Now.ToShortDateString()), DateTime.Now.ToShortDateString());
            new SendMailController().Post(notification);
            AuditService.SaveLog(new AuditLog
            {
                auditDate = DateTime.Now,
                message = "Scheduled volume report notifications sent",
                type = psp.core.ResourceStrings.Audit_Notification,
                name = "Volume Notification"
            });
        }

        public void RunGSRReport()
        {
            var notification = new GSRReport().BuildNotifications(new GSRReport().BuildNotificationData(DateTime.Now.ToShortDateString(), false), DateTime.Now.ToShortDateString());
            new SendMailController().Post(notification);
            AuditService.SaveLog(new AuditLog
            {
                auditDate = DateTime.Now,
                message = "Scheduled GSR report notifications sent",
                type = psp.core.ResourceStrings.Audit_Notification,
                name = "GSR Notification"
            });
        }

        public void RunRewashReport()
        {
            var notification = new RewashReport().BuildNotifications(DateTime.Now.ToShortDateString());
            if(notification != null)
            {
                new SendMailController().Post(notification);
            }
            AuditService.SaveLog(new AuditLog
            {
                auditDate = DateTime.Now,
                message = "Scheduled rewash report notifications sent",
                type = psp.core.ResourceStrings.Audit_Notification,
                name = "Rewash Notification"
            });
        }

        public void SaveGSRSnapshot()
        {
            new psp.api.Reports.GSRReport().BuildNotificationData(DateTime.Today.ToShortDateString(), true);
            AuditService.SaveLog(new AuditLog
            {
                auditDate = DateTime.Now,
                message = "Scheduled GSR save snapshot executed",
                type = psp.core.ResourceStrings.Audit_Notification,
                name = "GSR Notification"
            });
        }

        public static void Main(string[] args)
        {
            Program prog = new Program();
            switch (args[0])
            {
                case "volume":
                    prog.RunVolumeReport();
                    break;
                case "gsr":
                    prog.RunGSRReport();
                    break;
                case "rewash":
                    prog.RunRewashReport();
                    break;
                case "gsr_snapshot":
                    prog.SaveGSRSnapshot();
                    break;
            }
        }
    }
}
