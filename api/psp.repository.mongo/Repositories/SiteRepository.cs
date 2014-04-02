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
    public class SiteRepository : BaseRepository
    {
        public SiteRepository()
            : base(ResourceStrings.Mongo_Site_Collection)
        {
        }
        public IList<Site> GetAll()
        {
            return _collection.FindAllAs<Site>().OrderBy(o => o.name).ToList<Site>();
        }

        public Site GetById(ObjectId id)
        {
            var query = Query<Site>.EQ(e => e.Id, id);
            return _collection.FindOneAs<Site>(query);
        }

        public Site GetByName(string name)
        {
            var query = Query<Site>.EQ(e => e.name, name);
            return _collection.FindOneAs<Site>(query);
        }

        //public Site GetByEmailPassword(string email, string password)
        //{
        //    var query = Query.And(
        //        Query.EQ("email", email),
        //        Query.EQ("password", password));
        //    return _collection.FindOneAs<Site>(query);
        //}

        public Site Save(Site Site)
        {
            if (Site.sid != null && Site.sid != "") {
                Site.Id = new ObjectId(Site.sid);
            }
            _collection.Save<Site>(Site);
            return Site;
        }

        public Site Delete(Site Site)
        {
            var usr = Site;
            var query = Query<Site>.EQ(u => u.Id, new ObjectId(Site.sid));
            _collection.Remove(query);
            return usr;
        }
    }
}
