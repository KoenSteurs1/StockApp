using System.Net;
using System.Threading.Tasks;
using System.IO;
using System;
using Windows.Web.Http;
using App2.ViewModels;
using System.Collections.Generic;

namespace App2.Services
{

    public class QuotePriceService
    {
       private const string url = "http://finance.google.com/finance/info?client=ig&q=";

        public async Task<decimal?> getPrice(string ticker)
        {
            string result;
            decimal price;
            GoogleTicker gticker;
            string sPrice = String.Empty;
            //List<GoogleTicker> tickerArr = new List<GoogleTicker>();

            JsonWebClient client = new JsonWebClient();
            try
            { 
                var resp = await client.DoRequestAsync(url + ticker);
                result = resp.ReadToEnd();
                
                // doing some ugly things here but can't put the returned JSON into an array for some reason
                result = result.Replace("\n// [", "");
                result = result.Replace("\n]\n", "");
                gticker = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleTicker>(result);

                //tickerArr = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GoogleTicker>>(result);
                //sPrice = tickerArr[0].l;
                
                sPrice = gticker.l;
            }
            catch(Exception ex)
            {
                // something went wrong requesting the ticker from Google Finance, probably unknown ticker.
            }

            if (Decimal.TryParse(sPrice, out price))
                return price;
            else
                return null;
        }
    }
}
