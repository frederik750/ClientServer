using System;

namespace ServerBeta
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.ServerStart();;
        }
    }
}
