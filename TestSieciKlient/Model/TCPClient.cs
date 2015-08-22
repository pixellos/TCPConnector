using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Common_Files;
namespace TestSieciKlient.Model
{

    
    public class NetClient
    {
        public TcpClient client { get; private set; }
        string ip;
        int port;
        public Comunicator comunicator { get; private set; }

        public NetClient(string ip,int port)
        {
            ChangeParametrs(ip, port);
            client = new TcpClient();
        }

        public bool Connect()
        {
            client = new TcpClient();
            try
            {
                client.Connect(ip, port);
                comunicator = new Comunicator(client.GetStream());
            }
            catch (Exception ex)
            {
                return false;
               
                //ToDo Log += ex.ToString() + Environment.NewLine; 
            }
            return true;
        }

        public void ChangeParametrs(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
    }
}
