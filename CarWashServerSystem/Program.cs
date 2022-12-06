using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CarWashServerSystem.Models;
using Newtonsoft.Json;
using System.Runtime.InteropServices.ComTypes;

namespace CarWashServerSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            JsonConverter jsonConverter = new JsonConverter();
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);
            Console.WriteLine("Server Start.");
            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);

                if (data.ToString() =="Get")
                {
                    Console.WriteLine("Sending all clients from server");
                    listener.Send(Encoding.UTF8.GetBytes(jsonConverter.GetObject()));
                }
                else if(Int32.TryParse(data.ToString(), out int clientNumber))
                {
                    Console.WriteLine("Delete client with id: "+ clientNumber);
                    jsonConverter.DeleteObject(clientNumber);
                }
                else
                {
                    T tempClient = JsonConvert.DeserializeObject<T>(data.ToString());
                    if (tempClient != null)
                    Console.WriteLine("New Client Added with CarNumber: " + tempClient.CarNumber);
                    jsonConverter.AddObject(tempClient);
                }

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();

            }
        }
        }
}
