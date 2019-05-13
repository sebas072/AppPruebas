using appPrueba.Data;
using appPrueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appPrueba.Services
{
    public class UserDtaStore : IDataStore<Login>
    {
        private DataAccess dataAccess;
        public UserDtaStore()
        {
            dataAccess = new DataAccess();
        }
        public  Login login { get; set; }
        public async Task<bool> AddItemAsync(Login item)
        {
            dataAccess.Insert(item);
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = dataAccess.GetList<Login>().FirstOrDefault(s=>s.email == id);
            if (oldItem != null)
            {
                dataAccess.Delete(oldItem);
            }
            return await Task.FromResult(true);
        }
        public async Task<Login> GetItemAsync(string id, string p = null)
        {
            return await Task.FromResult(dataAccess.GetList<Login>().FirstOrDefault(s => s.email == id && s.Pass == p));   
        }
        public async Task<ICollection<Login>> GetItemsAsync()
        {
            return await Task.FromResult(dataAccess.GetList<Login>());
        }
        public async Task<bool> UpdateItemAsync(Login item)
        {
            dataAccess.Update(item);
            return await Task.FromResult(true);
        }
    }
}
