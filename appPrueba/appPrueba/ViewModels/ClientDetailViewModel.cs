using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using appPrueba.Models;
using Xamarin.Forms;
using appPrueba.Services;

namespace appPrueba.ViewModels
{
    public class ClientDetailViewModel : BaseViewModel
    {
        public Client Item { get; set; }
        public IDataStore<Client> DataStore => DependencyService.Get<IDataStore<Client>>() ?? new ClientsDataStore();

        ItemsStatus selectedJStatus;
        public ItemsStatus SelectedStatus
        {
            get { return selectedJStatus; }
            set
            {
                if (selectedJStatus != value)
                {
                    selectedJStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<ItemsStatus> itemsStatus { get; set; }
        public Command SaveClient { get; set; }
        public INavigation Navigation { get; internal set; }

        public ClientDetailViewModel(Client item = null)
        {
            itemsStatus = new List<ItemsStatus>
            {
                new ItemsStatus {Id = StatusCode.pending, Title="Pendiente"},
                new ItemsStatus {Id = StatusCode.approved, Title="Aprobado"},
                new ItemsStatus {Id = StatusCode.accepted, Title="Aceptado"},
                new ItemsStatus {Id = StatusCode.rejected, Title="Rechazado"},
                new ItemsStatus {Id = StatusCode.disabled, Title="Inabilitado"}
            };
            this.Item = item;
            SelectedStatus = itemsStatus.FirstOrDefault(s=>s.Id == (StatusCode)item.statusCd);
            SaveClient = new Command(async () => {
                Item.statusCd = (int)SelectedStatus.Id;
                await SaveAuditoria();
                await DataStore.UpdateItemAsync(Item);
                await Navigation.PopToRootAsync();
            });
        }
        private async Task SaveAuditoria()
        {
            IDataStore<Log> da = DependencyService.Get<IDataStore<Log>>() ?? new LogDataStore();
            var ItemGet = await DataStore.GetItemAsync(Item.id);
            if (ItemGet != Item)
            {
                await da.AddItemAsync(new Log { Id = Guid.NewGuid(), descripcion = "Actualizacion de campos", fecha = DateTime.Now, id_Client = Item.id });
            }
        }
    }
}
