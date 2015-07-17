#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using Microsoft.AspNet.SignalR;
using Owin;

namespace HDD.SignalR.Server
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration { Resolver = new DefaultDependencyResolver() };
            app.MapSignalR(config);
        }
    }
}