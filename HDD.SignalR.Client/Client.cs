#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.Utility;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;

namespace HDD.SignalR.Client
{
    public class Client : IClient, IMessageHubProxy
    {
        private HubConnection _connection;

        public Client(Uri uri)
        {
            var queryStringParameters = new Dictionary<string, string>();
            queryStringParameters.Add("token", "secret");
         
            _connection = new HubConnection(uri.AbsoluteUri, queryStringParameters);
            
            CreateHubProxies();
        }

        public void Connect()
        {
            _connection.Start().Wait();
        }

        private void CreateHubProxies()
        {
            var proxy = _connection.CreateHubProxy("MessageHub");
            proxy.On<string>("SendMessage", RxMessage);
        }

        private void RxMessage(string message)
        {
            OnMessage.RaiseEvent<string>(this, message);
        }

        #region Dispose

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _disposed = true;
                if (_connection != null)
                {
                    _connection.Dispose();
                }
            }
        }

        #endregion

        #region IMessageHubProxy

        public event EventHandler<EventArgs<string>> OnMessage;
        
        #endregion
    }
}