using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Server
    {

        public const String MsgDir = "/msg/";
        public const String WebDir = "/web/";
        public const String Version = "HTTP/1.1";

        private bool _running = false;
        public Socket clientSocket;

        private static readonly IPAddress IpHost = IPAddress.Parse("127.0.0.1");
        private static readonly IPEndPoint LocalEndPoint = new IPEndPoint(IpHost, 9000);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void ServerStart()
        {
            Console.WriteLine("Starting server on: 127.0.0.1:9000");
            Thread serverThread = new Thread(Run);
            serverThread.Start();
        }


        private void Run()
        {
            _running = true;

            try
            {
                listener.Bind(LocalEndPoint);
                listener.Listen(10);

                while (_running)
                {
                    ThreadPool.QueueUserWorkItem(HandleClient);
                }

                _running = false;
                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void HandleClient(object obj)
        {
            clientSocket = listener.Accept();
            NetworkStream clientNetworkStream = new NetworkStream(clientSocket);
            StreamReader reader = new StreamReader(clientNetworkStream);

            String msg = "";

            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
            }

            Console.WriteLine("Request: \n " + msg);

            Request request = Request.GetRequest(msg);
            Response response = Response.From(request);

            response.Header(clientNetworkStream);

            clientSocket.Close();
        }
    }
}