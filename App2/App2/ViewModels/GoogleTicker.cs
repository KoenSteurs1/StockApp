using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App2.ViewModels
{
    [DataContract]
    class GoogleTicker
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string t { get; set; }
        [DataMember]
        public string e { get; set; }
        [DataMember]
        public string l { get; set; }
        [DataMember]
        public string l_fix { get; set; }
        [DataMember]
        public string s { get; set; }
        [DataMember]
        public string ltt { get; set; }
        [DataMember]
        public string lt { get; set; }
        [DataMember]
        public string lt_dts { get; set; }
        [DataMember]
        public string c { get; set; }
        [DataMember]
        public string c_fix { get; set; }
        [DataMember]
        public string cp { get; set; }
        [DataMember]
        public string cp_fix { get; set; }
        [DataMember]
        public string ccol { get; set; }
        [DataMember]
        public string pcls_fix { get; set; }
    }
}
