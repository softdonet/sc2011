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

        //定义设备请求配置信息事件
        public delegate void ConfigHandle(Config_R config_R);
        private ConfigHandle configEvent;
        public event ConfigHandle ConfigEvent
        {
            add
            {
                configEvent += value;
            }
            remove
            {
                configEvent -= value;
            }
        }


        //定义用户事件
        public delegate void UserEventHandle(UserEvent_R userEvent_R);
        private UserEventHandle userEventEvent;
        public event UserEventHandle UserEventEvent
        {
            add
            {
                userEventEvent += value;
            }
            remove
            {
                userEventEvent -= value;
            }
        }

        //定义设备实时信息到达事件
        public delegate void RealTimeDataHandle(RealTimeData_R realTimeData_R);
        private RealTimeDataHandle realTimeDataEvent;
        public event RealTimeDataHandle RealTimeDataEvent
        {
            add
            {
                realTimeDataEvent += value;
            }
            remove
            {
                realTimeDataEvent -= value;
            }
        }


        //请求固件更新到达事件
        public delegate void FirmwareRequestHandle(FirmwareRequest_R firmwareRequest_R);
        private FirmwareRequestHandle firmwareRequestEvent;
        public event FirmwareRequestHandle FirmwareRequestEvent
        {
            add
            {
                firmwareRequestEvent += value;
            }
            remove
            {
                firmwareRequestEvent -= value;
            }
        }

        //设备注册事件
        public delegate void RegisterHandle(Register_R register_R);
        private RegisterHandle registerEvent;
        public event RegisterHandle RegisterEvent
        {
            add
            {
                registerEvent += value;
            }
            remove
            {
                registerEvent -= value;
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
            //端口设置为1789
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 1789);
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
                    /*---------------一个有效的命令--------------------
                        1.命令头为55AA=43605、
                        2.并且CRC16校验成功
                    -------------------------------------------------*/
                    if (tmpsize != 43605)
                    {
                        wexit = true;
                    }
                    else
                    {
                        header = new Head(byteData);
                        tmpsize = header.CommandCount;
                        byte[] tmpdata = new byte[tmpsize];
                        Array.Copy(byteData, tmpcp, tmpdata, 0, tmpsize);
                        tmpcp = tmpcp + tmpsize;
                        //CRC16校验
                        MessageBase mb = new MessageBase(tmpdata);
                        if (mb.Verify())
                        {
                            ReceivedCommand(clientSocket, tmpdata, header.CmdCommand);
                        }
                    }
                }
            }
            catch
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                int nIndex = 0;
                int tmpdevnum = 0;
                //foreach (ClientInfo client in clientList)
                //{
                //    if (client.socket == clientSocket)
                //    {
                //        tmpdevnum = client.intDevnum;
                //        clientList.RemoveAt(nIndex);
                //        break;
                //    }
                //    ++nIndex;
                //}
                clientSocket.Close();

                System.Windows.Forms.MessageBox.Show("设备" + tmpdevnum.ToString() + "强行断开网络！", "点名中心提示");
            }

        }

        /// <summary>
        /// 数据命令
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="tmpdata"></param>
        private void ReceivedCommand(Socket clientSocket, byte[] tmpdata, Command cmd)
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
                    Config_R cr = new Config_R(tmpdata);
                    if (this.configEvent != null)
                    {
                        this.configEvent(cr);
                    }
                    break;
                case Command.cmd_RealTimeDate:
                    RealTimeData_R rr = new RealTimeData_R(tmpdata);
                    if (this.realTimeDataEvent != null)
                    {
                        this.realTimeDataEvent(rr);
                    }
                    break;
                case Command.cmd_UserEvent:
                    UserEvent_R ur = new UserEvent_R(tmpdata);
                    if (this.userEventEvent != null)
                    {
                        this.userEventEvent(ur);
                    }
                    break;
                case Command.cmd_Register:
                    Register_R rg = new Register_R(tmpdata);
                    if (this.registerEvent != null)
                    {
                        this.registerEvent(rg);
                    }
                    break;
                case Command.cmd_FirmwareRequest:
                    FirmwareRequest_R fg = new FirmwareRequest_R(tmpdata);
                    if (this.firmwareRequestEvent != null)
                    {
                        this.firmwareRequestEvent(fg);
                    }
                    break;
                case Command.cmd_null:
                    break;
                default:
                    break;
            }
            //保持常连接
            //clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
        }
    }
}
