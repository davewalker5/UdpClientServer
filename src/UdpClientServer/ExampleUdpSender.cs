using System.Net;
using System.Net.Sockets;

namespace UdpClientServer
{
    public class ExampleUdpSender : ExampleUdpConnection
    {
        public void Connect(int port)
        {
            _sendEndpoint = new IPEndPoint(IPAddress.Broadcast, port);
            _client = new UdpClient();
            _client.Connect(_sendEndpoint);
        }
    }
}
