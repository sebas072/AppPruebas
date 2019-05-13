using appPrueba.Models;
using appPrueba.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace appPrueba.Data
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection db;

        public DataAccess()
        {
            var platform = DependencyService.Get<ISQLitePlatform>();
            db = platform.GetConnection();
            db.CreateTable<Log>();
            db.CreateTable<Client>();
            db.CreateTable<Login>();
        }
        public void Insert<T>(T model)
        {
            db.Insert(model);
        }
        public void Update<T>(T model)
        {
            db.Update(model);
        }
        public void Delete<T>(T model)
        {
            db.Delete(model);
        }
        public List<T> GetList<T>() where T : new()
        {
            return db.Table<T>().ToList();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
