using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using System.ComponentModel;
using TestSieci.Model;
using Common_Files;

namespace TestSieci.ViewModel
{
    class ServerViewModel
    {
        string ip = "127.0.0.1";
        int port = 1024;
        TCPServer TCPServer;
        Label isConnectedLabel,RecivedTextLabel;
        string RecivedText;
        
        BackgroundConnectionHelper backgroundHelper;        

        public ServerViewModel(Label isConnectedLabel, Label recivedTextLabel)
        {
            TCPServer = new TCPServer(ip, port);
            SetConectionLabel(isConnectedLabel);
            SetRecivedTextLabel(recivedTextLabel);
            backgroundHelper = new BackgroundConnectionHelper(new DoWorkEventHandler(DoAsyncTask), new RunWorkerCompletedEventHandler(UpdateGUI));
        }

        public void GetConnection()
        {
            TCPServer.StartServer();
            isConnectedLabel.Content = TCPServer.client.Connected;
            backgroundHelper.Start();
        }

        public void SetConectionLabel(Label isConnectedLabel) { this.isConnectedLabel = isConnectedLabel; }

        public void SetRecivedTextLabel(Label recivedTextLabel) { this.RecivedTextLabel = recivedTextLabel; } 

        internal void SendText_Changed(object sender) { TCPServer.SendText_Changed(sender); }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            RecivedText = TCPServer.ReadText();
        }

        private void DoAsyncTask(object sender, DoWorkEventArgs e)
        {
            if (RecivedText !=null)
            {
                RecivedTextLabel.Content = RecivedText;
            }
            var Reference = sender as BackgroundWorker;
            
        }
    }
}