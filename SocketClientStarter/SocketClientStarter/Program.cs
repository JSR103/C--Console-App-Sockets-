using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketClientStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = null;

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = null;

            try
            {
                Console.WriteLine("Welcome to my Socket Client Starter Example");
                Console.WriteLine("Please enter a valid IP Address and press enter");

                string strIPAddress = Console.ReadLine();

                Console.WriteLine("Please supply a valid Port Number 0 - 65535 and Press Enter");

                string strPortInput = Console.ReadLine();

                int nPortInput = 0;

                if (!IPAddress.TryParse(strIPAddress, out ipaddr))
                {
                    Console.WriteLine("Invalid server IP Address supplied");
                    return;
                }

                if (!int.TryParse(strPortInput.Trim(), out nPortInput))
                {
                    Console.WriteLine("Invalid Port supplied");
                    return;
                }

                if (nPortInput <= 0 || nPortInput > 65535)//Makes sure that the port is in range 
                {
                    Console.WriteLine("Port Number must be between 0 and 65535");
                    return;
                }

                System.Console.WriteLine(string.Format("IPAddress: {0} - Port{1} ", ipaddr.ToString(), nPortInput));

                client.Connect(ipaddr, nPortInput);//literally connecting to the server

                //Console.ReadKey();

                Console.WriteLine("Connected to the sever, type text and press enter to send it to the server, type <EXIT> to close");

                string inputCommand = string.Empty;

                while (true)//as long as true this while loop will send what the client inputs and sends to the server
                {
                    inputCommand = Console.ReadLine();


                    if (inputCommand.Equals("<EXIT>"))
                    {
                        break;//Exits the while Loop
                    }

                    byte[] buffsend = Encoding.ASCII.GetBytes(inputCommand);

                    client.Send(buffsend);

                    byte[] buffReceived = new byte[128];
                    int nRec = client.Receive(buffReceived);

                    Encoding.ASCII.GetString(buffReceived, 0, nRec);

                    Console.WriteLine("Data received: {0}", Encoding.ASCII.GetString(buffReceived, 0, nRec));


                }

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
            finally//Under here is what exits the app by pressing any key
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);

                    }
                    client.Close();
                    client.Dispose();
                }
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();

        }
    }
}
