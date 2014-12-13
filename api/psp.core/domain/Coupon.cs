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
    public class Coupon
    {
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string sid { get; set; }
        [DataMember]
        public DateTime startdate{ get; set; }
        [DataMember]
        public DateTime enddate { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public bool isactive { get; set; }
    }
}
