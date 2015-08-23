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


        public bool IsActive() { return (client != null); }

        public bool StartServer()
        {
            if (listener != null)
            {
                listener.Stop();
            }
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
            client = listener.AcceptTcpClient();
            if (comunicator == null)
            {
                comunicator = new Comunicator(client.GetStream());
            }
            else comunicator.ChangeParametrs(client.GetStream());
            if (listener.Pending())     //If someone connected with server
            {
                return true;
            }
            else return false;
        }

    }
}
