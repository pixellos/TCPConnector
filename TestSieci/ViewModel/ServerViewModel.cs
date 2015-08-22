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
        const string _IP = "127.0.0.1";
        const int _PORT = 1024;
        TCPServer TCPServer;
        Label isConnectedLabel;
        string outputText;
        public ServerViewModel(Label isConnectedLabel)
        {
            TCPServer = new TCPServer(_IP, _PORT);
            SetConectionLabel(isConnectedLabel);
        }

        /// <summary>
        /// It sets connection with server status label
        /// </summary>
        /// <param name="isConnectedLabel">Server status label</param>
        public void SetConectionLabel(Label isConnectedLabel)
        {
            this.isConnectedLabel = isConnectedLabel;
        }
        /// <summary>
        /// It Sends text, you should use it into TextChanged event
        /// </summary>
        /// <param name="text">Text to send</param>
        public void SendText_Changed(string text)
        {
            if (text != null)
            {
                if (text == "")
                {
                    text = ":ES"; //:EmptyString
                }
                SendText(text);
            }
            
        }

       
        /// <param name="text">You can pass sender object to make things easier</param>
        private void SendText(string text)
        {
            if (TCPServer != null && TCPServer.IsActive())
            {
                TCPServer.comunicator.BinaryWriter.Write(text);
            }
        }

        /// <summary>
        /// It Sends text, you should use it into TextChanged event
        /// </summary>
        /// <param name="textSender">You can pass sender object to make things easier</param>
        public void SendText_Changed(object textSender)
        {
            string text = (textSender as TextBox).Text;
            SendText(text);
        }
        /// <summary>
        /// Use it to try connect to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetConnectionButtonClick(object sender, RoutedEventArgs e)
        {
            TCPServer.StartServer();
            isConnectedLabel.Content = TCPServer.serverClient.Connected;
        }
    }
}
