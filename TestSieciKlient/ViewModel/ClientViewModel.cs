using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TestSieciKlient.Model;

namespace TestSieciKlient.ViewModel
{     
        
    ///<summary>
    ///Connection to ViewModel, this VievModel provides connection TCP with server. 
    ///</summary>
    class ClientViewModel
    {
        ///<param name="statusLabel">
        /// Label of client link with server status
        /// </param>
        /// <param name="receivedTextLabel"
        /// Label to receive text by TCPIP
        /// </param>
        const string _IP = "127.0.0.1";
        const int _PORT = 1024;
        NetClient netClient;
        private BackgroundWorker backgrounworker;
        
        private string label1text;
        private string label2text;
        Label statusLabel;
        Label recivedTextLabel;

        public ClientViewModel(Label statusLabel, Label recivedTextLabel)
        {
            netClient = new NetClient(_IP, _PORT);
            SetStatusLabel(statusLabel);
            SetRecivedLabel(recivedTextLabel);
        }

        ///<summary>
        ///Reseting of status Label
        /// </summary>
        public void SetStatusLabel(Label statusLabel)
        {   
            this.statusLabel = statusLabel;
        }

        ///<summary>
        /// Reseting recive text label
        /// </summary>
        public void SetRecivedLabel(Label recivedTextLabel)
        {
            this.recivedTextLabel = recivedTextLabel;
        }

        ///<summary>
        /// Making things easier ;)
        /// </summary>
        public void StartClientClick(object sender, RoutedEventArgs e)
        {
            netClient.Connect();
            BackgroundWorkerInitializator(backgrounworker, new DoWorkEventHandler(OnCallBack), new RunWorkerCompletedEventHandler(UpdateGUI));
        }

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
            string result;
            try
            {
                if (netClient.client.Connected)
                {
                    if ((netClient != null && (result = netClient.comunicator.BinaryReader.ReadString()) != ""))
                    {
                        
                        label2text = result;
                    }
                }
                else
                {
                    netClient.Connect();
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.ToString());
               
            }
            
            label1text = netClient.client.Connected.ToString();
        }
                  /// <summary>
                  /// CommandsToActon and update gui
                  /// </summary>
                  /// <param name="sender"></param>
                  /// <param name="e"></param>
        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Content = label1text;
            if (label2text != "")
            {
                if (label2text.ToCharArray()[0] == ':')
                {
                    switch (label2text)
                    {
                        default:
                            recivedTextLabel.Content = "$$$$$" + label2text + "$$$$$";
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
                    recivedTextLabel.Content = label2text;
                }
            }
            BackgroundWorker reference = sender as BackgroundWorker;
            reference.RunWorkerAsync();
        }
    }
}
