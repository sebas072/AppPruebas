using appPrueba.Models;
using appPrueba.Services;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace appPrueba.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        public IDataStore<Log> DataStore => DependencyService.Get<IDataStore<Log>>() ?? new LogDataStore();
        public ObservableCollection<Log> Items { get; set; }
        public bool isVisibledError { get; set; }
        public bool isVisibled { get; set; }
        public LogViewModel(Client cliet)
        {
            Title = "Historial";
            Items = new ObservableCollection<Log>();
            LoadLogs(cliet);
        }
        private async void LoadLogs(Client c)
        {
            var ItemsGet = (await DataStore.GetItemsAsync()).Where(s=> s.id_Client == c.id).ToList();
            if (ItemsGet.Count > 0)
            {
                isVisibled = true;
                isVisibledError = false;
                foreach (var i in ItemsGet)
                    Items.Add(i);
            }
            else {
                isVisibled = false;
                isVisibledError = true;
            }            
        }
    }
}
