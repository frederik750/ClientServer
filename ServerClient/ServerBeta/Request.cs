﻿using System;

namespace ServerBeta
{
    public class Request
    {
        public String Type { get; set; }

        public String Url { get; set; }

        public String Host { get; set; }

    
    
        private Request(String type, String url, String host)
        {
            Type = type;
            this.Url = url;
            Host = host;
        }

        public static Request GetRequest(String request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return null;
            }

            String[] tokens = request.Split(' ');

            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[4];

            return new Request(type,url,host);
        }
    }
}