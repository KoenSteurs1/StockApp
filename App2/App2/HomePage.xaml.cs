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
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace App2
{
    public partial class HomePage : ContentPage
    {
        StockStore ss { get; set;  }

        public HomePage()
        {
            InitializeComponent();

            lstView.ItemTapped += async (sender, e) =>
            {
                int id;
                id = ((Stock)e.Item).Id;
                await Navigation.PushModalAsync(new ViewStock(id));
            };
        }

        protected override async void OnAppearing()
        {
            ss = new StockStore();
            await ss.LoadStocks();

            lstView.ItemsSource = ss.stocks;

            base.OnAppearing();
        }

        async void Button_OnClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddStock());
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
