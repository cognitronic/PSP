using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using psp.api.helpers;
using System.Net.Mail;
using System.Configuration;

namespace psp.api.Controllers
{
    [RoutePrefix("api/sendmail")]
    public class SendMailController : ApiController
    {
        // GET api/sendmail
        [Route("")]
        public IEnumerable<string> Get()
        {
            //new psp.api.Reports.GSRReport().BuildNotificationData("10/14/2014", true);
            //new DataImport().ImportGSRIntoMongoFromSqlServer();
            return new string[] { "value1", "value2" };
        }

        // GET api/sendmail/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/sendmail
        [Route("")]
        public void Post([FromBody]NotificationMessage notificationMessage)
        {
            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP_Host"]);
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTP_Username"], ConfigurationManager.AppSettings["SMTP_Pwd"]);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(notificationMessage.FromEmail);

                if (notificationMessage.ToEmails != null && notificationMessage.ToEmails.Count > 0)
                {
                    foreach (var email in notificationMessage.ToEmails)
                    {
                        message.To.Add(new MailAddress(email));
                    }
                }
                if (notificationMessage.Bccs != null && notificationMessage.Bccs.Count > 0)
                {
                    foreach (var bcc in notificationMessage.Bccs)
                    {
                        message.Bcc.Add(bcc);
                    }
                }
                if (notificationMessage.Ccs != null && notificationMessage.Ccs.Count > 0)
                {
                    foreach (var cc in notificationMessage.Ccs)
                    {
                        message.CC.Add(cc);
                    }
                }

                message.Subject = notificationMessage.Subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = notificationMessage.MessageBody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception e)
            {
                AuditService.SaveLog(new AuditLog
                {
                    auditDate = DateTime.Now,
                    message = "Failed To Send Mail",
                    type = psp.core.ResourceStrings.Audit_Notification,
                    name = "SendMailController"
                });
            }
        }

        // PUT api/sendmail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sendmail/5
        public void Delete(int id)
        {
        }
    }
}
