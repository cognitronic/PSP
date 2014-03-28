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
        IUser GetById(ObjectId id);
        IUser GetByEmail(string email);
        IUser GetByEmailPassword(string email, string password);
        IUser Save(IUser user);
        IUser Delete(IUser user);
    }
}
