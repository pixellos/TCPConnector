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
    class TCPServer : abstract_Connector
    {
        public TcpListener listener { get; private set; }

        public TCPServer(string iP, int port):base(iP,port)
        {
        }

        public void StartServer()
        {
            if (listener!=null)
            {
                listener.Stop();
            }
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
            client = listener.AcceptTcpClient();
            comunicator = new Comunicator(client.GetStream());
        }

        public bool IsActive()
        {
            return (client != null && client.Connected);
        }
    }
}
