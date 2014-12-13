using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace psp.api.Reports.domain
{
    [Serializable]
    public class VolumeChart
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public IList<VolumeChartSeries> data { get; set; }

        [DataMember]
        public IList<string> categories { get; set; }
    }
}