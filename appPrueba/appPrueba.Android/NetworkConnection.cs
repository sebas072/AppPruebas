using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using appPrueba.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(appPrueba.Droid.NetworkConnection))]
namespace appPrueba.Droid
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            IsConnected = connectivityManager.ActiveNetworkInfo != null && connectivityManager.ActiveNetworkInfo.IsConnected;
        }
    }
}