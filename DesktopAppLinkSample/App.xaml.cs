using System;
using System.Text;
using System.Windows;

namespace DesktopAppLinkSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            if(e.Args.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Arguments:");
                for(int i = 0; i < e.Args.Length; i++)
                {
                    builder.AppendLine($"{i+1}\t{e.Args[i]}");
                }
                MessageBox.Show(builder.ToString());
                Environment.Exit(0x0);
            }

            MainWindow = new MainWindow();
            MainWindow.ShowDialog();
        }
    }
    
}
