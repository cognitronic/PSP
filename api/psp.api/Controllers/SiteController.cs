using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using psp.api.helpers;

namespace psp.api.Controllers
{
    [RoutePrefix("api/sites")]
    public class SiteController : ApiController
    {
         private readonly SiteRepository _repository;
         private readonly SiteWatch _sitewatch;
         private readonly WashLink _washlink;

        public SiteController()
        {
            _sitewatch = new SiteWatch();
            _washlink = new WashLink();
            _repository = new SiteRepository();
        }

        // GET api/site
        [Route("")]
        public IList<Site> Get()
        {
            //var test = _sitewatch.SitewatchSalesBySiteDate("2", DateTime.Today.AddDays(-60));
            var sites = _repository.GetAll().OrderBy(o => o.name).ToList<Site>();
            //var mysql = _washlink.WashLinkWashTotalsBySiteDate(sites[11], DateTime.Today.AddDays(-60));
            return sites;
        }

        // GET api/site/5
        [Route("{id}")]
        public Site Get(string id)
        {
            return _repository.GetById(new ObjectId(id));
        }

        // POST api/site
        public Site Post([FromBody]Site site)
        {
            return _repository.Save(site);
        }

        // PUT api/site/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/site/5
        public void Delete(int id)
        {
        }
    }
}
