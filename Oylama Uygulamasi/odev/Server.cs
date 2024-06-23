using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace odev
{
    internal class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static TcpListener Listener;

        
        public static Dictionary<string, int> Votes = new Dictionary<string, int>()
        {
            { "elma", 0 },
            { "armut", 0 },
            { "karpuz", 0 }
        };

        public static void SetupServer(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;
            Listener = new TcpListener(IPAddress.Any, Port);
            Console.WriteLine("Server Aktif");
            Console.WriteLine($"Maksimum bağlantı: {MaxPlayers}");
            Console.WriteLine($"Dinlenen port: {Port}");
        }

        public static void Start()
        {
            Listener.Start();
            Console.WriteLine("Başladı");
            Listener.BeginAcceptTcpClient(ACCB, null);
            Console.WriteLine("Oylayan kişi bekleniyor");
        }

        public static void ACCB(IAsyncResult asyncResult)
        {
            TcpClient client = Listener.EndAcceptTcpClient(asyncResult);
            Console.WriteLine($"Biri oyunu kullandı {client.Client.RemoteEndPoint}");
            Listener.BeginAcceptTcpClient(ACCB, null); // Yeni bağlantıları kabul etmeye devam et

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string vote = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim().ToLower();

            if (Votes.ContainsKey(vote))
            {
                Votes[vote]++;
                Console.WriteLine($"Oy alındı: {vote}. Toplam oy: {Votes[vote]}");
            }
            else
            {
                Console.WriteLine($"Geçersiz oy: {vote}");
            }

            
            byte[] response = Encoding.UTF8.GetBytes("Teşekkürler! Oyunuz alındı.");
            stream.Write(response, 0, response.Length);

            client.Close();
        }
    }
}
