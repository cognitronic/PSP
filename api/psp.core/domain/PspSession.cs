using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace psp.core.domain
{
    [Serializable]
    public class PspSession
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string val { get; set; }

        [DataMember]
        public ObjectId Id { get; set; }
    }
}
