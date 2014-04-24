using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace psp.core.domain.user
{
    public interface IUser
    {
        ObjectId Id { get; set; }
        string sid { get; set; }
        string first { get; set; }
        string last { get; set; }
        string email { get; set; }
        string password { get; set; }
        string role { get; set; }
    }
}
