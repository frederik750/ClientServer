using System;
using System.Threading;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            Thread serverThread = new Thread(new ThreadStart(Server.StartListener));
            serverThread.Start();
          
        }
    }
}
