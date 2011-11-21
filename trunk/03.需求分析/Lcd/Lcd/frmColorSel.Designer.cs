﻿namespace Lcd
{
    partial class frmColorSel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.txtForeColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBackColor = new System.Windows.Forms.TextBox();
            this.butOk = new System.Windows.Forms.Button();
            this.butCannel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtForeColor
            // 
            this.txtForeColor.Location = new System.Drawing.Point(110, 40);
            this.txtForeColor.Name = "txtForeColor";
            this.txtForeColor.ReadOnly = true;
            this.txtForeColor.Size = new System.Drawing.Size(122, 21);
            this.txtForeColor.TabIndex = 1;
            this.txtForeColor.DoubleClick += new System.EventHandler(this.txtForeColor_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "前景色：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "背景色：";
            // 
            // txtBackColor
            // 
            this.txtBackColor.Location = new System.Drawing.Point(110, 78);
            this.txtBackColor.Name = "txtBackColor";
            this.txtBackColor.ReadOnly = true;
            this.txtBackColor.Size = new System.Drawing.Size(122, 21);
            this.txtBackColor.TabIndex = 4;
            this.txtBackColor.DoubleClick += new System.EventHandler(this.txtForeColor_DoubleClick);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(70, 125);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 5;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCannel
            // 
            this.butCannel.Location = new System.Drawing.Point(157, 125);
            this.butCannel.Name = "butCannel";
            this.butCannel.Size = new System.Drawing.Size(75, 23);
            this.butCannel.TabIndex = 6;
            this.butCannel.Text = "取消";
            this.butCannel.UseVisualStyleBackColor = true;
            this.butCannel.Click += new System.EventHandler(this.butCannel_Click);
            // 
            // frmColorSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 200);
            this.Controls.Add(this.butCannel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.txtBackColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtForeColor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmColorSel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "颜色选择";
            this.Load += new System.EventHandler(this.frmColorSel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TextBox txtForeColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBackColor;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCannel;
    }
}