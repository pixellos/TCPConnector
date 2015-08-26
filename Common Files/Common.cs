using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Common
{
    public class Comunicator : IDisposable
    {
        Stream newStream;
        protected BinaryReader BinaryReader { get; private set; }
        protected BinaryWriter BinaryWriter { get; private set; }

        public Comunicator(Stream IOStream) { ChangeParametrs(IOStream); }

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
            try
            {

                return BinaryReader.ReadString();
            }
            catch (IOException exception)
            {
                MessageBox.Show("Cannot write into stream: (Extendend info) \n" + exception.Message);
                return "!! NO CONNECTION !!";        
            }

        }

        public void SendText(string text) {
            try
            {
                BinaryWriter.Write(text);
            }
            catch (IOException exception)
            {
                MessageBox.Show("No connection, cannot write into stream: (Extended)\n" + exception.Message);
            }
            
        }


        public void Dispose()
        {
            newStream.Close();
            BinaryReader.Close();
            BinaryWriter.Close();
        }

    }

    public abstract partial class Connector
    {
        public TcpClient client { get; protected set; }
        protected string ip;
        protected int port;
        protected Comunicator comunicator { get; set; }

        public Connector(string ip, int port)
        {
            ChangeParametrs(ip, port);
            client = new TcpClient();
        }
        
        /// <param name="ip">IP adress, format xxx.xxx.xxx.xxx</param>
        /// <param name="port"></param>
        public void ChangeParametrs(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public string ReadText()
        {
            return comunicator.ReadText();
        }

        public void SendText(string text)
        {
            if (IsConnected())
            {
                try
                {
                    if (text == null)
                    {
                        text = ":ES";
                    }
                    comunicator.SendText(text);
                }
                catch (IOException)
                {
                    MessageBox.Show("Połączenie zostało przerwane, kliknij połącz aby połączyć", "Oops");
                }
            }
        }

        public bool IsConnected()
        {
            if (client == null || comunicator == null)
            {
                return false;
            }
            else
            {
                comunicator.SendText(":AYT?");///Send command :AreYouThere, beacuse connected have Conection status to last IO Operation https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient.connected.aspx
                return client.Connected;
            }
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
                                         