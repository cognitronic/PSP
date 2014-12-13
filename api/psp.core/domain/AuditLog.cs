using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class AuditLog
    {
        [DataMember]
        public ObjectId Id { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public DateTime auditDate { get; set; }

        [DataMember]
        public object notification { get; set; }

        [DataMember]
        public string message { get; set; }
    }
}
