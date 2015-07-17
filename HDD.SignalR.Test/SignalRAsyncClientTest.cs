#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.SignalR.Server;
using HDD.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransportType = HDD.SignalR.Client.Enums.TransportType;

namespace HDD.SignalR.Test
{
    [TestClass]
    public class SignalRAsyncClientTest
    {
        [TestMethod]
        public async Task AsyncClient_ConnectToServer_ConnectionSucceeds()
        {
            var port = Network.GetTcpPort();
            var uriString = string.Format("http://localhost:{0}", port);
            var uri = new Uri(uriString);
                
            var applicationContext = Substitute.For<IApplicationContext>();
            using (var server = new Server.Server(uri))
            {
                server.Start(applicationContext);

                using (var client = new Client.AsyncClient(uri))
                {
                    await client.Connect(Client.Enums.TransportType.ServerSentEvents);
                }
            }
        }
    }
}
