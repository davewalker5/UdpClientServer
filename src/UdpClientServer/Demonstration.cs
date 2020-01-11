using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace UdpClientServer
{
    public class Demonstration
    {
        private const int ClientServerPort = 8889;
        private const int SenderListenerPort = 8890;

        private CancellationTokenSource _source = null;
        private int _pid;

        public Demonstration()
        {
            Process process = Process.GetCurrentProcess();
            _pid = process.Id;
        }

        public void StartServer()
        {
            ExampleUdpServer server = new ExampleUdpServer();
            server.Connect(ClientServerPort);

            bool isStop = false;
            do
            {
                try
                {
                    string data = server.Read(true);
                    Console.WriteLine($"{_pid} : Read {data.Length} bytes : {data}");
                    server.Send("ok");
                    isStop = data.Trim().ToLower() == "stop";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{_pid} : {ex.Message}");
                }
            }
            while (!isStop);

            // If we have a background process on another thread, cancel it
            if (_source != null)
            {
                _source.Cancel();
                _source.Dispose();
            }

            server.Close();
            server.Dispose();
        }

        public void StartSender()
        {
            ExampleUdpSender sender = new ExampleUdpSender();
            sender.Connect(SenderListenerPort);

            while (true)
            {
                try
                {
                    string data = DateTime.Now.ToString();
                    int sent = sender.Send(data);
                    Console.WriteLine($"{_pid} : Sent {sent} bytes : {data}");
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{_pid} : {ex.Message}");
                }
            }
        }

        public void StartBackgroundSender()
        {
            _source = new CancellationTokenSource();
            CancellationToken token = _source.Token;

            var task = Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                ExampleUdpSender sender = new ExampleUdpSender();
                sender.Connect(SenderListenerPort);

                while (true)
                {
                    try
                    {
                        string data = DateTime.Now.ToString();
                        int sent = sender.Send(data);
                        Console.WriteLine($"{_pid} : Sent {sent} bytes : {data}");
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.fff")} Status Sender : {ex.Message}");
                    }

                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                        sender.Close();
                        sender.Dispose();
                    }
                }

            }, token);
        }

        public void StartClient()
        {
            ExampleUdpClient client = new ExampleUdpClient();
            client.Connect(IPAddress.Loopback, ClientServerPort);

            bool haveData;
            bool stop = false;
            do
            {
                Console.Write("Data or [ENTER] to quit : ");
                string data = Console.ReadLine().Trim();
                haveData = !string.IsNullOrEmpty(data);
                if (haveData)
                {
                    int sent = client.Send(data);
                    Console.WriteLine($"{_pid} : Sent {sent} bytes : {data}");

                    string response = client.Read(true);
                    Console.WriteLine($"{_pid} : Read {response.Length} bytes : {response}");

                    stop = data.Trim().ToLower() == "stop";
                }
            }
            while (haveData && !stop);

            // If we have a background process on another thread, cancel it
            if (_source != null)
            {
                _source.Cancel();
                _source.Dispose();
            }

            client.Close();
            client.Dispose();
        }

        public void StartListener()
        {
            ExampleUdpListener listener = new ExampleUdpListener();
            listener.Connect(SenderListenerPort);

            while (true)
            {
                try
                {
                    string data = listener.Read(true);
                    Console.WriteLine($"{_pid} : Read {data.Length} bytes : {data}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{_pid} : {ex.Message}");
                }
            }
        }

        public void StartBackgroundListener()
        {
            _source = new CancellationTokenSource();
            CancellationToken token = _source.Token;

            var task = Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                ExampleUdpListener listener = new ExampleUdpListener();
                listener.Connect(SenderListenerPort);

                while (true)
                {
                    try
                    {
                        string data = listener.Read(true);
                        Console.WriteLine($"{_pid} : Read {data.Length} bytes : {data}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.fff")} Status Sender : {ex.Message}");
                    }

                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                        listener.Close();
                        listener.Dispose();
                    }
                }

            }, token);
        }
    }
}
