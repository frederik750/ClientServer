using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketEx
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 9000;
            IPAddress IpHost = IPAddress.Parse("127.0.0.1");
            IPEndPoint LocalEndPoint = new IPEndPoint(IpHost, port);

        string request = "GET / HTTP/1.1\r\nHost: " + IpHost +
                             "\r\nConnection: Close\r\n\r\n";

            Byte[] requestBytes = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];


            using var socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(LocalEndPoint);

            if (socket.Connected)
            {
                Console.WriteLine("Connection established");
            }
            else
            {
                Console.WriteLine("Connection failed");
                return;
            }

            socket.Send(requestBytes, requestBytes.Length, 0);

            int bytes = 0;
            var sb = new StringBuilder();

            do
            {
                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes));
            }
            while (bytes > 0);

            Console.WriteLine(sb.ToString());

        }
    }
}