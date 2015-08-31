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
        NetClient _netClient;
        Commands _CommandControler;
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
            get { return _netClient.IsConnected(); }
            set
            {
                _Connection = value;
                RaisePropertyChanged("Connection");
            }
        }

        string _TextToSend = "Write There:";
        public string TextToSend
        {
            get { return _TextToSend; }
            set
            {
                _TextToSend = value;
                _netClient.SendText(_TextToSend);
                RaisePropertyChanged("TextToSend");
            }
        }

        private string _Logs;

        public string Logs
        {
            get { return _Logs; }
            set
            {
                _Logs = value;
                RaisePropertyChanged("Logs");
            }
        }

        string _IP = "127.0.0.1";
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
            _netClient = new NetClient(_IP, _Port);
            _CommandControler = new Commands();
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
            _netClient.Dispose();
            _netClient = new NetClient(_IP, _Port);
            _netClient.Connect();
            _backgroundHelper.Start();
            RaisePropertyChanged("Connection");
        }

        private void OnCallBack(object sender, DoWorkEventArgs e)
        {
            _asyncRecivedText = _netClient.ReadText();
            _asyncConnection = _netClient.IsConnected();
        }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            Connection = _asyncConnection;            
            if (_asyncRecivedText != null && _asyncRecivedText != Commands.IsTherePartnerCommand)
            {
                RecivedText = _CommandControler.Decode(_asyncRecivedText);
            }
            var Reference = sender as BackgroundWorker;
            if (_netClient.IsConnected().Equals(true))
            {
                Reference.RunWorkerAsync();
            }
        }
    }
}