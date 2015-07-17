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
        /// <returns>true if connected, otherwise false</returns>
        bool Connect();

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="transportType">The transport mechanism to use</param>
        /// <returns>true if connected, otherwise false</returns>
        bool Connect(Enums.TransportType transportType);
    }
}