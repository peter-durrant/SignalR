#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using System;
using System.Threading.Tasks;

namespace HDD.SignalR.Client
{
    interface IAsyncClient : IDisposable
    {
        /// <summary>
        /// Connect to a server
        /// </summary>
        Task<bool> Connect();

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="transportType">The transport mechanism to use</param>
        /// <returns>The async task - returns true if connected, otherwise false</returns>
        Task<bool> Connect(Enums.TransportType transportType);
    }
}