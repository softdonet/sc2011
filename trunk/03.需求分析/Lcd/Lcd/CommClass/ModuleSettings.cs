using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;

namespace Lcd.CommClass
{
    #region 配置对象模型类
    public class ModuleSettings
    {
        public int FColor1 { get; set; }
        public int FColor2 { get; set; }
        public int FColor3 { get; set; }
        public int FColor4 { get; set; }
        public int FColor5 { get; set; }
        public int FColor6 { get; set; }
        public int FColor7{ get; set; }
        public int FColor8 { get; set; }
        public int FColor9 { get; set; }
        public int FColor10 { get; set; }
        public int FColor11 { get; set; }
        public int FColor12 { get; set; }
        public int FColor13 { get; set; }
        public int FColor14 { get; set; }
        public int FColor15 { get; set; }
        public int FColor16 { get; set; }
        public int FColor17 { get; set; }
        public int FColor18 { get; set; }
        public int FColor19 { get; set; }
        public int FColor20 { get; set; }
        public int FColor21 { get; set; }
        public int FColor22 { get; set; }
        public int FColor23 { get; set; }
        public int FColor24 { get; set; }
        public int FColor25 { get; set; }
        public int FColor26 { get; set; }
        public int FColor27 { get; set; }

        public int BColor1 { get; set; }
        public int BColor2 { get; set; }
        public int BColor3 { get; set; }
        public int BColor4 { get; set; }
        public int BColor5 { get; set; }
        public int BColor6 { get; set; }
        public int BColor7 { get; set; }
        public int BColor8 { get; set; }
        public int BColor9 { get; set; }
        public int BColor10 { get; set; }
        public int BColor11 { get; set; }
        public int BColor12 { get; set; }
        public int BColor13 { get; set; }
        public int BColor14 { get; set; }
        public int BColor15 { get; set; }
        public int BColor16 { get; set; }
        public int BColor17 { get; set; }
        public int BColor18 { get; set; }
        public int BColor19 { get; set; }
        public int BColor20 { get; set; }
        public int BColor21 { get; set; }
        public int BColor22 { get; set; }
        public int BColor23 { get; set; }
        public int BColor24 { get; set; }
        public int BColor25 { get; set; }
        public int BColor26 { get; set; }
        public int BColor27 { get; set; }


        [DescriptionAttribute("主窗体停留时间。"), CategoryAttribute("系统设置")]
        public int MainFormTime { get; set; }
        [DescriptionAttribute("欢迎窗体停留时间。"), CategoryAttribute("系统设置")]
        public int WelComeFromTime { get; set; }
        [DescriptionAttribute("欢迎文字。"), CategoryAttribute("系统设置")]
        public string WelComeText { get; set; }
      
    }

    #endregion
}
