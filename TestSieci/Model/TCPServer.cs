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

        public TCPServer(string iP, int port) : base(iP, port) { }

        public bool IsActive() { return (client != null); }

        public void StartServer()
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
            
            
            _listener = new TcpListener(IPAddress.Parse(_Ip), _Port);
            _listener.Start();
            client = _listener.AcceptTcpClient();
            _comunicator = new Comunicator(client.GetStream());
        }
    }
}
