using App2.Services;
using App2.ViewModels;
using PCLStorage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace App2
{
    public partial class HomePage : ContentPage
    {
        StockStore ss { get; set;  }

        public HomePage()
        {
            InitializeComponent();

            ss = new StockStore();

            lstView.ItemTapped += async (sender, e) =>
            {
                int id;
                id = ((Stock)e.Item).Id;
                await Navigation.PushModalAsync(new ViewStock(id));
            };
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await BindList();

            Task t = Task.Run(() => UpdatePrices());
            await t.ContinueWith((t1) =>
               {
                   Device.BeginInvokeOnMainThread(async () =>
                   {
                       await ss.SaveStocks();
                       await ss.LoadStocks();
                       lstView.ItemsSource = ss.stocks;
                   });
               });
        }
        
        private async Task BindList()
        {
            await ss.LoadStocks();
            lstView.ItemsSource = ss.stocks;
        }

        async private Task UpdatePrices()
        {
            await ss.UpdatePrices();
        }

        async void Button_OnClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddStock());
        }

        async void Button_ResetPrices(object sender, EventArgs args)
        {
            await ss.ResetPrices();
            await ss.LoadStocks();
            await BindList();
        }

        async void Button_UpdatePrices(object sender, EventArgs args)
        {
            await ss.UpdatePrices();
            await ss.SaveStocks();
            await BindList();
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete this item?", "Yes", "No");
            if (confirm)
            {
                var mi = ((MenuItem)sender);
                Stock item = ss.stocks.Where(x => x.Id.ToString() == mi.CommandParameter.ToString()).FirstOrDefault();
                ss.stocks.Remove(item);
                await ss.SaveStocks();
                lstView.ItemsSource = ss.stocks;
            }
        }
    }
}
