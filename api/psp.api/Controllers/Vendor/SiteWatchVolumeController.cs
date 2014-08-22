using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.api.helpers;
using psp.core.domain;

namespace psp.api.Controllers.Vendor
{
    [RoutePrefix("api/vendors/sitewatchvolume")]
    public class SiteWatchVolumeController : ApiController
    {
        // GET api/sitewatchvolume
        [Route("{siteId}/{volumeDate}")]
        public IEnumerable<SiteWatchSalesItem> Get(string siteId, string volumeDate)
        {
            var results = new SiteWatch().SitewatchSalesBySiteDate(siteId, DateTime.Parse(volumeDate));

            return results;
        }

        // GET api/sitewatchvolume/5
        [Route("{volumeDate:datetime}/{siteId:int}")]
        public IEnumerable<SiteWatchSalesItem> Get(DateTime volumeDate, string siteId)
        {
            var results = new SiteWatch().SitewatchSalesBySiteDate(siteId, volumeDate);

            return results;
        }

        // POST api/sitewatchvolume
        public void Post([FromBody]string value)
        {
        }

        // PUT api/sitewatchvolume/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sitewatchvolume/5
        public void Delete(int id)
        {
        }
    }
}
