using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClientServer
{
    public abstract class ExampleUdpConnection : IDisposable
    {
        protected UdpClient _client;
        protected IPEndPoint _sendEndpoint;
        protected IPEndPoint _receiveEndpoint;

        public void Close()
        {
            _client.Close();
        }

        public virtual int Send(string data)
        {
            byte[] datagram = Encoding.UTF8.GetBytes(data);
            int sent = _client.Send(datagram, datagram.Length);
            return sent;
        }

        public string Read(bool block)
        {
            string data = null;

            if ((_client.Available > 0) || block)
            {
                byte[] received = _client.Receive(ref _receiveEndpoint);
                data = Encoding.UTF8.GetString(received);
            }

            return data;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    try
                    {
                        _client.Close();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        _client.Dispose();
                    }

                }
            }

        }
    }
}
