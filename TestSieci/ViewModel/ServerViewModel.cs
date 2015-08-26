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
using System.ComponentModel;
using TestSieci.Model;



namespace TestSieci.ViewModel
{
   
 
    public class ServerViewModel : INotifyPropertyChanged
    {
        string ip = "127.0.0.1";
        int port = 1024;
        TCPServer TCPServer;
        private string _textToSend;
        private string _textStatus;
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand  TryToStartServer
        {
            internal set { }
            get
            {
                
                return _TryToStartServer;
            }
        }

        Common.CommandAction _TryToStartServer;


        public ServerViewModel()
        {
            TCPServer = new TCPServer(ip, port);

            _TryToStartServer = new Common.CommandAction(StartServer);
            
        }

        public void StartServer  ()
        {
            TCPServer.StartServer();
        }
        

        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                
                TCPServer.SendText(_textToSend);
                RaisePropertyChanged("TextToSend");
            }
        }

        public bool IsConnectedProperty
        {
            get { return TCPServer.IsConnected(); }
            set
            {
                StartServer();
                RaisePropertyChanged("IsConnectedProperty");
            }
        }

        

        public void GetConnection()
        {
            TCPServer.StartServer();
            _textStatus = TCPServer.IsConnected().ToString();
        }
                                                                                                               
    }
}