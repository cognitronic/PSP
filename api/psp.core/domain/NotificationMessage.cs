using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class NotificationMessage
    {
        public NotificationMessage()
        {
            this.ToEmails = new List<string>();
            this.Bccs = new List<string>();
            this.Ccs = new List<string>();
        }
        [DataMember]
        public string FromEmail { get; set; }
        [DataMember]
        public IList<string> ToEmails { get; set; }
        [DataMember]
        public IList<string> Bccs { get; set; }
        [DataMember]
        public IList<string> Ccs { get; set; }
        [DataMember]
        public string MessageBody { get; set; }
        [DataMember]
        public string Subject { get; set; }
    }
}
