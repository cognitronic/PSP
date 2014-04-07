using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using psp.core.datahelpers;

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
    }
}
