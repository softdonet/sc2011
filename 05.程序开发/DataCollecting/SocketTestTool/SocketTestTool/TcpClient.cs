using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SocketTestTool
{
    public class CNetClient
    {

        public Socket clientSocket;
        string ServerIpaddress;
        public CNetClient(string ServerIpaddress)
        {
            this.ServerIpaddress = ServerIpaddress;
        }

        public void Connect()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ipAddress = IPAddress.Parse(ServerIpaddress);
                //Server is listening on port 1000
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 1789);

                //Connect to the server
                clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
                //Start listening to the data asynchronously
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("不能与服务器建立网络连接");
            }
        }


        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "CNetClient_OnConnect", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        public void SendData(byte[] data)
        {
            try
            {
                clientSocket.Send(data, 0, data.Length, SocketFlags.None);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            clientSocket.Close();
        }

    }
}