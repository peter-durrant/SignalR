using System.Net;
using System.Net.Sockets;

namespace HDD.Utility
{
    public static class Network
    {
        public static int GetTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}
