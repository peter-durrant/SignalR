using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace HDD.Utility
{
    public static class Network
    {
        public static int GetTcpPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public static List<string> GetHostIpAddresses()
        {
            var ipAddresses = new List<string>();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                ipAddresses.Add(ip.ToString());
            }
            return ipAddresses;
        }
    }
}
