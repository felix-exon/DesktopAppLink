using System;
using Microsoft.Win32;

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

    public class DesktopAppLink
    {
        /// <summary>
        /// creates Links that call any application on your system
        /// </summary>
        /// <param name="protocolName">
        /// the name of the custom protocol you are about to install
        /// Create your hyperlinks like this: href="protocolName:"
        /// </param>
        /// <param name="applicationPath">exact path to the executable</param>
        /// <param name="arguments">any arguments you pass here will be forwarded to the executable</param>
        /// <param name="iconPath">some browsers will show the icon when invoking the application - optional</param>
        public static void CreateLink(
            string protocolName,
            string applicationPath,
            string arguments,
            string iconPath = null)
        {
            try
            {
                //Delete existing with same name
                RemoveLink(protocolName.ToLower());

                RegistryKey root = Registry.ClassesRoot.CreateSubKey(protocolName.ToLower());

                if(root == null)
                    throw new ArgumentException("error on creating root key");

                root.SetValue("URL Protocol", "");
                root.SetValue(null, $"URL:{protocolName.ToLower()}");

                if (!string.IsNullOrEmpty(iconPath))
                {
                    var iconKey = root.CreateSubKey("DefaultIcon");
                    if(iconKey == null)
                        throw new Exception("error on creating icon key");
                    iconKey.SetValue(null, $"\"{iconPath}\"");
                }

                var commandKey = root.CreateSubKey("shell")?.CreateSubKey("open")?.CreateSubKey("command");
                if(commandKey == null)
                    throw new Exception("error on creating command key");

                commandKey.SetValue(null, $"\"{applicationPath}\" {arguments} --\"%1\"");

                root.Close();
            }
            catch (Exception)
            {
                //Let's not leave broken entries behind !
                DesktopAppLink.RemoveLink(protocolName);
                throw;
            }
        }

        /// <summary>
        /// deletes the protocol with the specified name - if found
        /// </summary>
        /// <param name="protocolName">name of the protocol to delete</param>
        public static void RemoveLink(string protocolName)
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(protocolName.ToLower());
            }
            catch (ArgumentException)
            {
                //Most likely 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetSampleHyperlink(string protocolName)
        {
            return $"<a href={protocolName.ToLower()}:>Sample Text</a>";
        }
    }

}
