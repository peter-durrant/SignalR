#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.Utility;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using TransportType = HDD.SignalR.Client.Enums.TransportType;

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

        public bool Connect()
        {
            _connection.Start().Wait();
            return _connection.State == ConnectionState.Connected;
        }

        public bool Connect(Enums.TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.LongPolling:
                    _connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.LongPollingTransport()).Wait();
                    break;
                case TransportType.ServerSentEvents:
                    _connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.ServerSentEventsTransport()).Wait();
                    break;
                case TransportType.WebSockets:
                    _connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.WebSocketTransport()).Wait();
                    break;
                default:
                    _connection.Start().Wait();
                    break;
            }

            return _connection.State == ConnectionState.Connected;
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
                    _connection.Stop(new TimeSpan(0));
                    _connection = null;
                }
            }
        }

        #endregion

        #region IMessageHubProxy

        public event EventHandler<EventArgs<string>> OnMessage;
        
        #endregion
    }
}