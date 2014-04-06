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
    public class Notification
    {
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string sid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public DateTime lastran { get; set; }
        [DataMember]
        public IList<string> recipients { get; set; }
    }
}
