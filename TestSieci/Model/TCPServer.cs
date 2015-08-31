using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Common;

namespace TestSieci.Model
{
    sealed class TCPServer : Connector
    {
        public TcpListener _Listener { get; set; }

        bool _AsyncPending;

        public TCPServer(string iP, int port) : base(iP, port) { }

        public bool IsActive() { return (Client != null); }

        public void StartServer()
        {
            if (_Listener == null)
            {
                _Listener = new TcpListener(IPAddress.Parse(_Ip), _Port);
                _Listener.Start();
            }
            _comunicator = new Comunicator(null);
        }

        /// <summary>
        /// Use it, when u want to recive connection asynchronously at notUI thread, next use  UIThreadSetClientIfPending at UISynchronous Thread
        /// </summary>
        public void AsyncWaitForClient()
        {
            if (_Listener != null && _Listener.Pending())
            {
                _AsyncPending = true;
            }
        }

        /// <summary>
        /// Use it after AsyncWaitForClient
        /// </summary>
        public void UIThreadSetClientIfPending()
        {
            bool _Pending = _AsyncPending;
            if (_Pending)
            {
                Client = _Listener.AcceptTcpClient();// Remember only last connection TODO: Make multiconnection posibility.
                _comunicator = new Comunicator(Client.GetStream());
                _AsyncPending = false;
            }
        }
    
        /// <summary>
        /// Use it, when u want to freeze main Thread until connected
        /// </summary>
        public void WaitForClient()
        {
            if (_Listener != null && _Listener.Pending())
            {
                Client = _Listener.AcceptTcpClient();// Remember only last connection TODO: Make multiconnection posibility.
                _comunicator = new Comunicator(Client.GetStream());
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            if (_Listener !=null)
            {
                _Listener.Stop();
            }
        }
    }
}
