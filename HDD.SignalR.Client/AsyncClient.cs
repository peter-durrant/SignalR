#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.Utility;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportType = HDD.SignalR.Client.Enums.TransportType;

namespace HDD.SignalR.Client
{
    public class AsyncClient : IAsyncClient, IMessageHubProxy
    {
        private HubConnection _connection;

        public AsyncClient(Uri uri)
        {
            var queryStringParameters = new Dictionary<string, string>();
            queryStringParameters.Add("token", "secret");
         
            _connection = new HubConnection(uri.AbsoluteUri, queryStringParameters);
            
            CreateHubProxies();
        }

        public async Task Connect()
        {
            await _connection.Start();
        }

        public async Task Connect(Enums.TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.LongPolling:
                    await _connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.LongPollingTransport());
                    break;
                case TransportType.ServerSentEvents:
                    await _connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.ServerSentEventsTransport());
                    break;
                case TransportType.WebSockets:
                    await _connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.WebSocketTransport());
                    break;
                default:
                    await _connection.Start();
                    break;
            }
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