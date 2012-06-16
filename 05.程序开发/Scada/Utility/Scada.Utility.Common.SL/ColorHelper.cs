using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Scada.Utility.Common.SL
{
    public static class  ColorHelper
    {
        /// <summary>
        /// 16进制色字符串转ARGB
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public static Color ToColor(this string colorName)
        {
            if (colorName.StartsWith("#"))
                colorName = colorName.Replace("#", string.Empty);
            int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
            return new Color()
            {
                A = Convert.ToByte((v >> 24) & 255),
                R = Convert.ToByte((v >> 16) & 255),
                G = Convert.ToByte((v >> 8) & 255),
                B = Convert.ToByte((v >> 0) & 255)
            };
        }


        /// <summary>
        /// Color转Int32
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int ToArgb(this Color color)
        {
            int argb = color.A << 24;
            argb += color.R << 16;
            argb += color.G << 8;
            argb += color.B;
            return argb;
        }

    }
}
