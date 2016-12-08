using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace App2.ViewModels
{
    [DataContract]
    public class Stock
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Ticker { get; set; }
        [DataMember]
        public string Currency { get; set;  }
        [DataMember]
        public decimal ActualPrice { get; set; }
        [DataMember]
        public decimal PurchasePrice { get; set; }
        [DataMember]
        public DateTime PurchaseDate { get; set; }
        [DataMember]
        public int Amount { get; set; }

    }
}
