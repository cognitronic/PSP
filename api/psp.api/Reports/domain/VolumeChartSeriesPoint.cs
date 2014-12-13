using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace psp.api.Reports.domain
{
    [Serializable]
    public class VolumeChartSeriesPoint
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int y { get; set; }

        [DataMember]
        public string color { get; set; }

        [DataMember]
        public VolumeDto dto { get; set; }
    }
}