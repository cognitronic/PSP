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
    public class CouponCode
    {
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string codetext { get; set; }
        [DataMember]
        public string coupon { get; set; }
        [DataMember]
        public bool isredeemed { get; set; }
        [DataMember]
        public bool isassigned { get; set; }

        [DataMember]
        public string assignedemail { get; set; }
        [DataMember]
        public DateTime dateassigned { get; set; }
    }
}
