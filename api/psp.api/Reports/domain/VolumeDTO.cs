using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace psp.api.Reports.domain
{
    [Serializable]
    public class VolumeDto
    {
        [DataMember]
        public string siteName { get; set; }

        [DataMember]
        public string reportDate { get; set; }

        [DataMember]
        public int carCount { get; set; }

        [DataMember]
        public decimal pricePerCar { get; set; }
    }
}