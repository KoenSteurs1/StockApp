using App2.ViewModels;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App2
{
    public class StockStore
    {
        public ObservableCollection<Stock> stocks { get; set; }

        public StockStore() 
        {

        }

        async Task<bool> CheckLocalFileExists()
        {
            var returnvalue = false;
            ExistenceCheckResult res;

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            res = await rootFolder.CheckExistsAsync("StocksApp");

            if (res == ExistenceCheckResult.FolderExists)
            {
                IFolder folder = await rootFolder.GetFolderAsync("StocksApp");

                res = await folder.CheckExistsAsync("Stocks.xml");
                if (res == ExistenceCheckResult.FileExists)
                    returnvalue = true;
            }

            return returnvalue;
        }

        async public Task<bool> SaveStocks()
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Stock>));
            string myStr;
            var ms = new MemoryStream();
            serializer.Serialize(ms, stocks);

            ms.Position = 0;
            var sr = new StreamReader(ms);
            myStr = sr.ReadToEnd();

            // get hold of the file system
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            // create a folder, if one does not exist already
            IFolder folder = await rootFolder.CreateFolderAsync("StocksApp", CreationCollisionOption.OpenIfExists);

            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync("Stocks.xml", CreationCollisionOption.ReplaceExisting);

            // populate the file with some text
            await file.WriteAllTextAsync(myStr);

            return true;
        }

        async public Task<ObservableCollection<Stock>> LoadStocks()
        {
            if (await CheckLocalFileExists() == true)
                stocks = await LoadStocksFromLocalFile();
            else
                stocks = await Task.Run(() => LoadStocksFromEmbeddedResource());

            return stocks;
        }

        public async Task<bool> AddStock(Stock s)
        {
            await Task.Run(() => stocks.Add(s));

            return true;
        }

        public async Task<bool> RemoveStock(Stock s)
        {
            await Task.Run(() => stocks.Remove(s));

            return true;
        }

        private async Task <ObservableCollection<Stock>> LoadStocksFromLocalFile()
        {
            // load file from local file system
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync("StocksApp");
            IFile file = await folder.GetFileAsync("Stocks.xml");
            string fileInput = await file.ReadAllTextAsync();

            TextReader textReader = new StringReader(fileInput);

            var serializer = new XmlSerializer(typeof(ObservableCollection<Stock>));
            return (ObservableCollection<Stock>)serializer.Deserialize(textReader);
        }

        private ObservableCollection<Stock> LoadStocksFromEmbeddedResource()
        {
            ObservableCollection<Stock> localStocks = null;

            // load file from embedded resource
            var assembly = typeof(HomePage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("App2.Stocks.xml");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Stock>));
                localStocks = (ObservableCollection<Stock>)serializer.Deserialize(reader);
            }
            return localStocks;
        }
    }
}
