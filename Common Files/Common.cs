using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common_Files
{
    public class Comunicator
    {
        Stream newStream;
        public BinaryReader BinaryReader { get; private set; }
        public BinaryWriter BinaryWriter { get; private set; }
        public Comunicator(Stream IOStream)
        {
            ChangeParametrs(IOStream);
        }

        public void ChangeParametrs(Stream IOStream)
        {
            newStream = IOStream;
            BinaryWriter = new BinaryWriter(newStream);
            BinaryReader = new BinaryReader(newStream);
        }
    }

    public abstract class abstract_Connector
    {
        public TcpClient client { get; protected set; }
        protected string ip;
        protected int port;
        public Comunicator comunicator { get; protected set; }

        public abstract_Connector(string ip, int port)
        {
            ChangeParametrs(ip, port);
            client = new TcpClient();
        }
        
        public void ChangeParametrs(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
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


    }
}
