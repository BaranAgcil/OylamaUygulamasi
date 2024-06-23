using System;

namespace odev
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.SetupServer(5, 8585);
            Server.Start();
            Console.ReadKey();
        }
    }
}
