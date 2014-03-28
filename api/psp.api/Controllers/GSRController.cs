using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace psp.api.Controllers
{
    public class GSRController : ApiController
    {
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
}
