using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TestSieciKlient.Model;
using System.Windows.Input;

using Common;

namespace TestSieciKlient.ViewModel 
{     
    class ClientViewModel  : INotifyPropertyChanged
    {
        string _IP = "127.0.0.1";
        int _PORT = 1024;
        NetClient netClient;
        Commands CommandControler;
        private BackgroundConnectionHelper backgroundHelper;

        private string _asyncRecivedText;
        private bool _asyncConnection;

        public ICommand ConnectClick
        {
            get
            {
                return _ConnectClick;
            }
            set
            {
                RaisePropertyChanged("ConnectClick");
            }
        }
        private CommandAction _ConnectClick;
    

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ClientViewModel()
        {
            netClient = new NetClient(_IP, _PORT);
            CommandControler = new Commands();
            backgroundHelper = new BackgroundConnectionHelper(new DoWorkEventHandler(OnCallBack), new RunWorkerCompletedEventHandler(UpdateGUI));
            
            _ConnectClick = new CommandAction(StartClientClick);
        }

        public void StartClientClick()
        {
            if (netClient.Connect())
                backgroundHelper.Start();
            RaisePropertyChanged("Connection");
        }

        private void OnCallBack(object sender, DoWorkEventArgs e)
        {
            _asyncRecivedText = netClient.ReadText();
            _asyncConnection = netClient.IsConnected();
        }

        private void UpdateGUI(object sender, RunWorkerCompletedEventArgs e)
        {
            _Connection = _asyncConnection;            
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

        private string _RecivedText;
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

        private bool _Connection;
        public  bool Connection
        {
            get {  return _Connection; }
            set
            {
                _Connection = value;
                
                RaisePropertyChanged("Connection");
            }
        }

    }
}