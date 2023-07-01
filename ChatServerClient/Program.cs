using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatServerClient // Note: actual namespace depends on the project name.
{
    internal static class Program
    {
        static TcpClient client;
        static IPEndPoint server = new(new IPAddress(new byte[] { 127, 0, 0, 1 }), 10_000);
        static string userName;
        static void Main(string[] args)
        {
            bool accessGained = false;
            NetworkStream stream;
            do
            {
                client = new();
                try
                {
                    client.Connect(server);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                //Console.WriteLine("Connected to server!");
                stream = client.GetStream();
                accessGained = AuthenticateUser(stream);
            } while (!accessGained);

            Console.Clear();
            Console.WriteLine("Chat room:");
            string ? toSend = string.Empty;
            ChatManager chatManager = new(client);
            var task = new Task(() => chatManager.ReadMessage());
            task.Start();
            while (toSend != "quit()")
            {
                StringBuilder message = new();
                toSend = Console.ReadLine();
                message.Append(toSend);
                if (toSend is null) break;
                var bytesToSend = Encoding.UTF8.GetBytes(message.ToString());
                stream.Write(bytesToSend);

            }

            stream.Close();
            Console.Read();
        }

        public static bool AuthenticateUser(NetworkStream stream)
        {
            string? confirm = "N";
            string? login = "";
            string? password = "";
            Console.WriteLine("Welcome in ChatApp!");
            string loginStatus = "FAILED";
           // while (loginStatus.ToUpper() != "ACCEPTED")
           // {
                do
                {
                    Console.WriteLine("Please enter your credentials.");
                    Console.Write("Login: ");
                    login = Console.ReadLine();
                    Console.Write("Password: ");

                    StringBuilder passwordBuilder = new StringBuilder();
                    while (true)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        else if (key.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
                        {
                            passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                            Console.Write("\b \b");

                        }
                        else
                        {
                            passwordBuilder.Append(key.KeyChar);
                            Console.Write("*");
                        }
                    }
                    Console.WriteLine();
                    password = passwordBuilder.ToString();
                    Console.WriteLine("Confirm your entries (Y/N).");
                    confirm = Console.ReadLine();

                } while (confirm?.ToUpper() == "N");
                string message = $"[login]{login}[password]{password}";
                var bytesToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(bytesToSend, 0, bytesToSend.Length);

                var buffer = new byte[1_024];
                int bytesRead = stream.Read(buffer);
                loginStatus = Encoding.UTF8.GetString(buffer,0 , bytesRead).ToUpper();
            Console.WriteLine($"Received = '{loginStatus}'");
            if(loginStatus.ToUpper().Equals("ACCEPTED"))
            {
                Console.WriteLine($"Welcome {login}!");
                userName = login;
                return true;
            }else
            {
                Console.WriteLine($"Failed to login with given credentials!");
                return false;
            }      
           // }
        }
    }
}