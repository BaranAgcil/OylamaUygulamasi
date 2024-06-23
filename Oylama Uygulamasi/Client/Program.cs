using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerSettings.ServerAyarla("127.0.0.1", 8585); // IP adresini düzelttik

            Console.WriteLine("Oy verilecek seçenekler: elma, armut, karpuz");
            Console.Write("Lütfen oyunuzu girin: ");
            string vote = Console.ReadLine();

            Conn.Connect(vote);
            Console.ReadKey();
        }
    }
}
