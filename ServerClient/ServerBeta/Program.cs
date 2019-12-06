using System;

namespace ServerBeta
{
    class Program
    {
        static void Main(string[] args)
        {
            server server = new server();
            server.ServerStart();;
        }
    }
}
