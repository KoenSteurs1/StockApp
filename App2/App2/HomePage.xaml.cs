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
        public ObservableCollection<Stock> stocks { get; set; }
        public HomePage()
        {
            InitializeComponent();
            LoadStocks();
        }

        async void Button_OnClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddStock());
        }

        async void SaveStocks()
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
        }

        //void ButtonSave_OnClicked(object sender, EventArgs args)
        //{
        //    SaveStocks();
        //}

        private void LoadStocksFromEmbeddedResource()
        {
            // load file from added resource
            var assembly = typeof(HomePage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("App2.Stocks.xml");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Stock>));
                stocks = (ObservableCollection<Stock>)serializer.Deserialize(reader);
            }

            lstView.ItemsSource = stocks;
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

        private async void LoadStocksFromLocalFile()
        {
            // load file from local file system
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync("StocksApp");
            IFile file = await folder.GetFileAsync("Stocks.xml");
            string fileInput = await file.ReadAllTextAsync();

            TextReader textReader = new StringReader(fileInput);

            var serializer = new XmlSerializer(typeof(ObservableCollection<Stock>));
            stocks = (ObservableCollection<Stock>)serializer.Deserialize(textReader);

            lstView.ItemsSource = stocks;
        }

        async void LoadStocks()
        {
            if (await CheckLocalFileExists() == true)
                LoadStocksFromLocalFile();
            else
                LoadStocksFromEmbeddedResource();
        }
    }
}
