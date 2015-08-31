using System.Windows.Input;
using System.ComponentModel;
using TestSieci.Model;
using Common;

namespace TestSieci.ViewModel
{
    sealed class ServerViewModel : INotifyPropertyChanged
    {
        #region properties
        TCPServer _TCPServer;
        Commands _Commands;
        
        public event PropertyChangedEventHandler PropertyChanged;

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

        string _Ip = "127.0.0.1";
        public string IP
        {
            get { return _Ip; }
            set
            {
                _Ip = value;
                RaisePropertyChanged("IP");
            }
        }

        public bool IsConnectedProperty
        {
            get { return _TCPServer.IsConnected(); }
            set
            {
                RaisePropertyChanged("IsConnectedProperty");
            }
        }

        string _textToSend = "Write There:";
        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                _TCPServer.SendText(_textToSend);
                RaisePropertyChanged("TextToSend");
            }
        }

        string _asyncRecivedText;
        string _RecivedText = "Recived Message";
        public string RecivedText
        {
            get { return _RecivedText; }
            set
            {
                _RecivedText = value;
                RaisePropertyChanged("RecivedText");
            }
        }

        CommandAction _TryToStartServer;
        public ICommand  TryToStartServer
        {
            get
            {
                RaisePropertyChanged("IsConnectedProperty");
                return _TryToStartServer;
            }
        }
        #endregion

        BackgroundConnectionHelper _BackgroundHelper;

        public ServerViewModel()
        {
            _TCPServer = new TCPServer(_Ip, _Port);
            _TryToStartServer = new CommandAction(GetConnection);
            _BackgroundHelper = new BackgroundConnectionHelper(
                new DoWorkEventHandler(AsyncOperations),
                new RunWorkerCompletedEventHandler(UpdateGUI));
            _Commands = new Commands();

        }

        #region BackgroundHelper  methods
        private void AsyncOperations(object sender, DoWorkEventArgs e)
        {
            if (_TCPServer.IsConnected())
            {
                RaisePropertyChanged("IsConnectedProperty");
                _asyncRecivedText = _TCPServer.ReadText();
            }
            _TCPServer.AsyncWaitForClient();
        }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            _TCPServer.UIThreadSetClientIfPending();
            if (_asyncRecivedText != null && _asyncRecivedText !=Commands.IsTherePartnerCommand )
            {
                RecivedText = _Commands.Decode(_asyncRecivedText);
                RaisePropertyChanged("RecivedText");
            }
            var Reference = sender as BackgroundWorker;
            Reference.RunWorkerAsync();
        }
        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public void GetConnection()
        {
            if (_TCPServer != null)
            {
                _TCPServer.Dispose();
                _TCPServer = new TCPServer(_Ip, _Port);
                _TCPServer.StartServer();
                _BackgroundHelper.Start();
            }
            RaisePropertyChanged("IsConnectedProperty");
        }
    }
}