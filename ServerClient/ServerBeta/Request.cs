using System;

namespace Server
{
    public class Request
    {
        public String Type { get; set; }

        public String Url { get; set; }

        public String Host { get; set; }

        public String Referer { get; set; }

    
    
        private Request(String type, String url, String host, String referer)
        {
            Type = type;
            Url = url;
            Host = host;
            Referer = referer;
        }

        public static Request GetRequest(String request)
        {
            if (string.IsNullOrEmpty(request))
            {
                Console.WriteLine("Request is empty");
                return null;
            }

            String[] tokens = request.Split(' ', '\n');

            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[4];
            String referer = "";

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "Referer:")
                {
                    referer = tokens[i + 1];
                    break;
                }
            }

            return new Request(type,url,host, referer);
        }
    }
}