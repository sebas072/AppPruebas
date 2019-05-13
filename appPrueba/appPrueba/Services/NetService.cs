using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace appPrueba.Services
{
    public class NetService
    {
        public bool IsConected()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return networkConnection.IsConnected;
        }
    }
}
