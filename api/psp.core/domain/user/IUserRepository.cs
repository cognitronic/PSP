using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace psp.core.domain.user
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        User GetById(ObjectId id);
        User GetByEmail(string email);
        User GetByEmailPassword(string email, string password);
        User Save(User user);
        User Delete(User user);
    }
}
