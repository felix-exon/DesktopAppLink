using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace DesktopAppLinkSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Make sure to cleanup !!!
            foreach(string name in _names)
            {
                DesktopAppLink.RemoveLink(name);
            }
        }

        private List<string> _names = new List<string>();



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string exeToStart = Process.GetCurrentProcess().MainModule.FileName;
            string arguments = txtArguments.Text;
            string htmlArgs = txtHtmlArguments.Text;
            string protocolName = txtProtocolName.Text;

            if (string.IsNullOrEmpty(protocolName))
            {
                MessageBox.Show("Please name your protocol");
                return;
            }

            DesktopAppLink.CreateLink(protocolName, exeToStart, arguments);

            _names.Add(protocolName);
            
            string html = DesktopAppLink.GetSampleHyperlink(protocolName, "Click me!", htmlArgs);
            string tempPath = Path.GetTempFileName();
            File.WriteAllText(tempPath, html);
            string htmlFile = Path.Combine(Path.GetDirectoryName(tempPath), Path.GetFileNameWithoutExtension(tempPath) + ".html");
            File.Move(tempPath, htmlFile);
            Process.Start(htmlFile);
        }
    }
}
