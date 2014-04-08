using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using psp.api.datahelpers;
using System.Web.Mvc;

namespace psp.api.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly NotificationRepository _repository;

        public NotificationsController()
        {
            _repository = new NotificationRepository();
        }

        // GET api/notifications
        public IList<Notification> GetAll()
        {

            var notifications = _repository.GetAll().OrderBy(o => o.name).ToList<Notification>();
            return notifications;
        }

        // GET api/notifications/5
        public Notification Get(string id)
        {
            return _repository.GetById(new ObjectId(id));
        }

        // POST api/notifications
        public Notification Post([FromBody]Notification site)
        {
            return _repository.Save(site);
        }

        // PUT api/notifications/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/notifications/5
        public void Delete(int id)
        {
        }

        public IAPIResponse RunNotification([FromBody]NotificationParam parm)
        {
            var note = _repository.GetById(new ObjectId(parm.notificationId));

            switch (note.name)
            { 
                case("GSR_Audit"):

                    break;
                case("Volume_Report"):
                    var list = new SiteWatch().RunVolumeData(parm.reportDate);

                    break;
                case ("Rewash_Alert"):
                    list = new SiteWatch().RunRewashNotification(parm.reportDate);
                    break;
            }
            return null;
        }
    }

    public class NotificationParam
    {
        public string notificationId { get; set; }
        public string reportDate { get; set; }
    }
}
