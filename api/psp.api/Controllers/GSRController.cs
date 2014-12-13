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
using System.Text;
using System.Reflection;

namespace psp.api.Controllers
{
    [RoutePrefix("api/gsr")]
    public class GsrController : ApiController
    {
        private readonly GSRRepository _repository;

         public GsrController()
        {
            _repository = new GSRRepository();
        }

        // GET api/gsr
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("save/{gsrDate}")]
        public IEnumerable<string> GetSaveGSR(string gsrDate)
        {
            new psp.api.Reports.GSRReport().BuildNotificationData(gsrDate, true);
            return new string[] { "value1", "value2" };
        }

        // GET api/gsr/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("{siteId}/{gsrDate}")]
        public GSR Get(string siteId, string gsrDate)
        {
            var site = new SiteController().Get(siteId);

            var gsr = new GSRReport().GetAmountToAudit(site, DateTime.Parse(gsrDate), "", "");
            return gsr;
        }

        [Route("run-notification/{reportDate}")]
        public string Get(string reportDate)
        {
            var notification = new GSRReport().BuildNotifications(new GSRReport().BuildNotificationData(reportDate, false), reportDate);
            new SendMailController().Post(notification);
            return notification.Subject;
        }

        [Route("export/{siteName}/{dateRange}")]
        public HttpResponseMessage GetExportGSR(string siteName, string dateRange)
        {
            var sb = new StringBuilder();
            if(siteName == "All" || siteName == "")
            {
                sb.Append(new GSRReport().ExportGSRToCSV(new SiteRepository().GetAll(), dateRange));
            }
            else
            {
                var sites = new List<Site>();
                sites.Add(new SiteRepository().GetByName(siteName));
                sb.Append(new GSRReport().ExportGSRToCSV(sites, dateRange));
            }
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(sb.ToString());
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment"); //attachment will force download
            result.Content.Headers.ContentDisposition.FileName = "GSRExport.csv";

            return result;
        }

        // POST api/gsr
        [Route("export/session")]
        public string PostExportSession([FromBody]GSRExportModel parms)
        {
            var session = new PspSession();
            session.name = DateTime.Now.ToFileTimeUtc().ToString();;
            var sites = new List<Site>();
            foreach (var site in parms.sites)
            {
                sites.Add(new SiteRepository().GetByName(site));
            }
            session.val = new GSRReport().ExportGSRToCSV(sites, parms.dateRange);
            new SessionRepository().Save(session);
            return session.name;
        }

        [Route("{siteId}/{gsrDate}")]
        public GSR PostByDate([FromBody]GSRViewModel parms)
        {
            var site = new SiteController().Get(parms.site);

            var gsr = new GSRReport().GetAmountToAudit(site, DateTime.Parse(parms.gsrDate), parms.fromTime, parms.toTime);
            return gsr;
        }

        [Route("export/key/{key}")]

        // PUT api/gsr/5
        public HttpResponseMessage GetByKey(string key)
        {
            var session = new SessionRepository().GetByName(key);
            new SessionRepository().Delete(session.name);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(session.val);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment"); //attachment will force download
            result.Content.Headers.ContentDisposition.FileName = "GSRExport.csv";

            return result;
        }

        // DELETE api/gsr/5
        public void Delete(int id)
        {
        }
    }

    public class GSRViewModel
    {
        public string site { get; set; }
        public string gsrDate { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
    }

    public class GSRExportModel
    {
        public string dateRange { get; set; }
        public IList<string> sites { get; set; }
    }
}
