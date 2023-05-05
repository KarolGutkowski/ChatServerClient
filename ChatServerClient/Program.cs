using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatServerClient // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static TcpClient client = new();
        static IPEndPoint server = new(new IPAddress(new byte[] { 127, 0, 0, 1 }), 10_000);
        static void Main(string[] args)
        {
            client.ConnectAsync(server);
            NetworkStream stream = client.GetStream();

            string? toSend = string.Empty;
            while (toSend != "quit()")
            {
                var buffer = new byte[1_024];
            int received = stream.Read(buffer);

            var message = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"Message received: \"{message}\"");



            
                toSend = Console.ReadLine();
                if (toSend is null) break;
                var bytesToSend = Encoding.UTF8.GetBytes(toSend);
                stream.Write(bytesToSend);
            }

            stream.Close();
            Console.Read();
        }
    }
}