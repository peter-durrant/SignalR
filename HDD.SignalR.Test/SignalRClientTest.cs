#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.SignalR.Server;
using HDD.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace HDD.SignalR.Test
{
    [TestClass]
    public class SignalRClientTest
    {
        [TestMethod]
        public void Client_ConnectToServer_ConnectionSucceeds()
        {
            var port = Network.GetTcpPort();
            var uriString = string.Format("http://localhost:{0}", port);
            var uri = new Uri(uriString);

            var applicationContext = Substitute.For<IApplicationContext>();
            using (var server = new Server.Server(uri))
            {
                server.Start(applicationContext);

                using (var client = new Client.Client(uri))
                {
                    var connected = client.Connect(Client.Enums.TransportType.ServerSentEvents);
                    Assert.IsTrue(connected);
                }
            }
        }

        [TestMethod]
        public void Client_ConnectMultipleClientsToServer_ConnectionsSucceed()
        {
            var port = Network.GetTcpPort();
            var uriString = string.Format("http://localhost:{0}", port);
            var uri = new Uri(uriString);

            var applicationContext = Substitute.For<IApplicationContext>();
            using (var server = new Server.Server(uri))
            {
                server.Start(applicationContext);

                var clients = new List<Client.Client>();
                const int NumClients = 100;
                var clientReceivedMessageCount = 0;
                for (var i = 0; i < NumClients; i++)
                {
                    var client = new Client.Client(uri);
                    client.OnMessage += (sender, eventArgs) => clientReceivedMessageCount++;
                    var connected = client.Connect(Client.Enums.TransportType.ServerSentEvents);
                    Assert.IsTrue(connected);
                    clients.Add(client);
                }
                Assert.AreEqual(NumClients, clients.Count);
                server.SendMessage("x");
                foreach (var client in clients)
                {
                    client.Dispose();
                }

                // Probably won't receive all messages - no sleep or synchronisation implemented to ensure exactly NumClients messages
                Assert.IsTrue(clientReceivedMessageCount > 0);
                Assert.IsTrue(clientReceivedMessageCount <= NumClients);
            }
        }

        [TestMethod]
        public void Client_ConnectToServerThenTerminateServer_ConnectionSucceeds()
        {
            var port = Network.GetTcpPort();
            var uriString = string.Format("http://localhost:{0}", port);
            var uri = new Uri(uriString);

            var applicationContext = Substitute.For<IApplicationContext>();
            var server = new Server.Server(uri);
            server.Start(applicationContext);

            using (var client = new Client.Client(uri))
            {
                var connected = client.Connect(Client.Enums.TransportType.ServerSentEvents);
                Assert.IsTrue(connected);
                server.Dispose();
            }
        }
    }
}