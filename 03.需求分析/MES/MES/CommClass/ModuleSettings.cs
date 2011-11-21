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

        private int _barCodeStart;
        [DescriptionAttribute("�����뿪ʼ�ַ�λ�ã���Ŵ�1��ʼ����"), CategoryAttribute("ɨ��ǹͨѶ����")]
        public int BarCodeStart
        {
            get { return _barCodeStart; }
            set { _barCodeStart = value; }
        }

        private int _barCodeLength;
        [DescriptionAttribute("�����볤�ȡ�"), CategoryAttribute("ɨ��ǹͨѶ����")]
        public int BarCodeLength
        {
            get { return _barCodeLength; }
            set { _barCodeLength = value; }
        }
    }

    #endregion 
}
