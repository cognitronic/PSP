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
    [RoutePrefix("api/coupon-codes")]
    public class CouponCodeController : ApiController
    {
        private readonly CouponCodeRepository _repository;

        public CouponCodeController()
        {
            _repository = new CouponCodeRepository();
        }

        // GET api/couponcode
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("{coupon}")]
        public IList<CouponCode> Get(string coupon)
        {
            return _repository.GetByCouponByStatus(coupon, true);
        }

        // POST api/couponcode
        public void Post([FromBody]string value)
        {
        }

        // PUT api/couponcode/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/couponcode/5
        public void Delete(int id)
        {
        }
    }
}
