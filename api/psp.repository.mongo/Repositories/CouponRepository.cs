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
    public class CouponRepository : BaseRepository<Coupon>
    {
        public CouponRepository()
            : base(ResourceStrings.Mongo_Coupon_Collection)
        {
            
            
        }
        public IList<Coupon> GetAll()
        {
            return _collection.FindAllAs<Coupon>().OrderBy(o => o.name).ToList<Coupon>();
        }

        public Coupon GetById(ObjectId id)
        {
            var query = Query<Coupon>.EQ(e => e.Id, id);
            return _collection.FindOneAs<Coupon>(query);
        }

        public Coupon GetByName(string name)
        {
            var query = Query<Coupon>.EQ(e => e.name, name);
            return _collection.FindOneAs<Coupon>(query);
        }

    }
}
