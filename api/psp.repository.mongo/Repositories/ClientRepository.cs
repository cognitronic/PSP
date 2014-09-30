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
    public class ClientRepository : BaseRepository<Client>
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

        public IList<Client> GetForBirthdays(ClientSearchParams parms)
        {
            var collection = _database.GetCollection<Client>("client");
            var andQueries = new List<IMongoQuery>();
            if (!string.IsNullOrEmpty(parms.lastname))
            {
                andQueries.Add(Query.Matches("lastname", BsonRegularExpression.Create(new Regex(parms.lastname, RegexOptions.IgnoreCase))));
            }
            if (!string.IsNullOrEmpty(parms.email))
            {
                andQueries.Add(Query.Matches("email", BsonRegularExpression.Create(new Regex(parms.email, RegexOptions.IgnoreCase))));
            }
            if (!string.IsNullOrEmpty(parms.birthdate))
            {
                andQueries.Add(Query.EQ("birthdate", parms.birthdate));
            }
            if (!string.IsNullOrEmpty(parms.dateregistered))
            {
                andQueries.Add(Query.EQ("dateregistered", BsonDateTime.Create(parms.dateregistered).AsBsonDateTime));
            }
            if (andQueries != null && andQueries.Count > 0)
            {
                var query = Query.And(andQueries);
                return collection.Find(query).OrderBy(o => o.lastname).ToList<Client>();
            }
            return null;
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

        public void Delete(string id)
        {
            var query = Query<Client>.EQ(u => u.Id, new ObjectId(id));
            _collection.Remove(query);
        }
    }
}
