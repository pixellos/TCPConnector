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
    
    class ServerViewModel
    {
        string ip = "127.0.0.1";
        int port = 1024;
        TCPServer TCPServer;
        Label isConnectedLabel;

        public ServerViewModel(Label isConnectedLabel)
        {
            TCPServer = new TCPServer(ip, port);
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
