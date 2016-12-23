using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Globalization;
using System.ComponentModel;

namespace App2.ViewModels
{
    [DataContract]
    public class Stock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private int _id;
        [DataMember]
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged("Id"); } }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Ticker { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public decimal ActualPrice { get; set; }
        [DataMember]
        public decimal PurchasePrice { get; set; }
        [DataMember]
        public DateTime PurchaseDate { get; set; }
        [DataMember]
        public int Amount { get; set; }
        public string PurchasePriceText
        {
            get
            {
                return "Purchase Price: " + PurchasePrice.ToString() + " " + Currency;
            }
        }
        public string AmountText
        {
            get
            {
                return "Amount: " + Amount.ToString();
            }
        }
        public string ActualPriceText
        {
            get
            {
                return "Actual Price: " + ActualPrice.ToString() + " " + Currency; 
            }
        }
        public decimal ProfitLossAmount
        {
            get
            {
                return (ActualPrice - PurchasePrice) * Amount;
            }
        }
        public string ProfitLossAmountString
        {
            get
            {
                //return "ProfitLossAmountString";
                return ProfitLossAmount.ToString("0.##") + " " + Currency;
            }
        }
        public decimal ProfitLossPercentage
        {
            get
            {
                return ((ActualPrice / PurchasePrice) - 1) * 100;
            }
        }
        public string ProfitLossString
        {
            get
            {
                //return "ProfitLossString";
                return ProfitLossPercentage.ToString("0.##") + "%";
            }

        }public string ProfitLossColor
        {
            get
            {
                return (ProfitLossAmount > 0 ? "Green" : "Red");
            }
        }
    }
}
