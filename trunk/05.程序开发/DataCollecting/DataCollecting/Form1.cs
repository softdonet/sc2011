using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace DataCollecting
{
    public partial class Form1 : Form
    {

        struct ClientInfo
        {
            public Socket socket;   //Socket of the client
            public int intDevnum;  //设备编号（设备编号从1开始，0为非法设备）
        }


        public Form1()
        {
            InitializeComponent();

          byte[]  hehe=  BitConverter.GetBytes(10);
        }

        //服务器socket,主侦听socket
        Socket serverSocket;

        ArrayList clientList;
        private void Form1_Load(object sender, EventArgs e)
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

                //Transform the array of bytes received from the user into an
                //intelligent form of object Data
                bool wexit = false;
                int tmpcp = 0;//数组位置指针
                int tmpsize = 0;// BitConverter.ToInt32(byteData, 0);

                // ReceivedCommand(clientSocket, byteData);
                //{
                //    byte[] tmpdata = new byte[tmpsize];

                //    Array.Copy(byteData, tmpcp, tmpdata, 0, tmpsize);
                //    tmpcp = tmpsize;

                //    ReceivedCommand(clientSocket, tmpdata);
                //}

                while (!wexit)
                {
                    try
                    {
                        tmpsize = BitConverter.ToInt32(byteData, 4);
                        if ((tmpsize == 0) || (tmpsize > 100))
                        {
                            wexit = true;
                        }
                        else
                        {
                            byte[] tmpdata = new byte[tmpsize];
                            Array.Copy(byteData, tmpcp, tmpdata, 0, tmpsize);
                            tmpcp = tmpcp + tmpsize;
                            ReceivedCommand(clientSocket, tmpdata);
                        }
                    }
                    catch { }
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
        delegate void SetTextHandle(string str);

        private void ReceivedCommand(Socket clientSocket, byte[] tmpdata)
        {
            try
            {
                SetText(System.Text.Encoding.Default.GetString(tmpdata, 0, tmpdata.Length));
            }
            catch
            { }
        }


        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        private void SetText(string str)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextHandle(SetText), str);
            }
            else
            {
                this.textBox1.Text += str + Environment.NewLine;

            }
        }

    }
}
