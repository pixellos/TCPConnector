using System.Windows.Input;
using System.ComponentModel;
using TestSieci.Model;

namespace TestSieci.ViewModel
{
    public class ServerViewModel : INotifyPropertyChanged
    {
        string _Ip = "10.10.12.227";
        int _Port = 1024;
        TCPServer _TCPServer;
        string _textToSend;
        string _textStatus;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        Common.CommandAction _TryToStartServer;
        public ICommand  TryToStartServer
        {
            get
            {
                RaisePropertyChanged("IsConnectedProperty");
                return _TryToStartServer;
            }
        }

        public ServerViewModel()
        {
            _TCPServer = new TCPServer(_Ip, _Port);
            _TryToStartServer = new Common.CommandAction(StartServer);
        }

        public void StartServer  ()
        {
            _TCPServer.StartServer();
        }

        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                  _textToSend = value;
                RaisePropertyChanged("IsConnectedProperty");
                _TCPServer.SendText(_textToSend);
                RaisePropertyChanged("TextToSend");
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

        public int Port
        {
            get { return _Port; }
            set { _Port = value;}
        }

        public string IP
        {
            get { return _Ip; }
            set
            {
                _Ip = value;
                RaisePropertyChanged("IP");
            }
        }

        public void GetConnection()
        {
            _TCPServer.StartServer();
            _textStatus = _TCPServer.IsConnected().ToString();
        }
    }
}