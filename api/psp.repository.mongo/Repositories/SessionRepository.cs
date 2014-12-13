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
    public class SessionRepository: BaseRepository<PspSession>
    {
        public SessionRepository()
            : base(ResourceStrings.Mongo_Session_Collection)
        { 
        }
        public IList<PspSession> GetAll()
        {
            return _collection.FindAllAs<PspSession>().OrderBy(o => o.val).ToList<PspSession>();
        }

        public PspSession GetById(ObjectId id)
        {
            var query = Query<PspSession>.EQ(e => e.Id, id);
            return _collection.FindOneAs<PspSession>(query);
        }

        public PspSession GetByName(string name)
        {
            var query = Query<PspSession>.EQ(e => e.name, name);
            return _collection.FindOneAs<PspSession>(query);
        }

        public PspSession Save(PspSession session)
        {
            //if (client.sid != null && client.sid != "")
            //{
            //    client.Id = new ObjectId(client.sid);
            //}
            _collection.Save<PspSession>(session);
            return session;
        }

        public void Delete(string name)
        {
            var query = Query<PspSession>.EQ(u => u.name, name);
            _collection.Remove(query);
        }
    }
}
