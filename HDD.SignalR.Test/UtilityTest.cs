#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HDD.SignalR.Test
{
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void Network_GetPort_ReturnsUnusedPort()
        {
            var port = Network.GetTcpPort();
            Console.WriteLine("Port: {0}", port);
            Assert.AreNotEqual(0, port);
        }
    }
}