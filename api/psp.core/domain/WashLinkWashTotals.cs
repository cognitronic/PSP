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
    public class WashLinkWashTotals
    {
        [DataMember]
        public int primeshinewash { get; set; }
        //[DataMember]
        //public int platinumwash { get; set; }
        [DataMember]
        public int protexwash { get; set; }
        [DataMember]
        public int premierwash { get; set; }
        [DataMember]
        public int tireshine { get; set; }

        [DataMember]
        public int plusplus { get; set; }

        [DataMember]
        public int rainx { get; set; }

        [DataMember]
        public string sitename { get; set; }

        [DataMember]
        public DateTime date { get; set; }
    }
}
