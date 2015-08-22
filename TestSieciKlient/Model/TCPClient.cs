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
    
    sealed class NetClient :abstract_Connector
    {
        public NetClient(string ip, int port)  :base(ip,port)
        {
        }
    }
}
