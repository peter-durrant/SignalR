#region Copyright (c) 2015, P Durrant

// Copyright (c) 2015, P Durrant
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice,
//    this list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

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
