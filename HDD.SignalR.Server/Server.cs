#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion 

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;

namespace HDD.SignalR.Server
{
    public class Server : IServer
    {
        private Uri _uri;
        private IDisposable _server;

        public Server(Uri uri)
        {
            _uri = uri;
        }
        
        public void Start(IApplicationContext context)
        {
            MessageHub.SetContext(context);
            _server = WebApp.Start<Startup>(_uri.AbsoluteUri);
            Console.WriteLine("Server running on {0}", _uri.AbsoluteUri);
        }

        public void SendMessage(string message)
        {
            GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients.All.SendMessage(message);
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
                if (_server != null)
                {
                    _server.Dispose();
                }
            }
        }

        #endregion
    }
}