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