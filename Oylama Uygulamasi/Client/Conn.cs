using System;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Conn
    {
        public static TcpClient Client = new TcpClient();

        public static void Connect(string vote)
        {
            Client.BeginConnect(ServerSettings.HOST, ServerSettings.PORT, ar =>
            {
                Client.EndConnect(ar);
                Console.WriteLine("Bağlanıldı");

                NetworkStream stream = Client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(vote);
                stream.Write(buffer, 0, buffer.Length);

                buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Sunucudan yanıt: {response}");

                Client.Close();
            }, null);

            Console.WriteLine("Beklemede");
        }
    }
}
