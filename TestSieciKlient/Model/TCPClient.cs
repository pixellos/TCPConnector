using Common;
namespace TestSieciKlient.Model
{

    sealed class NetClient : Connector
    {
        public NetClient(string ip, int port) : base(ip, port) { }
    }
}