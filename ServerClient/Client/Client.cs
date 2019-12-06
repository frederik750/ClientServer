using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal static class Client
    {
        public static bool Connected = false;

        public static void RunClient()
        {
            IPAddress iphost = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEndPoint = new IPEndPoint(iphost, 9000);

            Socket sender = new Socket(iphost.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Console.WriteLine("Looking for connection");

                sender.Connect(remoteEndPoint);
                Connected = true;

                Console.WriteLine("Connected to -> {0}", sender.RemoteEndPoint);


                NetworkStream serverStream = new NetworkStream(sender);
                StreamReader reader = new StreamReader(serverStream);


                String msg = "";

                while (reader.Peek() != -1)
                {
                    msg += reader.ReadLine() + "\n";
                }

                Console.WriteLine(msg);
                /* byte[] messageSent = Encoding.ASCII.GetBytes("Test Message from client <EOF>");
                 int byteSent = sender.Send(messageSent);


                 byte[] buffer = new byte[1024];

                 int byteReceived = sender.Receive(buffer);
                 Console.WriteLine("Message from Server -> {0}",
                     Encoding.ASCII.GetString(buffer,
                         0, byteReceived));

                 sender.Shutdown(SocketShutdown.Both);

                 sender.Close();*/

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
