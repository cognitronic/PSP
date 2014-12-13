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
    [RoutePrefix("api/reports/volume")]
    public class VolumeController : ApiController
    {
       [Route("{reportDate}")]
        public VolumeChart Get(string reportDate)
        {
            var results = new VolumeReport().GetVolumeByDate(reportDate);
            return results;
        }

       [Route("washes/{reportDate}")]
       public VolumeChart GetByWashes(string reportDate)
       {
           var results = new VolumeReport().GetVolumeByWashes(reportDate);
           return results;
       }

        // GET api/volume/5
        [Route("run-notification/{reportDate}")]
        public string GetRunNotification(string reportDate)
        {
            var notification = new VolumeReport().BuildNotifications(new VolumeReport().BuildVolumeData(reportDate), reportDate);
            new SendMailController().Post(notification);
            return notification.Subject;
        }

        // POST api/volume
        public void Post([FromBody]string value)
        {
        }

        // PUT api/volume/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/volume/5
        public void Delete(int id)
        {
        }
    }
}
