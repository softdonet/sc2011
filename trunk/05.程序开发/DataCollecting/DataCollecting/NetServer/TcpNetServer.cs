using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using NetData;
using BusinessRules;
using Utility;

namespace DataCollecting.NetServer
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class DeviceClient
    {

        public DeviceClient(Socket clientSocket)
        {
            ByteData = new byte[1024];
            ClientSocket = clientSocket;
        }
        public Socket ClientSocket { get; set; }
        public byte[] ByteData { get; set; }

        /// <summary>
        /// 清除缓冲区
        /// </summary>
        public void ClearBuffer()
        {
            for (int i = 0; i < ByteData.Length; i++)
            {
                ByteData[i] = 0x00;
            }
        }

        /// <summary>
        /// 获取转化后的字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] GetRealByteData()
        {
            return StringHelper.ConvertDataUp(ByteData);
        }

        public void Send(byte[] data)
        {
            ClientSocket.Send(StringHelper.ConvertDataDown(data));
        }
    }


    /// <summary>
    /// TCP Socket通讯
    /// yanghk at 2012-01-01
    /// </summary>
    public class TcpNetServer
    {
        BLL bll = new BLL();

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

        //定义设备测试
        public delegate void TestHandle_S(Test_S test_S);
        private TestHandle_S testEvent_S;
        public event TestHandle_S TestEvent_S
        {
            add
            {
                testEvent_S += value;
            }
            remove
            {
                testEvent_S -= value;
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

        //系统异常事件
        public delegate void SystenErrorHandle(Exception ex);
        private SystenErrorHandle systenError;
        public event SystenErrorHandle SystenErrorEvent
        {
            add
            {
                systenError += value;
            }
            remove
            {
                systenError -= value;
            }
        }


        //服务器socket,主侦听socket
        private Socket serverSocket;
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


        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                //Start listening for more clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
                //Once the client connects then start receiving the commands from her
                DeviceClient deviceClient = new DeviceClient(clientSocket);
                clientSocket.BeginReceive(deviceClient.ByteData, 0, deviceClient.ByteData.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), deviceClient);
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
                DeviceClient deviceClient = (DeviceClient)ar.AsyncState;
                try
                {
                    deviceClient.ClientSocket.EndReceive(ar);
                }
                catch
                {
                    deviceClient.ClientSocket.Close();
                    return;
                }
                //如果客户端发来一串0x00,就说明其主动关闭了连接
                if (deviceClient.ByteData[0] == 0x00)
                {
                    deviceClient.ClientSocket.Close();
                    return;
                }
                //数据转化
                byte[] realData = deviceClient.GetRealByteData();
                bool wexit = false;
                int tmpcp = 0;//数组位置指针
                int tmpsize = 0;
                Head header = null;

                while (!wexit)
                {
                    tmpsize = BitConverter.ToUInt16(realData, tmpcp);
                    /*---------------一个有效的命令--------------------
                        1.命令头为55AA=Const.UP_HEADER、
                        2.并且CRC16校验成功
                    -------------------------------------------------*/
                    if (tmpsize != Const.UP_HEADER)
                    {
                        wexit = true;
                    }
                    else
                    {
                        header = new Head(realData);
                        tmpsize = header.CommandCount;
                        byte[] tmpdata = new byte[tmpsize];
                        Array.Copy(realData, tmpcp, tmpdata, 0, tmpsize);
                        tmpcp = tmpcp + tmpsize;
                        //CRC16校验
                        MessageBase mb = new MessageBase(tmpdata);
                        if (mb.Verify())
                        {
                            ReceivedCommand(deviceClient, tmpdata, header.CmdCommand);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeviceClient deviceClient = (DeviceClient)ar.AsyncState;
                deviceClient.ClientSocket.Close();
                LogHelper.WriteExceptionLog(ex);
                if (this.systenError != null)
                {
                    this.systenError(ex);
                }

            }

        }

        /// <summary>
        /// 数据命令
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="tmpdata"></param>
        private void ReceivedCommand(DeviceClient deviceClient, byte[] tmpdata, Command cmd)
        {
            switch (cmd)
            {
                //测试命令
                case Command.cmd_Test_R:
                    Test_R tr = new Test_R(tmpdata);
                    if (testEvent_R != null)
                    {
                        this.testEvent_R(tr);
                    }
                    //begin---------------------
                    Head h = new Head();
                    h.CmdHeader = Const.DOWN_HEADER;
                    h.CmdCommand = Command.cmd_Test_R;
                    h.DataContext = tr.Header.DataContext;
                    h.DeviceSN = tr.Header.DeviceSN;
                    h.State = 1;
                    h.SateTimeMark = DateTime.Now;

                    Test_S ts = new Test_S();
                    ts.Header = h;
                    ts.Content = tr.Content;
                    deviceClient.Send(ts.ToByte());
                    if (testEvent_S != null)
                    {
                        this.testEvent_S(new Test_S(ts.ToByte()));
                    }
                    //end---------------------
                    break;
                //设备退出命令
                case Command.cmd_Logout_R:
                    break;
                //设备请求配置信息
                case Command.cmd_Config_R:
                    Config_R cr = new Config_R(tmpdata);
                    if (this.configEvent_R != null)
                    {
                        this.configEvent_R(cr);
                    }
                    //begin
                    Head hconfig = new Head();
                    hconfig.CmdHeader = Const.DOWN_HEADER;
                    hconfig.CmdCommand = Command.cmd_Reply;
                    hconfig.DataContext = cr.Header.DataContext;
                    hconfig.DeviceSN = cr.Header.DeviceSN;
                    hconfig.SateTimeMark = DateTime.Now;
                    if (Comm.UpdateToDB)
                    {
                        if (bll.RefreshData(cr))
                        {
                            hconfig.State = 1;
                        }
                        else
                        {
                            hconfig.State = 2;
                        }
                    }
                    else
                    {
                        hconfig.State = 1;
                    }
                    ReplyBase_S replyBase_S_config = bll.GetDeviceInfor(cr.Header.DeviceSN);
                    replyBase_S_config.Header = hconfig;
                    deviceClient.Send(replyBase_S_config.ToByte());
                    if (this.realTimeDataEvent_S != null)
                    {
                        this.realTimeDataEvent_S(new RealTimeData_S(replyBase_S_config.ToByte()));
                    }
                    //end
                    break;
                //设备实时数据上传
                case Command.cmd_RealTimeDate_R:
                    RealTimeData_R rr = new RealTimeData_R(tmpdata);
                    if (this.realTimeDataEvent_R != null)
                    {
                        this.realTimeDataEvent_R(rr);
                    }
                    //begin
                    Head hRealTime = new Head();
                    hRealTime.CmdHeader = Const.DOWN_HEADER;
                    hRealTime.CmdCommand = Command.cmd_Reply;
                    hRealTime.DataContext = rr.Header.DataContext;
                    hRealTime.DeviceSN = rr.Header.DeviceSN;
                    hRealTime.SateTimeMark = DateTime.Now;
                    if (Comm.UpdateToDB)
                    {
                        if (bll.SaveRealTimeData(rr))
                        {
                            hRealTime.State = 1;
                        }
                        else
                        {
                            hRealTime.State = 2;
                        }
                    }
                    else
                    {
                        hRealTime.State = 1;
                    }
                    ReplyBase_S replyBase_S_realTime = bll.GetDeviceInfor(rr.Header.DeviceSN);
                    replyBase_S_realTime.Header = hRealTime;
                    deviceClient.Send(replyBase_S_realTime.ToByte());
                    if (this.realTimeDataEvent_S != null)
                    {
                        this.realTimeDataEvent_S(new RealTimeData_S(replyBase_S_realTime.ToByte()));
                    }
                    //end
                    break;
                //设备用户事件告知
                case Command.cmd_UserEvent_R:
                    UserEvent_R ur = new UserEvent_R(tmpdata);
                    if (this.userEventEvent_R != null)
                    {
                        this.userEventEvent_R(ur);
                    }
                    //begin
                    Head hUserEvent = new Head();
                    hUserEvent.CmdHeader = Const.DOWN_HEADER;
                    hUserEvent.CmdCommand = Command.cmd_Reply;
                    hUserEvent.DataContext = ur.Header.DataContext;
                    hUserEvent.DeviceSN = ur.Header.DeviceSN;
                    hUserEvent.SateTimeMark = DateTime.Now;
                    if (Comm.UpdateToDB)
                    {
                        if (bll.SaveUserEvent(ur))
                        {
                            hUserEvent.State = 1;
                        }
                        else
                        {
                            hUserEvent.State = 2;
                        }
                    }
                    else
                    {
                        hUserEvent.State = 1;
                    }
                    ReplyBase_S replyBase_S_userEvent = bll.GetDeviceInfor(ur.Header.DeviceSN);
                    replyBase_S_userEvent.Header = hUserEvent;
                    deviceClient.Send(replyBase_S_userEvent.ToByte());
                    if (this.realTimeDataEvent_S != null)
                    {
                        this.realTimeDataEvent_S(new RealTimeData_S(replyBase_S_userEvent.ToByte()));
                    }
                    //end
                    break;
                //设备注册
                case Command.cmd_Register_R:
                    Register_R rg = new Register_R(tmpdata);
                    if (this.registerEvent_R != null)
                    {
                        this.registerEvent_R(rg);
                    }
                    break;

                //设备请求固件更新
                case Command.cmd_FirmwareRequest_R:
                    FirmwareRequest_R fg = new FirmwareRequest_R(tmpdata);
                    if (this.firmwareRequestEvent_R != null)
                    {
                        this.firmwareRequestEvent_R(fg);
                    }
                    break;
                //服务器回复(仅做测试用)
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
            deviceClient.ClearBuffer();
            try
            {
                deviceClient.ClientSocket.BeginReceive(deviceClient.ByteData, 0, deviceClient.ByteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), deviceClient);
            }
            catch
            {

            }
        }
    }
}
