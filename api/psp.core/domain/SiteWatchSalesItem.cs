using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class SiteWatchSalesItem
    {
        [DataMember]
        public string objid { get; set; }
        [DataMember]
        public string item { get; set; }
        [DataMember]
        public string itemtype { get; set; }
        [DataMember]
        public string total { get; set; }
        [DataMember]
        public string logdate { get; set; }
        [DataMember]
        public string reportcategory { get; set; }
        [DataMember]
        public string locationid { get; set; }
        [DataMember]
        public string amt { get; set; }
        [DataMember]
        public string val { get; set; }
        [DataMember]
        public string sitename { get; set; }
    }
}
