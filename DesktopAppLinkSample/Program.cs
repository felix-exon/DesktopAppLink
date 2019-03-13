using System;

namespace DesktopAppLinkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App started...");
            Console.WriteLine();

            if (args.Length > 0)
            {
                Console.WriteLine("Seems like the app has been started via Web-Link");
                Console.WriteLine($"Arguments: {string.Join(", ", args)}");
                Console.WriteLine();
                Console.WriteLine("Congratulations!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Press p to create the protocol:");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.P)
                {
                    Console.WriteLine();
                    DesktopAppLink.CreateLink("applink.sample", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, "\"Just some random argument\"");
                    Console.WriteLine("Link added..");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press any key to exit:");
            Console.ReadKey();
        }
    }

}
