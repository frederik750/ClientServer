﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Client client = new Client();
            client.RunClient();
        }
    }
}