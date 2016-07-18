using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket_XML_Send_Receive_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                string ip_ext = args[0]; // TO DO
                int port_send_ext = 10000;
                string encoding = "ASCII";
                string content = "asdsa";
                bool shouldAddLengthPrefix = false;
                try
                {
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip_ext), port_send_ext);
                    server2.Connect(serverEndPoint);
                    Console.WriteLine("CLIENT: conectat la server socket <" + ip_ext + ":" + port_send_ext.ToString() + ">");
                    var bytesToSend = StringConverter.GetBytesToSend(encoding
                                                  , content
                                                  , shouldAddLengthPrefix);
                    server2.Send(bytesToSend, 0, bytesToSend.Length, SocketFlags.None);
                    Console.WriteLine("CLIENT: date expediate de la client la server socket.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CLIENT: probleme conectare/trimitere de la client la server socket <" + ip_ext + ":" + port_send_ext.ToString() + ">");
                    //Debug(ex.ToString());
                }
                finally
                {
                    if (server2 != null)
                    {
                        server2.Close();
                        ((IDisposable)server2).Dispose();
                        Console.WriteLine("CLIENT: deconectat de la server socket");
                    };
                };

            }
        }
    }
}
