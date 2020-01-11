using System.Net;
using System.Net.Sockets;

namespace UdpClientServer
{
    public class ExampleUdpClient : ExampleUdpConnection
    {
        public void Connect(IPAddress remoteAddress, int port)
        {
            _sendEndpoint = new IPEndPoint(remoteAddress, port);
            _receiveEndpoint = new IPEndPoint(IPAddress.Any, 0);
            _client = new UdpClient();
            _client.Connect(_sendEndpoint);
        }
    }
}
