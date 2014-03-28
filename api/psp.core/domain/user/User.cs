using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace psp.core.domain.user
{
    [Serializable]
    public class User : IUser
    {
        [DataMember]
        public ObjectId Id { get; set; }

        [DataMember]
        public string first { get; set; }

        [DataMember]
        public string last { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string password { get; set; }
    }
}
