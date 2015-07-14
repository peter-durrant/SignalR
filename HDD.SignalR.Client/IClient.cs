#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using System;

namespace HDD.SignalR.Client
{
    interface IClient : IDisposable
    {
        /// <summary>
        /// Connect to a server
        /// </summary>
        void Connect();
    }
}