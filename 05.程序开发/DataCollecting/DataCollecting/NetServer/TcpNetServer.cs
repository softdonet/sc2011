using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using DataCollecting.Common;
using DataCollecting.NetData;

namespace DataCollecting.NetServer
{
    /// <summary>
    /// TCP Socket通讯
    /// yanghk at 2012-01-01
    /// </summary>
    public class TcpNetServer
    {

        //定义设备测试
        public delegate void TestHandle(Test_R test_R);
        private TestHandle testEvent;
        public event TestHandle TestEvent
        {
            add
            {
                testEvent += value;
            }
            remove
            {
                testEvent -= value;
            }
        }

        //服务器socket,主侦听socket
        private Socket serverSocket;
        ArrayList clientList;
        public TcpNetServer()
        {
            //We are using TCP sockets
            serverSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);
            //端口设置为6622
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 6622);
            //Bind and listen on the given address
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(4);
            //Accept the incoming clients
            serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
        }

        byte[] byteData = new byte[1024];
        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                //Start listening for more clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
                //Once the client connects then start receiving the commands from her
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), clientSocket);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "CNetServer_OnAccept",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                bool wexit = false;
                int tmpcp = 0;//数组位置指针
                int tmpsize = 0;
                Head header = null;
                while (!wexit)
                {
                    tmpsize = BitConverter.ToUInt16(byteData, tmpcp);
                    if (tmpsize == 43605)
                    {
                        header = new Head(byteData);
                        tmpsize = header.CommandCount;
                        byte[] tmpdata = new byte[tmpsize];
                        Array.Copy(byteData, tmpcp, tmpdata, 0, tmpsize);
                        tmpcp = tmpcp + tmpsize;
                        ReceivedCommand(clientSocket, tmpdata, header.CmdCommand);
                    }
                }
            }
            catch
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                int nIndex = 0;
                int tmpdevnum = 0;
                foreach (ClientInfo client in clientList)
                {
                    if (client.socket == clientSocket)
                    {
                        tmpdevnum = client.intDevnum;
                        clientList.RemoveAt(nIndex);
                        break;
                    }
                    ++nIndex;
                }
                clientSocket.Close();

                System.Windows.Forms.MessageBox.Show("设备" + tmpdevnum.ToString() + "强行断开网络！", "点名中心提示");
            }

        }

        /// <summary>
        /// 数据命令
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="tmpdata"></param>
        private void ReceivedCommand(Socket clientSocket, byte[] tmpdata,Command cmd)
        {
            switch (cmd)
            {
                case Command.cmd_Test:
                    Test_R tr = new Test_R(tmpdata);
                    if (testEvent != null)
                    {
                        this.testEvent(tr);
                    }
                    break;
                case Command.cmd_Logout:
                    break;
                case Command.cmd_Config:
                    break;
                case Command.cmd_RealTimeDate:
                    break;
                case Command.cmd_UserEvent:
                    break;
                case Command.cmd_Register:
                    break;
                case Command.cmd_FirmwareRequest:
                    break;
                case Command.cmd_null:
                    break;
                default:
                    break;
            }
        }
    }
}
