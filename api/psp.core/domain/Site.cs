using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Runtime.Serialization;


namespace psp.core.domain
{
    [Serializable]
    public class Site
    {
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string sid { get; set; }
        [DataMember]
        public string location { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public int sitewatchid { get; set; }
        [DataMember]
        public string washlinkip { get; set; }
        [DataMember]
        public string dbconnectionstring { get; set; }
        [DataMember]
        public string dbtype { get; set; }
        [DataMember]
        public string dbname { get; set; }

    }
}
