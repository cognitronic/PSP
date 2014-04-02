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
