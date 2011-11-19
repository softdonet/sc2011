namespace MES.UserControls
{
    partial class ucError
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbLoutPanelError = new System.Windows.Forms.TableLayoutPanel();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblScanNumber = new System.Windows.Forms.Label();
            this.tbLoutPanelError.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLoutPanelError
            // 
            this.tbLoutPanelError.BackColor = System.Drawing.Color.ForestGreen;
            this.tbLoutPanelError.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbLoutPanelError.ColumnCount = 1;
            this.tbLoutPanelError.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbLoutPanelError.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbLoutPanelError.Controls.Add(this.lblNote, 0, 2);
            this.tbLoutPanelError.Controls.Add(this.lblStatus, 0, 1);
            this.tbLoutPanelError.Controls.Add(this.lblScanNumber, 0, 0);
            this.tbLoutPanelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLoutPanelError.Location = new System.Drawing.Point(0, 0);
            this.tbLoutPanelError.Name = "tbLoutPanelError";
            this.tbLoutPanelError.RowCount = 3;
            this.tbLoutPanelError.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.88235F));
            this.tbLoutPanelError.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.11765F));
            this.tbLoutPanelError.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tbLoutPanelError.Size = new System.Drawing.Size(407, 286);
            this.tbLoutPanelError.TabIndex = 13;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.White;
            this.lblNote.Location = new System.Drawing.Point(83, 229);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(240, 29);
            this.lblNote.TabIndex = 6;
            this.lblNote.Text = "READING ERROR";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(99, 93);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(209, 108);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "NG";
            // 
            // lblScanNumber
            // 
            this.lblScanNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblScanNumber.AutoSize = true;
            this.lblScanNumber.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanNumber.ForeColor = System.Drawing.Color.White;
            this.lblScanNumber.Location = new System.Drawing.Point(151, 17);
            this.lblScanNumber.Name = "lblScanNumber";
            this.lblScanNumber.Size = new System.Drawing.Size(105, 59);
            this.lblScanNumber.TabIndex = 4;
            this.lblScanNumber.Text = "NG";
            // 
            // ucError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbLoutPanelError);
            this.Name = "ucError";
            this.Size = new System.Drawing.Size(407, 286);
            this.tbLoutPanelError.ResumeLayout(false);
            this.tbLoutPanelError.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbLoutPanelError;
        public System.Windows.Forms.Label lblNote;
        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Label lblScanNumber;
    }
}
