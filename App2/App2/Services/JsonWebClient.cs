using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
    public class JsonWebClient
    {
        public async Task<System.IO.TextReader> DoRequestAsync(WebRequest req)
        {
            try
            {
                var task = Task.Factory.FromAsync((cb, o) => ((HttpWebRequest)o).BeginGetResponse(cb, o), res => ((HttpWebRequest)res.AsyncState).EndGetResponse(res), req);

                var result = await task;
                var resp = result;
                var stream = resp.GetResponseStream();
                var sr = new System.IO.StreamReader(stream);
                return sr;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<System.IO.TextReader> DoRequestAsync(string url)
        {
            HttpWebRequest req = HttpWebRequest.CreateHttp(url);
            // req.ContentType = "application/json";
            req.AllowReadStreamBuffering = true;
            var tr = await DoRequestAsync(req);
            return tr;
        }
    }
}
