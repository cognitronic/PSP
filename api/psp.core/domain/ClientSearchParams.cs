using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class ClientSearchParams
    {
        [DataMember]
        public string lastname { get; set; }
        
        [DataMember]
        public string email { get; set; }
        
        [DataMember]
        public string birthdate { get; set; }
        
        [DataMember]
        public string dateregistered { get; set; }
    }
}
