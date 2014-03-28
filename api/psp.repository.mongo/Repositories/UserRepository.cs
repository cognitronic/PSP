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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository()
            : base(ResourceStrings.Mongo_Users_Collection)
        {
        }
        public IList<User> GetAll()
        {
            return _collection.FindAllAs<User>().ToList<User>();
        }

        public IUser GetById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public IUser GetByEmail(string email)
        {
            var query = Query<User>.EQ(e => e.email, email);
            return _collection.FindOneAs<User>(query);
        }

        public IUser GetByEmailPassword(string email, string password)
        {
            var query = Query.And(
                Query.EQ("email", email),
                Query.EQ("password", password));
            return _collection.FindOneAs<User>(query);
        }

        public IUser Save(IUser user)
        {
            throw new NotImplementedException();
        }

        public IUser Delete(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
