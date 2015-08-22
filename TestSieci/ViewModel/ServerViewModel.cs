using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Net.Sockets;
using System.Net;
using System.IO;
using TestSieci.Model;

namespace TestSieci.ViewModel
{
    /// <summary>
    /// Basic viewmodel for main server window
    /// </summary>
    
    class ServerViewModel
    {
        string _IP = "127.0.0.1";
        int _PORT = 1024;
        TCPServer TCPServer;
        Label isConnectedLabel;

        public ServerViewModel(Label isConnectedLabel)
        {
            TCPServer = new TCPServer(_IP, _PORT);
            SetConectionLabel(isConnectedLabel);
        }

        public void SetConectionLabel(Label isConnectedLabel)
        {
            this.isConnectedLabel = isConnectedLabel;
        }

        public void GetConnection()
        {
            TCPServer.StartServer();
            isConnectedLabel.Content = TCPServer.client.Connected;
        }

        internal void SendText_Changed(object sender)
        {
            TCPServer.SendText_Changed(sender);
        }
    }
}
