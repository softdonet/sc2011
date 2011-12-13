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

        [DescriptionAttribute("主窗体停留时间。"), CategoryAttribute("时间设置")]
        public int MainFormTime { get; set; }
        [DescriptionAttribute("欢迎窗体停留时间。"), CategoryAttribute("时间设置")]
        public int WelComeFromTime { get; set; }
        
        [DescriptionAttribute("主页数字字体大小。"), CategoryAttribute("系统主页字体设置")]
        public float NumFontSize { get; set; }
        [DescriptionAttribute("主页数字字体。"), CategoryAttribute("系统主页字体设置")]
        public float NumFontFamily { get; set; }

        [DescriptionAttribute("主页汉子字体大小。"), CategoryAttribute("系统主页字体设置")]
        public float HZFontSize { get; set; }
        [DescriptionAttribute("主页汉子字体（如：宋体）。"), CategoryAttribute("系统主页字体设置")]
        public float HZFontFamily { get; set; }

        [DescriptionAttribute("主页英文字体大小。"), CategoryAttribute("系统主页字体设置")]
        public float YWFontSize { get; set; }
        [DescriptionAttribute("主页英文字体。"), CategoryAttribute("系统主页字体设置")]
        public float YWFontFamily { get; set; }

        [DescriptionAttribute("主页时间字体大小。"), CategoryAttribute("系统主页字体设置")]
        public float TimeFontSize { get; set; }
        [DescriptionAttribute("主页时间字体。"), CategoryAttribute("系统主页字体设置")]
        public float TimeFontFamily { get; set; }

        [DescriptionAttribute("主页JPH字体大小。"), CategoryAttribute("系统主页字体设置")]
        public float JPHFontSize { get; set; }
        [DescriptionAttribute("主页JPH字体。"), CategoryAttribute("系统主页字体设置")]
        public float JPHFontFamily { get; set; }




        [DescriptionAttribute("第一行欢迎文字。"), CategoryAttribute("欢迎页面字体设置")]
        public string RowOneWelComeText { get; set; }
        [DescriptionAttribute("第二行欢迎文字。"), CategoryAttribute("欢迎页面字体设置")]
        public string RowTwoWelComeText { get; set; }
        [DescriptionAttribute("欢迎字体大小。"), CategoryAttribute("欢迎页面字体设置")]
        public float FontSize { get; set; }
        [DescriptionAttribute("欢迎文字字体（如：宋体）。"), CategoryAttribute("欢迎页面字体设置")]
        public string FontFamily { get; set; }
        [DescriptionAttribute("欢迎文字到顶端距离（单位：像素）。"), CategoryAttribute("欢迎页面字体设置")]
        public string WelComeTextTop { get; set; }
        [DescriptionAttribute("欢迎文字行距（单位：像素）。"), CategoryAttribute("欢迎页面字体设置")]
        public string WelComeTextRowledge { get; set; }
        [DescriptionAttribute("欢迎界面背景图片（格式：jpg）。"), CategoryAttribute("欢迎页面字体设置")]
        public string WelComeBackgroundImage { get; set; }
    }

    #endregion
}
