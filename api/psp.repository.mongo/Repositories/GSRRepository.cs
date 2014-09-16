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

namespace psp.repository.mongo.Repositories
{
    public class GSRRepository : BaseRepository<GSR>
    {
        public GSRRepository()
            : base(ResourceStrings.Mongo_GSR_Collection)
        {
        }
        public IList<GSR> GetAll()
        {
            return _collection.FindAllAs<GSR>().OrderBy(o => o.gsrDate).ToList<GSR>();
        }

        public GSR GetById(ObjectId id)
        {
            var query = Query<GSR>.EQ(e => e.Id, id);
            return _collection.FindOneAs<GSR>(query);
        }

        public GSR GetByGSRDate(string date)
        {
            var query = Query<GSR>.EQ(e => e.gsrDate, date);
            return _collection.FindOneAs<GSR>(query);
        }

        public IList<GSR> GetBySiteId(string siteId)
        {
            var query = Query<GSR>.EQ(e => e.Id, new ObjectId(siteId));
            return _collection.FindAs<GSR>(query).ToList<GSR>();
        }

        public GSR GetBySiteIdGSRDate(string site, string date)
        {
            var query = Query.And(
                Query.EQ("sitename", site),
                Query.EQ("gsrdate", date));
            return _collection.FindOneAs<GSR>(query);
        }

        public GSR Save(GSR gsr)
        {
            if (gsr.sid != null && gsr.sid != "")
            {
                gsr.Id = new ObjectId(gsr.sid);
            }
            _collection.Save<GSR>(gsr);
            return gsr;
        }

        public GSR Delete(GSR client)
        {
            var usr = client;
            var query = Query<GSR>.EQ(u => u.Id, new ObjectId(client.sid));
            _collection.Remove(query);
            return usr;
        }
    }
}
