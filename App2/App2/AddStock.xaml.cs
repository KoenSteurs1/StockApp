using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App2
{
    public partial class AddStock : ContentPage
    {
        public AddStock()
        {
            InitializeComponent();
        }

        async void Button_OnClicked(object sender, EventArgs args)
        {
            bool errors = false;
            string errorMessage = string.Empty;

            Stock item = new ViewModels.Stock();

            item.Ticker = txtTicker.Text;

            string amount = txtAmount.Text;
            int dAmount;
            if (Int32.TryParse(amount, out dAmount))
            {
                item.Amount = dAmount;
            }
            else
            {
                errors = true;
                errorMessage += "Amount must be numeric.";
            }

            string price = txtPrice.Text;
            decimal dPrice;
            if (Decimal.TryParse(price, out dPrice))
            {
                item.PurchasePrice = dPrice;
            }
            else
            {
                errors = true;
                errorMessage += "Price must be numeric.";
            } 

            if (errors)
            {
                await DisplayAlert("Error", errorMessage, "OK");
            }
            else
            {
                await DisplayAlert("Added", "Stock has been added", "OK");
                await Navigation.PushModalAsync(new HomePage());
            }

            
        }
    }
}
