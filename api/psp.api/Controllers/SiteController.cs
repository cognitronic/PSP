using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;

namespace psp.api.Controllers
{
    public class SiteController : ApiController
    {
         private readonly SiteRepository _repository;

        public SiteController()
        {
            _repository = new SiteRepository();
        }

        // GET api/site
        public IList<Site> GetAll()
        {
            var sites = _repository.GetAll();
            return sites;
        }

        // GET api/site/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/site
        public void Post([FromBody]string value)
        {
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
