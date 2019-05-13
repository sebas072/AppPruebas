using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using appPrueba.Droid;
using appPrueba.Services;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLitePlatform))]
namespace appPrueba.Droid
{
    public class AndroidSQLitePlatform : ISQLitePlatform
    {
        private string GetPath()
        {
            var dbName = "appPrueba.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return path;
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }
        public SQLiteAsyncConnection GetConnectionAsync()
        {
            return new SQLiteAsyncConnection(GetPath());
        }
    }
}