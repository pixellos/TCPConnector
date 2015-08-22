using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Windows;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common_Files
{
    internal class Comunicator : IDisposable
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

        public bool IsStreamDescribed()
        {
            if (newStream == null)
            {
                return false;
            }
            else return true;
        }

        public string ReadText()
        {   
            return BinaryReader.ReadString();
        }

        public void SendText(string text)
        {  
            BinaryWriter.Write(text);
        }

        public void Dispose()
        {
            newStream.Close();
            BinaryReader.Close();
            BinaryWriter.Close();
        }
    }

    public abstract class abstract_Connector
    {
        public TcpClient client { get; protected set; }
        protected string ip;
        protected int port;
        Comunicator comunicator { get; set; }

        public abstract_Connector(string ip, int port)
        {
            ChangeParametrs(ip, port);
            client = new TcpClient();
        }
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="ip">IP adress, format xxx.xxx.xxx.xxx</param>
        /// <param name="port"></param>
        public void ChangeParametrs(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void SendText(string text)
        {
            if (IsConnected().Equals(true))
            {
                try
                {
                    comunicator.SendText(text);
                }
                catch (IOException)
                {
                    MessageBox.Show("Połączenie zostało przerwane, kliknij połącz aby połączyć", "Oops");
                }
                  
            }
        }

        public string ReadText()
        {
            try
            {
                if (IsEverythingExist() && IsConnected().Value.Equals(true))
                {
                    return comunicator.ReadText();
                }
                else return null;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Straciłeś połączenie z powodu " + ex.Message+ " Próba odnowienia połączenia", "Opps");
                Connect();
                return null;
            }
        }
        public bool IsEverythingExist()
        {
            if (client != null && comunicator != null)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Is client connected bool?
        /// </summary>
        /// <returns>If there's no client returns null</returns>
        public bool? IsConnected()
        {
            if (client == null || comunicator == null)
            {
                return null;
            }
            else
            {
                comunicator.SendText(":AYT?");///Send command :AreYouThere, beacuse connected have Conection status to last IO Operation https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient.connected.aspx
                return client.Connected;
            }
        }

        public bool StartServer(TcpListener tcpListener)
        {
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
            tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            tcpListener.Start();
            client = tcpListener.AcceptTcpClient();
            if (comunicator == null)
            {
                comunicator = new Comunicator(client.GetStream());
            }
            else comunicator.ChangeParametrs(client.GetStream());
            if (tcpListener.Pending())     //If someone connected with server
            {
                return true;
            }
            else return false;
        }

        public bool Connect()
        {
            try
            {
                client = new TcpClient(ip, port);
                comunicator = new Comunicator(client.GetStream());
            }
            catch (Exception ex)
            {
                MessageBox.Show("U cannot connect beacuse " + ex.Message, "Opps, i cannot " + "Connect :/");
                return false;
            }
            return true;
        }
    }
}
                                         