using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        public void Connect()
        {
            IPAddress localHost = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localHost, 8000);

            server.Start();
            Console.WriteLine("Server is listening on port 8000");
          

        }
    }
}
