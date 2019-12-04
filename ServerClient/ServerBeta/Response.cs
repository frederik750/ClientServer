using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace ServerBeta
{
    public class Response
    {

        private Byte[] data = null;
        private String status;
        private String mime;

        private Response(String status, String mime, Byte[] data)
        {
            this.status = status;
            this.mime = mime;
            this.data = data;
        }

        public static Response From(Request request)
        {
            if (request == null)
            {
                return MakeNullRequest();; 
            }

            if (request.Type == "GET")
            {
                String fileDir = Environment.CurrentDirectory + Server.WebDir + request.Url;
                FileInfo file = new FileInfo(fileDir);
                if (file.Exists && file.Extension.Contains("."))
                {

                }
            }
            else
            {
                return makePageMethodNotAllowedResponse();
            }

            return null;
        }

        private static Response MakeNullRequest()
        {
            String fileDir = Environment.CurrentDirectory + Server.MsgDir + "400.html/";
            FileInfo file = new FileInfo(fileDir);
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);


            return new Response("400 Bad Request", "text/html", dataBytes);
        }

        private static Response MakePageNotFoundRequest()
        {
            String fileDir = Environment.CurrentDirectory + Server.MsgDir + "404.html/";
            FileInfo file = new FileInfo(fileDir);
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);


            return new Response("400 Bad Request", "text/html", dataBytes);
        }

        private static Response makePageMethodNotAllowedResponse()
        {
            String fileDir = Environment.CurrentDirectory + Server.MsgDir + "405.html/";
            FileInfo file = new FileInfo(fileDir);
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);


            return new Response("400 Bad Request", "text/html", dataBytes);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Flush();
            stream.Write(data, 0, data.Length);
            writer.WriteLine(
                $"{Server.Version} {status}\r\nServer: {Server.Name}\r\nContent-Type: {mime}\r\nAccept-Ranges: bytes\r\nContent-Length: {data.Length}\r\n");
        }
    }
}