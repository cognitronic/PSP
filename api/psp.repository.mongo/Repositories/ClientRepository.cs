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
    public class ClientRepository : BaseRepository
    {
        public ClientRepository()
            : base(ResourceStrings.Mongo_Client_Collection)
        {
        }
        public IList<Client> GetAll()
        {
            return _collection.FindAllAs<Client>().OrderBy(o => o.lastname).ToList<Client>();
        }

        public Client GetById(ObjectId id)
        {
            var query = Query<Client>.EQ(e => e.Id, id);
            return _collection.FindOneAs<Client>(query);
        }

        public Client GetByEmail(string email)
        {
            var query = Query<Client>.EQ(e => e.email, email);
            return _collection.FindOneAs<Client>(query);
        }

        public Client Save(Client client)
        {
            if (client.sid != null && client.sid != "")
            {
                client.Id = new ObjectId(client.sid);
            }
            _collection.Save<Client>(client);
            return client;
        }

        public Client Delete(Client client)
        {
            var usr = client;
            var query = Query<Client>.EQ(u => u.Id, new ObjectId(client.sid));
            _collection.Remove(query);
            return usr;
        }
    }
}
