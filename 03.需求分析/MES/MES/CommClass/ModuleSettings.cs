using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;

namespace MES.CommClass
{
    #region ���ö���ģ����
    public class ModuleSettings
    {
        private string _comport;
        [DescriptionAttribute("���ж˿����á�"), CategoryAttribute("ɨ��ǹͨѶ����")]
        public string Comport
        {
            get { return _comport; }
            set { _comport = value; }
        }

        private int _baudRate;
        [DescriptionAttribute("���������á�"), CategoryAttribute("ɨ��ǹͨѶ����")]
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }
    }

    #endregion 
}
