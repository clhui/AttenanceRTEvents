using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RTEvents
{
    /// <summary>
    /// 多路复用技术(Multiplexing)目的：为了避免创建多个TCP而造成系统资源的浪费和超载，从而有效地利用TCP连接。
    /// </summary>
    public static class RabbitMQFactory
    {
        public static string ip;
        public static int mqport;
        public static string user;
        public static string password;
        private static ConnectionFactory factory;
        private static IConnection sharedConnection;
        private static int ChannelCount { get; set; }
        private static readonly object _locker = new object();

        public static IConnection SharedConnection
        {
            get
            {
                if (ChannelCount >= 1000)
                {
                    if (sharedConnection != null && sharedConnection.IsOpen)
                    {
                        sharedConnection.Close();
                    }
                    sharedConnection = null;
                    ChannelCount = 0;
                }

                lock (_locker)
                {
                    if (sharedConnection == null)
                    {
                        sharedConnection = GetConnection();
                        ChannelCount++;

                    }

                    if (!sharedConnection.IsOpen)
                    {
                        sharedConnection = GetConnection();
                        ChannelCount++;


                    }
                }
                return sharedConnection;
            }
        }

        private static IConnection GetConnection()
        {
            if (factory == null) {

                factory = new ConnectionFactory
                {
                    HostName = ip,
                    UserName = user,
                    Password = password,
                    Port = (mqport == 0)? AmqpTcpEndpoint.UseDefaultPort: mqport,//5672
                    VirtualHost = ConnectionFactory.DefaultVHost,//使用默认值："/"
                                                                 //Protocol = Protocols.DefaultProtocol,
                    AutomaticRecoveryEnabled = true
                };
            }
            return factory.CreateConnection();
        }
    }
}
