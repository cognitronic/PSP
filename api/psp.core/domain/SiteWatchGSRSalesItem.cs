using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace psp.core.domain
{
    [Serializable]
    public class SiteWatchGSRSalesItem
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
        public DateTime logdate { get; set; }
        [DataMember]
        public string reportcategory { get; set; }
        [DataMember]
        public int locationid { get; set; }
        [DataMember]
        public decimal amt { get; set; }
        [DataMember]
        public decimal val { get; set; }
    }
}
