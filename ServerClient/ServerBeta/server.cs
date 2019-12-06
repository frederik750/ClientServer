
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ServerBeta;

class server
{

    public const String MsgDir = "/msg/";
    public const String WebDir = "/web/";
    public const String Version = "HTTP/1.1";
    public const String Name = "Web-Server";

    private bool _running = false;

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
                Console.WriteLine("Waiting for connection");

                Socket client = listener.Accept();


                HandleClient(client);

                client.Close();
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

    private void HandleClient(Socket clientSocket)
    {
        NetworkStream clientNetworkStream = new NetworkStream(clientSocket);
        StreamReader reader = new StreamReader(clientNetworkStream);


        String msg = "";

        while (reader.Peek() != -1)
        {
            msg += reader.ReadLine() + "\n";
        }

        Debug.WriteLine("Request: \n " + msg);

        Request request = Request.GetRequest(msg);
        Response response = Response.From(request);

        response.Post(clientNetworkStream);
    }
}