using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;

namespace MES.CommClass
{
    #region 配置对象模型类
    public class ModuleSettings
    {
        private string _comport;
        [DescriptionAttribute("串行端口设置。"), CategoryAttribute("扫描枪通讯设置")]
        public string Comport
        {
            get { return _comport; }
            set { _comport = value; }
        }

        private int _baudRate;
        [DescriptionAttribute("波特率设置。"), CategoryAttribute("扫描枪通讯设置")]
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        private int _barCodeStart;
        [DescriptionAttribute("条形码开始字符位置（序号从1开始）。"), CategoryAttribute("扫描枪通讯设置")]
        public int BarCodeStart
        {
            get { return _barCodeStart; }
            set { _barCodeStart = value; }
        }

        private int _barCodeLength;
        [DescriptionAttribute("条形码长度。"), CategoryAttribute("扫描枪通讯设置")]
        public int BarCodeLength
        {
            get { return _barCodeLength; }
            set { _barCodeLength = value; }
        }
    }

    #endregion 
}
