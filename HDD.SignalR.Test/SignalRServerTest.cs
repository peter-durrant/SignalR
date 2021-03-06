﻿#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.SignalR.Server;
using HDD.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace HDD.SignalR.Test
{
    [TestClass]
    public class SignalRServerTest
    {
        [TestMethod]
        public void Server_CreateAndStartServer_Starts()
        {
            var port = Network.GetTcpPort();
            var uriString = string.Format("http://localhost:{0}", port);
            var uri = new Uri(uriString);

            var applicationContext = Substitute.For<IApplicationContext>();
            using (var server = new Server.Server(uri))
            {
                server.Start(applicationContext);
            }
        }
    }
}