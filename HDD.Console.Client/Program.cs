#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.Utility;
using System;

namespace HDD.Console.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            var uri = new Uri("http://localhost:8080");
            using (var client = new SignalR.Client.Client(uri))
            {
                client.OnMessage += client_OnMessage;

                try
                {
                    client.Connect();
                    System.Console.ReadLine();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Exception {0}", e.Message);
                }
            }
        }

        void client_OnMessage(object sender, EventArgs<string> e)
        {
            System.Console.WriteLine("Message received:\n{0}", e.Value);
        }
    }
}