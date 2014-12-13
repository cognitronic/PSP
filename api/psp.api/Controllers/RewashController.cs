using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using psp.api.Reports;
using MongoDB.Bson;
using psp.api.helpers;
using psp.api.Reports;
using psp.api.Reports.domain;

namespace psp.api.Controllers
{
    [RoutePrefix("api/reports/rewash")]
    public class RewashController : ApiController
    {
        // GET api/rewash
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/rewash/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("run-notification/{reportDate}")]
        public string GetRunNotification(string reportDate)
        {
            var notification = new RewashReport().BuildNotifications(reportDate);
            new SendMailController().Post(notification);
            return notification.Subject;
        }

        // POST api/rewash
        public void Post([FromBody]string value)
        {
        }

        // PUT api/rewash/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/rewash/5
        public void Delete(int id)
        {
        }
    }
}
