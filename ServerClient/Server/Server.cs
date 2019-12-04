using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
                    Console.WriteLine("Waiting for connection");

                    Socket client = listener.Accept();

                    byte[] buffer = new byte[1024];
                    string data = null;

                    while (true)
                    {
                        int numByte = client.Receive(buffer);

                        data += Encoding.ASCII.GetString(buffer, 0, numByte);

                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("Text received -> {0} ", data);
                    byte[] message = Encoding.ASCII.GetBytes("Test");

                    client.Send(message);

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
