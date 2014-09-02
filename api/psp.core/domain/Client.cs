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
    public class Client : psp.core.domain.IClient
    {
        [DataMember]
        public ObjectId Id { get; set; }
        [DataMember]
        public string sid { get; set; }
        [DataMember]
        public string firstname { get; set; }
        [DataMember]
        public string lastname { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string zip { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public bool isactive { get; set; }
        [DataMember]
        public DateTime dateregistered { get; set; }
        [DataMember]
        public bool receivenotifications { get; set; }
        [DataMember]
        public string birthdate { get; set; }
        [DataMember]
        public bool addressverified { get; set; }
        [DataMember]
        public bool birthdaycouponsent { get; set; }
    }
}
