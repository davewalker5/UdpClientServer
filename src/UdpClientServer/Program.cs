namespace UdpClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0].Trim().ToLower())
            {
                case "server":
                    new Demonstration().StartServer();
                    break;
                case "sender":
                    new Demonstration().StartSender();
                    break;
                case "client":
                    new Demonstration().StartClient();
                    break;
                case "listener":
                    new Demonstration().StartListener();
                    break;
                case "server-sender":
                    Demonstration serverSender = new Demonstration();
                    serverSender.StartBackgroundSender();
                    serverSender.StartServer();
                    break;
                case "client-listener":
                    Demonstration clientListener = new Demonstration();
                    clientListener.StartBackgroundListener();
                    clientListener.StartClient();
                    break;
                default:
                    break;
            }
        }

    }
}
