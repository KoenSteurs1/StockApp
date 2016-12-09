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
            await Navigation.PushModalAsync(new HomePage());
        }
    }
}
