﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace psp.api.Controllers
{
    public class SiteController : ApiController
    {
        // GET api/site
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
