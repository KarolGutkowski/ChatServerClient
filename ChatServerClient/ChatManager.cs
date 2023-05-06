using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
namespace ChatServerClient
{
    public class ChatManager
    {
        
        private TcpClient tcpClient;
        public ChatManager(TcpClient client)
        {
            this.tcpClient = client;
        }

        public void ReadMessage()
        {
            var stream = tcpClient.GetStream();
            while (true)
            {
                if (stream.DataAvailable)
                {
                    var buffer = new byte[1_024];
                    stream.Read(buffer);
                    string received = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(received);
                }
            }
        }
       
    }
}
