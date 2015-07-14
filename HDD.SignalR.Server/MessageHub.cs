#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using Microsoft.AspNet.SignalR;

namespace HDD.SignalR.Server
{
    [AuthorizeConnection]
    public class MessageHub : Hub
    {
        private static IApplicationContext _context;

        public static void SetContext(IApplicationContext context)
        {
            _context = context;
        }
    }
}