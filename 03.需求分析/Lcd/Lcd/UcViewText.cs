using System;
using System.Drawing;
using System.Windows.Forms;




namespace Lcd
{


    /// <summary>
    /// 自定义窗体
    /// </summary>
    public partial class UcViewText : UserControl
    {

        #region 自定义属性


        public Color ForeColor
        {
            set { this.labelValue.BackColor = value; }
        }

        public Color BackGroudColor
        {
            set { this.BackColor = value; }
        }

        public String DisplayValue
        {
            set { this.labelValue.Text = value; }
        }

        #endregion


        #region 构造函数

        public UcViewText()
        {
            InitializeComponent();
        }

        #endregion





    }
}
