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

        // GET api/gsr/5
        public string Get(int id)
        {
            return "value";
        }

        public GSR Get(string gsrDate, string site)
        {
            var gsr = _repository.GetBySiteIdGSRDate(site, gsrDate);
            return gsr;
        }

        // POST api/gsr
        public void Post([FromBody]string value)
        {
        }

        // PUT api/gsr/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/gsr/5
        public void Delete(int id)
        {
        }
    }

    public class GSRViewModel
    {
        public string site { get; set; }
        public DateTime gsrDate { get; set; }
    }
}
