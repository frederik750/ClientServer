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
                String fileDir = Environment.CurrentDirectory + server.WebDir + request.Url;
                FileInfo file = new FileInfo(fileDir);
                if (file.Exists && file.Extension.Contains("."))
                {
                    return MakeFromFile(file);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(file + "/");
                    if (!di.Exists)
                    {
                        return MakePageNotFoundRequest();
                    }
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo ff in files)
                    {
                        string name = ff.Name;
                        if (name.Contains("default.html") || name.Contains("default.htm") ||
                            name.Contains("index.html") || name.Contains("index.htm"))
                        {
                            file = ff;
                            return MakeFromFile(ff);
                        }
                    }

                }

                if (!file.Exists)
                {
                    return MakePageNotFoundRequest();
                }
            }
            else
            {
                return MakePageMethodNotAllowedResponse();
            }



            return MakePageNotFoundRequest();
        }

        private static Response MakeFromFile(FileInfo file)
        {
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);
            fs.Close();

            return new Response("200 OK", "text/html", dataBytes);
        }

        private static Response MakeNullRequest()
        {
            String fileDir = Environment.CurrentDirectory + server.MsgDir + "400.html";
            FileInfo file = new FileInfo(fileDir);
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);
            fs.Close();

            return new Response("400 Bad Request", "text/html", dataBytes);
        }

        private static Response MakePageNotFoundRequest()
        {
            String fileDir = Environment.CurrentDirectory + server.MsgDir + "404.html";
            FileInfo file = new FileInfo(fileDir);
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);
            fs.Close();

            return new Response("400 Bad Request", "text/html", dataBytes);
        }

        private static Response MakePageMethodNotAllowedResponse()
        {
            String fileDir = Environment.CurrentDirectory + server.MsgDir + "405.html";
            FileInfo file = new FileInfo(fileDir);
            FileStream fs = file.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] dataBytes = new byte[fs.Length];
            reader.Read(dataBytes, 0, dataBytes.Length);
            fs.Close();

            return new Response("400 Bad Request", "text/html", dataBytes);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(
                $"{server.Version} {status}\r\nServer: {server.Name}\r\nContent-Type: {mime}\r\nAccept-Ranges: bytes\r\nContent-Length: {data.Length}\r\n");
            writer.Flush();
            stream.Write(data, 0, data.Length);
        }
    }
}