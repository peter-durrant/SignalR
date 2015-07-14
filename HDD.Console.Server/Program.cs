#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.SignalR.Server;
using System;

namespace HDD.Console.Server
{
    class Program
    {
        static IApplicationContext _context = new ApplicationContext();

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            var uri = new Uri("http://localhost:8080");
            using (var server = new SignalR.Server.Server(uri))
            {
                server.Start(_context);

                while (true)
                {
                    System.Console.WriteLine("Send a message (or type QUIT to exit)");
                    string message = System.Console.ReadLine();
                    if (message == "QUIT")
                    {
                        break;
                    }
                    else
                    {
                        server.SendMessage(message);
                    }
                }
            }
        }
    }
}