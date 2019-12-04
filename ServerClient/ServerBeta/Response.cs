using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace ServerBeta
{
    public class Response
    {

        private Byte[] buffer = null;

        private Response(String ,Byte[] buffer)
        {

        }

        public static Response From(Request request)
        {

        }

        public void Post(NetworkStream stream)
        {
            stream.Write();
        }
    }
}