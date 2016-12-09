using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App2
{
    public partial class MyTabbedPage : TabbedPage
    {
        public MyTabbedPage()
        {
            var navigationPage = new NavigationPage(new HomePage());
            //navigationPage.Icon = "schedule.png";
            navigationPage.Title = "Schedule";

            //Children.Add(new TodayPageCS());
            Children.Add(navigationPage);
        }
    }
}
