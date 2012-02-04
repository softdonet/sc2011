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
        public delegate void TestHandle_R(Test_R test_R);
        private TestHandle_R testEvent_R;
        public event TestHandle_R TestEvent_R
        {
            add
            {
                testEvent_R += value;
            }
            remove
            {
                testEvent_R -= value;
            }
        }

        //定义设备请求配置信息事件
        public delegate void ConfigHandle_R(Config_R config_R);
        private ConfigHandle_R configEvent_R;
        public event ConfigHandle_R ConfigEvent_R
        {
            add
            {
                configEvent_R += value;
            }
            remove
            {
                configEvent_R -= value;
            }
        }


        //定义用户事件
        public delegate void UserEventHandle_R(UserEvent_R userEvent_R);
        private UserEventHandle_R userEventEvent_R;
        public event UserEventHandle_R UserEventEvent_R
        {
            add
            {
                userEventEvent_R += value;
            }
            remove
            {
                userEventEvent_R -= value;
            }
        }

        //定义设备实时信息到达事件
        public delegate void RealTimeDataHandle_R(RealTimeData_R realTimeData_R);
        private RealTimeDataHandle_R realTimeDataEvent_R;
        public event RealTimeDataHandle_R RealTimeDataEvent_R
        {
            add
            {
                realTimeDataEvent_R += value;
            }
            remove
            {
                realTimeDataEvent_R -= value;
            }
        }




        //定义设备实时信息回复事件
        public delegate void RealTimeDataHandle_S(RealTimeData_S realTimeData_S);
        private RealTimeDataHandle_S realTimeDataEvent_S;
        public event RealTimeDataHandle_S RealTimeDataEvent_S
        {
            add
            {
                realTimeDataEvent_S += value;
            }
            remove
            {
                realTimeDataEvent_S -= value;
            }
        }


        //请求固件更新到达事件
        public delegate void FirmwareRequestHandle_R(FirmwareRequest_R firmwareRequest_R);
        private FirmwareRequestHandle_R firmwareRequestEvent_R;
        public event FirmwareRequestHandle_R FirmwareRequestEvent_R
        {
            add
            {
                firmwareRequestEvent_R += value;
            }
            remove
            {
                firmwareRequestEvent_R -= value;
            }
        }

        //设备注册事件
        public delegate void RegisterHandle_R(Register_R register_R);
        private RegisterHandle_R registerEvent_R;
        public event RegisterHandle_R RegisterEvent_R
        {
            add
            {
                registerEvent_R += value;
            }
            remove
            {
                registerEvent_R -= value;
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
                case Command.cmd_Test_R:
                    Test_R tr = new Test_R(tmpdata);
                    if (testEvent_R != null)
                    {
                        this.testEvent_R(tr);
                    }
                    break;
                case Command.cmd_Logout_R:
                    break;
                case Command.cmd_Config_R:
                    Config_R cr = new Config_R(tmpdata);
                    if (this.configEvent_R != null)
                    {
                        this.configEvent_R(cr);
                    }
                    break;
                case Command.cmd_RealTimeDate_R:
                    RealTimeData_R rr = new RealTimeData_R(tmpdata);
                    if (this.realTimeDataEvent_R != null)
                    {
                        this.realTimeDataEvent_R(rr);
                    }
                    break;
                case Command.cmd_UserEvent_R:
                    UserEvent_R ur = new UserEvent_R(tmpdata);
                    if (this.userEventEvent_R != null)
                    {
                        this.userEventEvent_R(ur);
                    }
                    break;
                case Command.cmd_Register_R:
                    Register_R rg = new Register_R(tmpdata);
                    if (this.registerEvent_R != null)
                    {
                        this.registerEvent_R(rg);
                    }
                    break;
                case Command.cmd_FirmwareRequest_R:
                    FirmwareRequest_R fg = new FirmwareRequest_R(tmpdata);
                    if (this.firmwareRequestEvent_R != null)
                    {
                        this.firmwareRequestEvent_R(fg);
                    }
                    break;

                case Command.cmd_Reply:
                    RealTimeData_S rts = new RealTimeData_S(tmpdata);
                    if (this.realTimeDataEvent_S != null)
                    {
                        this.realTimeDataEvent_S(rts);
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
