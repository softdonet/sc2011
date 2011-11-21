using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.CommClass
{
    /// <summary>
    /// 条形码数据解析类
    /// </summary>
    public class MessageFuns
    {
        private byte[] data;
        public void InputData(byte[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// 获取命令头
        /// </summary>
        /// <returns></returns>
        public byte GetCmdHead()
        {
            return data[0];
        }

        /// <summary>
        /// 获取条形码
        /// </summary>
        /// <returns></returns>
        public string GetBarCode(int start, int length)
        {
            byte[] tmpbyte = new byte[length];
            for (int i = 0; i < length; i++)
            {
                tmpbyte[i] = data[start - 1 + i];
            }
            return System.Text.ASCIIEncoding.Default.GetString(tmpbyte);
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            string temp = System.Text.ASCIIEncoding.Default.GetString(data).ToUpper();
            if (temp.IndexOf("NO") > -1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取结束字符
        /// </summary>
        /// <returns></returns>
        public byte GetEndCode()
        {
            return data[data.Length - 1];
        }

        /// <summary>
        /// 获取命令长度
        /// </summary>
        /// <returns></returns>
        public int GetCmdLength()
        {
            return data.Length;
        }

        ///// <summary>
        ///// 计算校验位
        ///// </summary>
        ///// <returns></returns>
        //public byte CountVerifyCode()
        //{
        //    byte b = 0;
        //    for (int i = 2; i < data.Length - 2; i++)
        //    {
        //        b = Convert.ToByte(b ^ data[i]);
        //    }
        //    return b;
        //}

        ///// <summary>
        ///// 从数据中取出校验码
        ///// </summary>
        ///// <returns></returns>
        //public byte GetVerifyCode()
        //{
        //    return data[data.Length - 2];
        //}
    }
}
