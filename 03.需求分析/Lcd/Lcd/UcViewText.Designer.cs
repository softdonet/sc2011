﻿namespace Lcd
{
    partial class UcViewText
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelValue
            // 
            this.labelValue.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelValue.Location = new System.Drawing.Point(128, 84);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(100, 30);
            this.labelValue.TabIndex = 0;
            this.labelValue.Text = "T1";
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UcViewText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelValue);
            this.Name = "UcViewText";
            this.Size = new System.Drawing.Size(395, 196);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelValue;
    }
}
