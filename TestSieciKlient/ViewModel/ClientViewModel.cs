using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TestSieciKlient.Model;

namespace TestSieciKlient.ViewModel
{     
    class ClientViewModel
    {
        string _IP = "127.0.0.1";
        int _PORT = 1024;
        NetClient netClient;
        private BackgroundWorker backgrounworker;      //Todo Backgroundworker to external class
        private string statusString;
        private string reciveString;
        Label statusLabel;
        Label recivedTextLabel;

        public ClientViewModel(Label statusLabel, Label recivedTextLabel)
        {
            netClient = new NetClient(_IP, _PORT);
            SetStatusLabel(statusLabel);
            SetRecivedLabel(recivedTextLabel);
        }

        public bool? IsConnected()
        {
            return netClient.IsConnected();
        }

        public void SetStatusLabel(Label statusLabel)
        {   
            this.statusLabel = statusLabel;
        }
        
        public void SetRecivedLabel(Label recivedTextLabel)
        {
            this.recivedTextLabel = recivedTextLabel;
        }
        
        public void StartClientClick(object sender, RoutedEventArgs e)
        {
            if (netClient.Connect())
            {
                BackgroundWorkerInitializator(backgrounworker, new DoWorkEventHandler(OnCallBack), new RunWorkerCompletedEventHandler(UpdateGUI));
            }
        }

        /// <summary>
        /// If u want get Backgroundworker at loop, at updateGui cal (sender as BackgroundWorker).RunWorkerAsync();
        /// </summary>
        /// <param name="backgroundWorker"></param>
        /// <param name="ToDo"></param>
        /// <param name="UpdateGui"></param>
        private void BackgroundWorkerInitializator(BackgroundWorker backgroundWorker, DoWorkEventHandler ToDo, RunWorkerCompletedEventHandler UpdateGui)
        {
            if (backgroundWorker == null)
            {
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += ToDo;
                backgroundWorker.RunWorkerCompleted += UpdateGui;
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void OnCallBack(object sender, DoWorkEventArgs e)
        {
            reciveString = netClient.ReadText();
            statusString = netClient.client.Connected.ToString();
        }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Content = statusString;
            
            /////////////////////////////////////////////////////////////////
            //////////////////TODO !!! CODE/ENCODE COMMON LIB!///////////////
            if (reciveString != null)
            {
                if (reciveString != "")
                {
                    if (reciveString.ToCharArray()[0] == ':')
                    {
                        switch (reciveString)
                        {
                            default:
                                recivedTextLabel.Content = "$$$$$" + reciveString + "$$$$$";
                                break;
                            case ":AYT?":
                                MessageBox.Show("Jest");
                                break;
                            case ":ES":
                                recivedTextLabel.Content = "";
                                break;
                            case ":FF":
                                recivedTextLabel.Content = "#####KomendaFF##########";
                                break;
                        }
                    }
                    else
                    {
                        recivedTextLabel.Content = reciveString;
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////
            var Reference = sender as BackgroundWorker;
            if (IsConnected().Value.Equals(true))
            {
                Reference.RunWorkerAsync();
            }
        }
    }
}
