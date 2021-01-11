using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RTEvents
{
    class NetUtil
    {
        public static bool isPing(String host,int timeout) {
            Ping p1 = new Ping();
            
            PingReply reply = p1.Send(host, timeout); //发送主机名或Ip地址
            StringBuilder sbuilder;
            if (reply.Status == IPStatus.Success)
            {
                sbuilder = new StringBuilder();
                sbuilder.AppendLine(string.Format("PING Address: {0} ", reply.Address.ToString()));
                sbuilder.AppendLine(string.Format("RoundTrip time: {0} ", reply.RoundtripTime));
                sbuilder.AppendLine(string.Format("Time to live: {0} ", reply.Options.Ttl));
                sbuilder.AppendLine(string.Format("Don't fragment: {0} ", reply.Options.DontFragment));
                sbuilder.AppendLine(string.Format("Buffer size: {0} ", reply.Buffer.Length));
                Console.WriteLine(sbuilder.ToString());
                return true;
            }
            else if (reply.Status == IPStatus.TimedOut)
            {
                Console.WriteLine("ping超时》"+host);
                return false;
            }
            else
            {

                Console.WriteLine("ping失败》"+host);
                return false;
            }
        }
        /**
   * 判断端口通不通
   * @param host
   * @param port
   * @return
   */
        public static bool isHostConnectableSyn(String host, int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint point = new IPEndPoint(ip, port);
                socket.ReceiveTimeout = 200;
                socket.SendTimeout = 200;
                socket.Connect(point);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                try
                {
                    socket.Close();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return true;
        }
        /**
  * 判断端口通不通
  * @param host
  * @param port
  * @return
  */
        public static bool isHostConnectable(String host, int port, int timeOut)
        {
           // IPEndPoint ipep = new IPEndPoint(host, port);//IP和端口
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

           
            try
            {
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint point = new IPEndPoint(ip, port);
                ConnectSocketDelegate connect = ConnectSocket;
                IAsyncResult asyncResult = connect.BeginInvoke(point, socket, null, null);

                bool connectSuccess = asyncResult.AsyncWaitHandle.WaitOne(timeOut, false);
                if (!connectSuccess)
                {
                    Console.WriteLine(string.Format("失败！错误信息：{0}", "连接超时"));
                    return false;
                }

                string exmessage = connect.EndInvoke(asyncResult);
                if (!string.IsNullOrEmpty(exmessage))
                {
                    Console.WriteLine(string.Format("失败！错误信息：{0}", exmessage));
                    return false;
                }
               //socket.ReceiveTimeout = 200;
                //socket.SendTimeout = 200;
                //socket.Connect(point);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                try
                {
                    socket.Close();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return true;
        }
        private delegate string ConnectSocketDelegate(IPEndPoint ipep, Socket sock);
        private static string ConnectSocket(IPEndPoint ipep, Socket sock)
        {
            string exmessage = "";
            try
            {
                sock.Connect(ipep);
            }
            catch (System.Exception ex)
            {
                exmessage = ex.Message;
            }
            finally
            {
            }

            return exmessage;
        }
        public static bool checkRemotePort(string ipAddress, int port)
        {
            bool result = false;
            try
            {
                IPAddress ip = IPAddress.Parse(ipAddress);
                IPEndPoint point = new IPEndPoint(ip, port);
                TcpClient tcp = new TcpClient();
                tcp.Connect(point);
                result = true;
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                //10061 Connection is forcefully rejected.
                if (ex.ErrorCode != 10061)
                {
                    Console.WriteLine("10061错误");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //10061 Connection is forcefully rejected.
                return result;
            }
            return result;
        }
    }
}
