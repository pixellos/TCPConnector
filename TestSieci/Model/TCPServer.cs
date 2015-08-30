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
        TcpListener _listener { get; set; }

        bool _AsyncPending;

        public TCPServer(string iP, int port) : base(iP, port) { }

        public bool IsActive() { return (client != null); }

        public void StartServer()
        {
            if (_listener == null)
            {
                _listener = new TcpListener(IPAddress.Parse(_Ip), _Port);
                _listener.Start();
            }
            _comunicator = new Comunicator(null);
        }

        /// <summary>
        /// Use it, when u want to recive connection asynchronously at notUI thread, next use  UIThreadSetClientIfPending at UISynchronous Thread
        /// </summary>
        public void AsyncWaitForClient()
        {
            if (_listener != null && _listener.Pending())
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
                client = _listener.AcceptTcpClient();// Remember only last connection TODO: Make multiconnection posibility.
                _comunicator = new Comunicator(client.GetStream());
                _AsyncPending = false;
            }
        }
    
        /// <summary>
        /// Use it, when u want to freeze main Thread until connected
        /// </summary>
        public void WaitForClient()
        {
            if (_listener != null && _listener.Pending())
            {
                client = _listener.AcceptTcpClient();// Remember only last connection TODO: Make multiconnection posibility.
                _comunicator = new Comunicator(client.GetStream());
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            if (_listener !=null)
            {
                _listener.Stop();
            }
        }
    }
}
