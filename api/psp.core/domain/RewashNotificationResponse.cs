using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class RewashNotificationResponse : IAPIResponse
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ReturnUrl { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string RewashCount { get; set; }
        [DataMember]
        public string CarCount { get; set; }
        [DataMember]
        public string Percentage { get; set; }
    }
}
