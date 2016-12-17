using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App2
{
    public partial class ViewStock : ContentPage
    {
        int id;
        
        public ViewStock(int Id)
        {
            InitializeComponent();
            id = Id;
        }

        protected override async void OnAppearing()
        {
            StockStore ss = new StockStore();
            await ss.LoadStocks();

            Stock s = new Stock();
            s = ss.stocks.Where(x => x.Id == id).FirstOrDefault();

            BindingContext = s;

            base.OnAppearing();
        }
    }
}
