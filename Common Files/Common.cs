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
    public abstract partial class Connector  : IDisposable
    {
        public TcpClient Client { get; protected set; }
        protected string _Ip;
        protected int _Port;
        protected Comunicator _comunicator { get; set; }
       
        public Connector(string ip, int port)
        {
            this._Ip = ip;
            this._Port = port;
            Client = new TcpClient();
        }

        public string ReadText()
        {
            try
            {
            	return _comunicator.ReadText();
            }
            catch (System.Exception exception)
            {
                MessageBox.Show("Dont have connection" + exception.Message);
                return "##NOT CONNECTED##";
            }
        }

        public void SendText(string text)
        {
            if (IsConnected())
            {
                try
                {
                    if (text == null)
                        text = ":ES";

                    _comunicator.SendText(text);
                }
                catch (IOException ioException)
                {
                    MessageBox.Show("Connection was incidentaly closed" + ioException.Message, "Oops");
                }
            }
        }

        public bool IsConnected()
        {
            if (Client == null || _comunicator == null )
                return false;
            else
            {
                //_comunicator.SendText(":AYT?");///Send command :AreYouThere, beacuse connected have Conection status equals connection stauts  during last IO Operation https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient.connected.aspx
                return Client.Connected;
            }
        }

        public bool Connect()
        {
            try
            {
                Client = new TcpClient(_Ip, _Port);
                _comunicator = new Comunicator(Client.GetStream());
            }
            catch (Exception ex)
            {
                MessageBox.Show("U cannot connect beacuse " + ex.Message, "Opps, i cannot " + "Connect :/");
                return false;
            }
            return true;
        }

        public virtual void Dispose()
        {
            if (_comunicator != null)
                _comunicator.Dispose();
            Client.Close();
        }
    }
}
                                         