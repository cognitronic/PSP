using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace psp.api.Reports.domain
{
    [Serializable]
    public class VolumeChartSeries
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public IList<VolumeChartSeriesPoint> data { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string stack { get; set; }
    }
}