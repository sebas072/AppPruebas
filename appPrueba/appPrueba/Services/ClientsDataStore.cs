using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appPrueba.Data;
using appPrueba.Models;

namespace appPrueba.Services
{
    public class ClientsDataStore : IDataStore<Client>
    {
        private DataAccess dataAccess;   
        public ClientsDataStore()
        {
            dataAccess = new DataAccess();
        }
        public async Task<bool> AddItemAsync(Client item)
        {
            dataAccess.Insert(item);
            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Client item)
        {
            dataAccess.Update(item);
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = dataAccess.GetList<Client>().FirstOrDefault(s => s.id == id);
            if (oldItem != null)
            {
                dataAccess.Delete(oldItem);
            }  
            return await Task.FromResult(true);
        }
        public async Task<Client> GetItemAsync(string id,string p = null)
        {
            return await Task.FromResult(dataAccess.GetList<Client>().FirstOrDefault(s => s.id == id ));
        }

        public async Task<ICollection<Client>> GetItemsAsync()
        {
            return await Task.FromResult(dataAccess.GetList<Client>());
        }
    }
}