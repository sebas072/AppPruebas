using appPrueba.Data;
using appPrueba.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace appPrueba.Services
{
    public class LogDataStore : IDataStore<Log>
    {
        private DataAccess dataAccess;
        public LogDataStore()
        {
            dataAccess = new DataAccess();
        }
        public async Task<bool> AddItemAsync(Log item)
        {
            dataAccess.Insert(item);
            return await Task.FromResult(true);
        }
        public async Task<ICollection<Log>> GetItemsAsync()
        {
            return await Task.FromResult(dataAccess.GetList<Log>());
        }
        #region Methos not using
        public Task<bool> UpdateItemAsync(Log item)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Log> GetItemAsync(string id, string p = null)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
