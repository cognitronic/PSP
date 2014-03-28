using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace psp.api.Controllers
{
    public class CustomerRegistrationController : ApiController
    {
        // GET api/customerregistration
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/customerregistration/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/customerregistration
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customerregistration/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customerregistration/5
        public void Delete(int id)
        {
        }
    }
}
