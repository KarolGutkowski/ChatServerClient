using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatServerClient // Note: actual namespace depends on the project name.
{
    internal static class Program
    {
        static TcpClient client = new();
        static IPEndPoint server = new(new IPAddress(new byte[] { 127, 0, 0, 1 }), 10_000);
        static void Main(string[] args)
        {
            try
            {
                client.Connect(server);
            }catch(SocketException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Connected to server!");
            NetworkStream stream = client.GetStream();
            //AuthenticateUser(stream);


            string? toSend = string.Empty;
            ChatManager chatManager = new(client);
            var task = new Task(() => chatManager.ReadMessage());
            task.Start();
            while (toSend != "quit()")
            {
                toSend = Console.ReadLine();
                if (toSend is null) break;
                var bytesToSend = Encoding.UTF8.GetBytes(toSend);
                stream.Write(bytesToSend);

            }

            stream.Close();
            Console.Read();
        }

        public static void AuthenticateUser(NetworkStream stream)
        {
            string? confirm = "N";
            string? login = "";
            string? password = "";
            Console.WriteLine("Welcome in ChatApp!");
            string loginStatus = "FAILED";
            while (loginStatus.ToUpper() != "ACCEPTED")
            {
                do
                {
                    Console.WriteLine("Please enter your credentials.");
                    Console.Write("Login: ");
                    login = Console.ReadLine();
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                    Console.WriteLine("Confirm your entries (Y/N).");
                    confirm = Console.ReadLine();

                } while (confirm?.ToUpper() == "N");
                string message = $"[login]{login}[password]{password}";
                var bytesToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(bytesToSend, 0, bytesToSend.Length);

                var buffer = new byte[1_024];
                stream.Read(buffer);
                loginStatus = Encoding.UTF8.GetString(buffer).ToUpper();
            }
        }
    }
}