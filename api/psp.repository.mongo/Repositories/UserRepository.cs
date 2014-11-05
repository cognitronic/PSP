using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using psp.core.domain.user;
using psp.core;
using psp.repository.mongo.Cache;

namespace psp.repository.mongo.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository()
            : base(ResourceStrings.Mongo_Users_Collection)
        {
        }
        public IList<User> GetAll()
        {
            return _collection.FindAllAs<User>().ToList<User>();
        }

        public User GetById(ObjectId id)
        {
            var query = Query<User>.EQ(e => e.Id, id);
            return _collection.FindOneAs<User>(query);
        }

        public User GetByEmail(string email)
        {
            var query = Query<User>.EQ(e => e.email, email);
            return _collection.FindOneAs<User>(query);
        }

        public User GetByEmailPassword(string email, string password)
        {
            var query = Query.And(
                Query.EQ("email", email),
                Query.EQ("password", password));
            return _collection.FindOneAs<User>(query);
        }

        public User Save(User user)
        {
            if (user.sid != null && user.sid != "") {
                user.Id = new ObjectId(user.sid);
            }
            _collection.Save<User>(user);
            return user;
        }

        public void Delete(string id)
        {
            var query = Query<User>.EQ(u => u.Id, new ObjectId(id));
            _collection.Remove(query);
        }
    }
}
