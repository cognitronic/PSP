using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using psp.core.domain;
using psp.core;
using psp.repository.mongo.Cache;
using System.Text.RegularExpressions;

namespace psp.repository.mongo.Repositories
{
    public class CouponCodeRepository : BaseRepository<CouponCode>
    {
        public CouponCodeRepository()
            : base(ResourceStrings.Mongo_CouponCode_Collection)
        {
            
            
        }
        public IList<CouponCode> GetAll()
        {
            return _collection.FindAllAs<CouponCode>().OrderBy(o => o.coupon).ToList<CouponCode>();
        }

        public CouponCode GetById(ObjectId id)
        {
            var query = Query<CouponCode>.EQ(e => e.Id, id);
            return _collection.FindOneAs<CouponCode>(query);
        }

        public IList<CouponCode> GetByCoupon(string coupon)
        {
            var collection = _database.GetCollection<CouponCode>("coupon_code");
            var query = Query<CouponCode>.EQ(e => e.coupon, coupon);
            return collection.Find(query).OrderBy(o => o.codetext).ToList<CouponCode>();
        }

        public IList<CouponCode> GetByCouponByStatus(string coupon, bool isassigned)
        {
            var collection = _database.GetCollection<CouponCode>("coupon_code");
            var query = Query.And(Query<CouponCode>.EQ(e => e.coupon, coupon), Query<CouponCode>.EQ(e => e.isassigned, isassigned));
            return collection.Find(query).OrderByDescending(o => o.dateassigned).ToList<CouponCode>();
        }

        public CouponCode GetByCode(string code)
        {
            var query = Query<CouponCode>.EQ(e => e.code, code);
            return _collection.FindOneAs<CouponCode>(query);
        }

        public CouponCode GetByCodeText(string codetext)
        {
            var query = Query<CouponCode>.EQ(e => e.codetext, codetext);
            return _collection.FindOneAs<CouponCode>(query);
        }

        public CouponCode Save(CouponCode code)
        {
            if (code.coupon != null && code.coupon != "")
            {
                //code.Id = new ObjectId(code.sid);
            }
            _collection.Save<CouponCode>(code);
            return code;
        }
    }
}
