using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using appPrueba.Models;
using appPrueba.Views;
using appPrueba.Services;
using System.Collections.Generic;

namespace appPrueba.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        public string authToken { get; set; }
        public IDataStore<Client> DataStore => DependencyService.Get<IDataStore<Client>>() ?? new ClientsDataStore();
        public ObservableCollection<Client> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ClientsViewModel()
        {
            Title = "Clientes";
            Items = new ObservableCollection<Client>();
            LoadClients();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        private async void LoadClients()
        {
            IDataStore<Login> da = DependencyService.Get<IDataStore<Login>>() ?? new UserDtaStore();
            var user = await da.GetItemsAsync();
            Login us = new Login();
            foreach (var u in user)
                us = u;
            authToken = us.authToken;
            await addClients();
        }
        private async Task addClients()
        {
            if (netService.IsConected())
            {
                var response = await apiService.APICosumeGet<List<Client>>("sch/prospects.json", authToken);
                if (response.IsSucces)
                {
                    foreach (var it in (List<Client>)response.Result)
                    {
                        if (!await isExist(it))
                        {
                            await DataStore.AddItemAsync(it);
                        }
                    } 
                }
            }
            Items.Clear();
            var itemsGet = await DataStore.GetItemsAsync();
            foreach (var it in itemsGet)
                Items.Add(SetIcionStatus(it));
        }

        private  Client SetIcionStatus(Client c)
        {
            switch ((StatusCode)c.statusCd)
            {
                case StatusCode.pending:
                    c.img = "pending.png";
                    break;
                case StatusCode.approved:
                    c.img = "approved.png";
                    break;
                case StatusCode.accepted:
                    c.img = "accepted.png";
                    break;
                case StatusCode.rejected:
                    c.img = "rejected.png";
                    break;
                case StatusCode.disabled:
                    c.img = "disabled.png";
                    break;
            }
            return c;
        }
        private async Task<bool> isExist(Client it)
        {
            return await Task.FromResult(DataStore.GetItemAsync(it.id) != null);
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                await addClients();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}