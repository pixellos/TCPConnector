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
    class TCPServer
    {
        public TcpListener listener { get; private set; }
        public TcpClient serverClient { get; private set; }
        public Comunicator comunicator;
        public string IP { get; private set; }
        public int Port { get; private set; }
        public TCPServer(string IP,int Port)
        {
            ChangeParametrs(IP, Port);
        }
        void ChangeParametrs(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
        }

        public void StartServer()
        {
            if (listener!=null)
            {
                listener.Stop();
            }
            listener = new TcpListener(IPAddress.Parse(IP), Port);
            listener.Start();
            serverClient = listener.AcceptTcpClient();
            comunicator = new Comunicator(serverClient.GetStream());
        }

        public bool IsActive()
        {
            return (serverClient != null && serverClient.Connected);
        }
    }
}
