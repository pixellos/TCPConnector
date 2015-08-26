using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TestSieciKlient.Model;
using Common;

namespace TestSieciKlient.ViewModel
{     
    class ClientViewModel
    {
        string _IP = "127.0.0.1";
        int _PORT = 1024;
        NetClient netClient;
        Commands CommandControler;
        private BackgroundConnectionHelper backgroundHelper;
        private string statusString, reciveString;
        Label statusLabel, recivedTextLabel;

        public ClientViewModel(Label statusLabel, Label recivedTextLabel)
        {
            netClient = new NetClient(_IP, _PORT);
            SetStatusLabel(statusLabel);
            SetRecivedLabel(recivedTextLabel);
            CommandControler = new Commands();
            backgroundHelper = new BackgroundConnectionHelper(new DoWorkEventHandler(OnCallBack), new RunWorkerCompletedEventHandler(UpdateGUI));
        }

        public bool? IsConnected()  { return netClient.IsConnected(); }

        public void SetStatusLabel(Label statusLabel) { this.statusLabel = statusLabel; }   
            
        public void SetRecivedLabel(Label recivedTextLabel) { this.recivedTextLabel = recivedTextLabel; }
        
        public void StartClientClick(object sender, RoutedEventArgs e)
        {
            if (netClient.Connect())
                backgroundHelper.Start();
        }

        private void OnCallBack(object sender, DoWorkEventArgs e)
        {
            reciveString = netClient.ReadText();
            statusString = netClient.client.Connected.ToString();
        }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Content = statusString;
            if (reciveString != null)
            {
               recivedTextLabel.Content = CommandControler.Decode(reciveString);
            }
            var Reference = sender as BackgroundWorker;
            if (IsConnected().Value.Equals(true))
            {
                Reference.RunWorkerAsync();
            }
        }
    }
}