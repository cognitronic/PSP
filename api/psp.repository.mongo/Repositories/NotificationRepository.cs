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
    public class NotificationRepository : BaseRepository
    {
        public NotificationRepository()
            : base(ResourceStrings.Mongo_Notification_Collection)
        {
        }
        public IList<Notification> GetAll()
        {
            return _collection.FindAllAs<Notification>().OrderBy(o => o.name).ToList<Notification>();
        }

        public Notification GetById(ObjectId id)
        {
            var query = Query<Notification>.EQ(e => e.Id, id);
            return _collection.FindOneAs<Notification>(query);
        }

        public Notification GetByName(string name)
        {
            var query = Query<Notification>.EQ(e => e.name, name);
            return _collection.FindOneAs<Notification>(query);
        }

        public Notification Save(Notification Site)
        {
            if (Site.sid != null && Site.sid != "")
            {
                Site.Id = new ObjectId(Site.sid);
            }
            _collection.Save<Notification>(Site);
            return Site;
        }

        public Notification Delete(Notification Site)
        {
            var usr = Site;
            var query = Query<Notification>.EQ(u => u.Id, new ObjectId(Site.sid));
            _collection.Remove(query);
            return usr;
        }

        //public Site GetByEmailPassword(string email, string password)
        //{
        //    var query = Query.And(
        //        Query.EQ("email", email),
        //        Query.EQ("password", password));
        //    return _collection.FindOneAs<Site>(query);
        //}

        
    }
}

