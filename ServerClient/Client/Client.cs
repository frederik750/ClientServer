using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Client
    {
        private static TcpClient _client;
        private static StreamReader _sReader;
        private static StreamWriter _sWriter;

        private static Boolean _isConnected;


        public static void RunClient(String ipAddress, int portNum)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);
            HandleCommunication();
        }
        public static void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);
            _isConnected = true;
            String sData = null;
            while (_isConnected)
            {
                Console.Write("> ");
                sData = Console.ReadLine();
                // write data and make sure to flush, or the buffer will continue to 
                // grow, and your data might not be sent when you want it, and will
                // only be sent once the buffer is filled.
                _sWriter.WriteLine(sData);
                _sWriter.Flush();
                // if you want to receive anything
                //String sDataIncomming = _sReader.ReadLine();
            }
        }
        /* public static bool Connected = false;

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


                 byte[] messageSent = Encoding.ASCII.GetBytes("Test Message from client <EOF>");

                 NetworkStream stream = new NetworkStream(sender);


                  int byteSent = sender.Send(messageSent);


                  byte[] buffer = new byte[1024];

                  int byteReceived = sender.Receive(buffer);
                  Console.WriteLine("Message from Server -> {0}",
                      Encoding.ASCII.GetString(buffer,
                          0, byteReceived));

                  sender.Shutdown(SocketShutdown.Both);

                  sender.Close();
                  sender.Close();
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
             }
         }*/
    }
}
