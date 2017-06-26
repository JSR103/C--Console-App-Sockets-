using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace SocketServerStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Any;
            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);

            try
            {



                listenerSocket.Bind(ipep);

                listenerSocket.Listen(5);

                Socket client = listenerSocket.Accept();

                Console.WriteLine("Client Connected. " + client.ToString() + " IP End Point: " + client.RemoteEndPoint.ToString());

                byte[] buff = new byte[128];//basically how big/size your "thing" that your trying to send can be..if that makes sense 

                int numberOfReceivedBytes = 0;

                while (true)
                {

                    numberOfReceivedBytes = client.Receive(buff);

                    Console.WriteLine("Number of received bytes: " + numberOfReceivedBytes.ToString());

                    Console.WriteLine("Data sent by client is: " + buff);

                    string recivedText = Encoding.ASCII.GetString(buff, 0, numberOfReceivedBytes);

                    Console.WriteLine("Data sent by client is: " + recivedText);

                    client.Send(buff);//Once you press a key if will be sent right away

                    if (recivedText == "x")
                    {
                        break;//Closes the app if buff is x
                    }

                    Array.Clear(buff, 0, buff.Length);//Cleans

                    numberOfReceivedBytes = 0; //sets it back to 0 for no future problems 



                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }

        }
    }
}
