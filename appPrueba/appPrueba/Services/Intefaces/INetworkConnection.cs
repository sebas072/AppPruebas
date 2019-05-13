using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Services
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}
