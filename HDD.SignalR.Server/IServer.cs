#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using System;

namespace HDD.SignalR.Server
{
    interface IServer : IDisposable
    {
        /// <summary>
        /// Start a server
        /// </summary>
        /// <param name="context">Reference to the application context so that hubs can access application state</param>
        void Start(IApplicationContext context);

        /// <summary>
        /// Send a message to all connected clients
        /// </summary>
        /// <param name="message">The message to send</param>
        void SendMessage(string message);
    }
}