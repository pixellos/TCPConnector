using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Common_Files;

namespace TestSieci.Model
{
    sealed class TCPServer : abstract_Connector
    {
        TcpListener listener { get; set; }

        public TCPServer(string iP, int port):base(iP,port) {}

        public bool StartServer() { return StartServer(listener); }

        public bool IsActive() { return (client != null); }
    }
}
