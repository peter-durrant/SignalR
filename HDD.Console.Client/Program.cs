#region Copyright (c) 2015, P Durrant

// Copyright (c) 2015, P Durrant
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice,
//    this list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

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