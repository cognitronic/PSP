using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace psp.api.Controllers
{
    public class CouponController : ApiController
    {
        // GET api/coupon
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/coupon/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/coupon
        public void Post([FromBody]string value)
        {
        }

        // PUT api/coupon/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/coupon/5
        public void Delete(int id)
        {
        }
    }
}
