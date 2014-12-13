using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.api.helpers;
using System.Configuration;
using psp.api.Controllers;

namespace psp.api.Controllers.Vendor
{
    [RoutePrefix("api/vendors/washlinkwashtotals")]
    public class WashLinkWashTotalsController : ApiController
    {
        [Route("{siteId}/{reportDate}")]
        public WashLinkWashTotals Get(string siteId, string reportDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAPIUrl"]);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var site = new SiteController().Get(siteId);

            var results = new WashLink().WashLinkWashTotalsBySiteDate(site, DateTime.Parse(reportDate), "", "");
            return results;
        }

        // GET api/washlinkwashtotals/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/washlinkwashtotals
        public void Post([FromBody]string value)
        {
        }

        // PUT api/washlinkwashtotals/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/washlinkwashtotals/5
        public void Delete(int id)
        {
        }
    }
}
