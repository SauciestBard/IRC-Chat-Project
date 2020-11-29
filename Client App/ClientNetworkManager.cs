﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client_App
{
    class ClientNetworkManager
    {
        String ServerIP;
        readonly Int32 Port = 6667;

        public bool ReadServerIP()
        {
            string UserInput;
            Console.WriteLine("Enter Server IP (If local, use 127.0.0.1): ");
            UserInput = Console.ReadLine();
            bool ValidateIp = IPAddress.TryParse(UserInput, out _);
            if (ValidateIp)
            {
                Console.WriteLine("Valid");
                ServerIP = UserInput;
                return true;
            }
            else
            {
                Console.WriteLine("Invalid");
                return false;
            }
                
        }

        public String Send(string Message)
        {
            string responceData = "";
            try
            {
                var client = new TcpClient(ServerIP, Port);
                NetworkStream Stream = client.GetStream();

                byte[] data = Encoding.ASCII.GetBytes(Message);
                Stream.Write(data, 0, data.Length);
                //Console.WriteLine("Send message: " + Message);

                data = new byte[256];
                responceData = String.Empty;

                int i;
                while ((i = Stream.Read(data, 0, data.Length)) != 0)
                {
                    responceData = responceData + Encoding.ASCII.GetString(data, 0, i);
                }
                
                //int bytes = Stream.Read(data, 0, data.Length);
                //responceData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Recived Message: " + responceData);

                Stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return responceData;
        }
    }
}
