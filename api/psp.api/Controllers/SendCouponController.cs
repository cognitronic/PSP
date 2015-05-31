using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using psp.api.Reports;
using MongoDB.Bson;
using psp.api.helpers;
using System.Configuration;

namespace psp.api.Controllers
{
    [RoutePrefix("api/coupons")]
    public class SendCouponController : ApiController
    {
        // GET api/sendcoupon
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/sendcoupon/5
        [Route("{coupon}")]
        public string Get(string coupon)
        {
            return "value";
        }

        // POST api/sendcoupon
        [Route("")]
        public void Post([FromBody]SendCouponParams parms)
        {
            switch(parms.coupon)
            {
                case "birthday":
                    new SendCoupon().SendBirthdayCoupon(parms);
                    break;
                case "courtesy":
                    new SendCoupon().SendCourtesyCoupon(parms);
                    break;
            }
        }

        // PUT api/sendcoupon/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sendcoupon/5
        public void Delete(int id)
        {
        }
    }
}
