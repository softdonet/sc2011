using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;

namespace Lcd.CommClass
{
    #region ���ö���ģ����
    public class ModuleSettings
    {
        [ReadOnlyAttribute(true)]
        public int FColor1 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor2 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor3 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor4 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor5 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor6 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor7 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor8 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor9 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor10 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor11 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor12 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor13 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor14 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor15 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor16 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor17 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor18 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor19 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor20 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor21 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor22 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor23 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor24 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor25 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor26 { get; set; }
        [ReadOnlyAttribute(true)]
        public int FColor27 { get; set; }

        [ReadOnlyAttribute(true)]
        public int BColor1 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor2 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor3 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor4 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor5 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor6 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor7 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor8 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor9 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor10 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor11 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor12 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor13 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor14 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor15 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor16 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor17 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor18 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor19 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor20 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor21 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor22 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor23 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor24 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor25 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor26 { get; set; }
        [ReadOnlyAttribute(true)]
        public int BColor27 { get; set; }

        [DescriptionAttribute("������ͣ��ʱ�䡣"), CategoryAttribute("ʱ������")]
        public int MainFormTime { get; set; }
        [DescriptionAttribute("��ӭ����ͣ��ʱ�䡣"), CategoryAttribute("ʱ������")]
        public int WelComeFromTime { get; set; }
        
        [DescriptionAttribute("��ҳ���������С��"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float NumFontSize { get; set; }
        [DescriptionAttribute("��ҳ�������塣"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float NumFontFamily { get; set; }

        [DescriptionAttribute("��ҳ���������С��"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float HZFontSize { get; set; }
        [DescriptionAttribute("��ҳ�������壨�磺���壩��"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float HZFontFamily { get; set; }

        [DescriptionAttribute("��ҳӢ�������С��"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float YWFontSize { get; set; }
        [DescriptionAttribute("��ҳӢ�����塣"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float YWFontFamily { get; set; }

        [DescriptionAttribute("��ҳʱ�������С��"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float TimeFontSize { get; set; }
        [DescriptionAttribute("��ҳʱ�����塣"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float TimeFontFamily { get; set; }

        [DescriptionAttribute("��ҳJPH�����С��"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float JPHFontSize { get; set; }
        [DescriptionAttribute("��ҳJPH���塣"), CategoryAttribute("ϵͳ��ҳ��������")]
        public float JPHFontFamily { get; set; }




        [DescriptionAttribute("��һ�л�ӭ���֡�"), CategoryAttribute("��ӭҳ����������")]
        public string RowOneWelComeText { get; set; }
        [DescriptionAttribute("�ڶ��л�ӭ���֡�"), CategoryAttribute("��ӭҳ����������")]
        public string RowTwoWelComeText { get; set; }
        [DescriptionAttribute("��ӭ�����С��"), CategoryAttribute("��ӭҳ����������")]
        public float FontSize { get; set; }
        [DescriptionAttribute("��ӭ�������壨�磺���壩��"), CategoryAttribute("��ӭҳ����������")]
        public string FontFamily { get; set; }
        [DescriptionAttribute("��ӭ���ֵ����˾��루��λ�����أ���"), CategoryAttribute("��ӭҳ����������")]
        public string WelComeTextTop { get; set; }
        [DescriptionAttribute("��ӭ�����оࣨ��λ�����أ���"), CategoryAttribute("��ӭҳ����������")]
        public string WelComeTextRowledge { get; set; }
        [DescriptionAttribute("��ӭ���汳��ͼƬ����ʽ��jpg����"), CategoryAttribute("��ӭҳ����������")]
        public string WelComeBackgroundImage { get; set; }
    }

    #endregion
}
