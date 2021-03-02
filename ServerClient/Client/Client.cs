using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Client
    {
        private static readonly IPAddress IpHost = IPAddress.Parse("127.0.0.1");
        private static readonly IPEndPoint LocalEndPoint = new IPEndPoint(IpHost, 9000);

        public void RunClient()
        {
         
            string requestGET = "GET / HTTP/1.1\r\nHost: " + IpHost +
                                "\r\nConnection: Close\r\n\r\n";

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(LocalEndPoint);

            if (clientSocket.Connected)
            {
                Console.WriteLine("Connected to server");
            }
            else
            {
                throw new Exception("Failed to connect to server");

            }

            Byte[] requestBytes = Encoding.ASCII.GetBytes(requestGET);

            clientSocket.Send(requestBytes, requestBytes.Length, 0);

            int bytes = 0;
            Byte[] bytesReceived = new Byte[254];
            StringBuilder sb = new StringBuilder();

            do
            {
                bytes = clientSocket.Receive(bytesReceived, bytesReceived.Length, 0);
                sb.Append(Encoding.ASCII.GetString(bytesReceived), 0, bytes);
            } while (bytes > 0);

            Console.WriteLine(sb.ToString());
            clientSocket.Close();
            Console.ReadKey();
        }

    }
}
