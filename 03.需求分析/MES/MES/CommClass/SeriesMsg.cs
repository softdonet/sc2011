using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace MES.CommClass
{

    public class SeriesMsg
    {
        public SerialPort port;
        private List<byte> rlist;
        public delegate void UserDataReceived(object sender, byte[] e);
        public event UserDataReceived OnUserDataReceived;
        public SeriesMsg(ModuleSettings sttings)
        {
            rlist = new List<byte>();
            port = new SerialPort();
            port.BaudRate = sttings.BaudRate;
            port.PortName = sttings.Comport;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.ReadBufferSize = 512;
            port.WriteBufferSize = 512;
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            if (port.IsOpen == false)
            {
                try
                {
                    port.Open();
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    port.Close();
                }
            }
            else
            {
                port.Close();
                System.Threading.Thread.Sleep(200);
                port.Open();
            }
        }

        /// <summary>
        /// 串口收取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] tmpbyte = new byte[port.BytesToRead];
                port.Read(tmpbyte, 0, tmpbyte.Length);
                rlist.AddRange(tmpbyte);
                if (findEndMark(tmpbyte))
                {
                    byte[] tmpdata = new byte[rlist.Count];
                    rlist.CopyTo(0, tmpdata, 0, rlist.Count);
                    rlist.Clear();
                    if (this.OnUserDataReceived != null)
                    {
                        OnUserDataReceived(this, tmpdata);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("收到乱码！");
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                port.Close();
                System.Threading.Thread.Sleep(2000);
                port.Open();
            }
        }

        /// <summary>
        /// 判断是否包含结束位
        /// </summary>
        /// <param name="finddata"></param>
        /// <returns></returns>
        private bool findEndMark(byte[] endData) //结束符判断，现在结束符为 0x0A
        {
            for (int i = 0; i < endData.Length; i++)
            {
                if (endData[i] == 0x0A)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// 析构函数
        /// </summary>
        ~SeriesMsg()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }
    }
}
