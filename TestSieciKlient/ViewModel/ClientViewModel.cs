using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TestSieciKlient.Model;
using System.Windows.Input;
using System.IO;

using Common;

namespace TestSieciKlient.ViewModel 
{     
    class ClientViewModel  : INotifyPropertyChanged
    {
        #region Properties
        NetClient netClient;
        Commands CommandControler;
        BackgroundConnectionHelper _backgroundHelper;
        public event PropertyChangedEventHandler PropertyChanged;

        string _asyncRecivedText;
        string _RecivedText;
        public string RecivedText
        {
            get
            {
                return _RecivedText;
            }
            set
            {

                _RecivedText = value;
                RaisePropertyChanged("RecivedText");
            }
        }

        bool _asyncConnection;
        bool _Connection;
        public bool Connection
        {
            get { return netClient.IsConnected(); }
            set
            {
                _Connection = value;
                RaisePropertyChanged("Connection");
            }
        }

        string _textToSend = "Write There:";
        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                netClient.SendText(_textToSend);
                RaisePropertyChanged("TextToSend");
            }
        }

        string _IP = "10.10.12.227";
        public string IP
        {
            get { return _IP; }
            set
            {
                _IP = value;
                RaisePropertyChanged("IP");
            }
        }

        int _Port = 1024;
        public int Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                RaisePropertyChanged("Port");
            }
        }

        CommandAction _ConnectClick;
        public ICommand ConnectClick
        {
            get { return _ConnectClick; }
            set { RaisePropertyChanged("ConnectClick"); }
        }
        #endregion

        public ClientViewModel()
        {
            netClient = new NetClient(_IP, _Port);
            CommandControler = new Commands();
            _backgroundHelper = new BackgroundConnectionHelper(new DoWorkEventHandler(OnCallBack), new RunWorkerCompletedEventHandler(UpdateGUI));
            _ConnectClick = new CommandAction(StartClientClick);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void StartClientClick()
        {
            netClient.Dispose();
            netClient = new NetClient(_IP, _Port);
            netClient.Connect();
            _backgroundHelper.Start();
            RaisePropertyChanged("Connection");
        }

        private void OnCallBack(object sender, DoWorkEventArgs e)
        {
            _asyncRecivedText = netClient.ReadText();
            _asyncConnection = netClient.IsConnected();
        }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            Connection = _asyncConnection;            
            if (_asyncRecivedText != null)
            {
                RecivedText = CommandControler.Decode(_asyncRecivedText);
            }
            var Reference = sender as BackgroundWorker;
            if (netClient.IsConnected().Equals(true))
            {
                Reference.RunWorkerAsync();
            }
        }
    }
}