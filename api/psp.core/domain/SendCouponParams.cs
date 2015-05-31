using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class SendCouponParams
    {
        [DataMember]
        public string coupon { get; set; }

        [DataMember]
        public string clientId { get; set; }

        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string sentBy { get; set; }
    }
}
