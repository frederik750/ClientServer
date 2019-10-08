using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal static class Server
    {
        public static void StartListener()
        {
            IPAddress iphost = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(iphost, 9000);

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
