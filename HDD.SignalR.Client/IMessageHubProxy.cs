#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using System;
using HDD.Utility;

namespace HDD.SignalR.Client
{
    interface IMessageHubProxy
    {
        /// <summary>
        /// Event fired when the client receives a new message
        /// </summary>
        event EventHandler<EventArgs<string>> OnMessage;
    }
}