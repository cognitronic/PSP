using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using psp.api.Controllers;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using System.Text;

namespace psp.api.helpers
{
    public class SendCoupon
    {
        public void SendBirthdayCoupon(SendCouponParams parms)
        {
            var notification = new NotificationRepository().GetByName("Birthday Coupons");
            var message = new NotificationMessage();
            StringBuilder sb = new StringBuilder();
            sb.Append(NotificationTemplates.BirthdayEmailHTMLStart(parms.clientId));
            sb.Append(NotificationTemplates.BirthdayEmailHTMLEnd());
            message.Bccs = notification.recipients;
            message.ToEmails.Add(parms.email);
            message.FromEmail = notification.fromemail;
            message.MessageBody = sb.ToString();
            message.Subject = notification.subject;

            new SendMailController().Post(message);

            var client = new ClientRepository().GetByEmail(parms.email);
            client.birthdaycouponsent = true;
            new ClientRepository().Save(client);

            AuditService.SaveLog(new AuditLog
            {
                auditDate = DateTime.Now,
                message = "Birthday coupon sent",
                name = "Birthday Coupon Notification",
                type = "birthday_notification",
                notification = message
            });


        }
    }
}