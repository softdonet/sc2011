namespace MES.UserControls
{
    partial class ucSuccess
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
            this.tbLoutPanelSuccess = new System.Windows.Forms.TableLayoutPanel();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblScanNumber = new System.Windows.Forms.Label();
            this.tbLoutPanelSuccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLoutPanelSuccess
            // 
            this.tbLoutPanelSuccess.BackColor = System.Drawing.Color.ForestGreen;
            this.tbLoutPanelSuccess.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbLoutPanelSuccess.ColumnCount = 1;
            this.tbLoutPanelSuccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbLoutPanelSuccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbLoutPanelSuccess.Controls.Add(this.lblNote, 0, 2);
            this.tbLoutPanelSuccess.Controls.Add(this.lblStatus, 0, 1);
            this.tbLoutPanelSuccess.Controls.Add(this.lblScanNumber, 0, 0);
            this.tbLoutPanelSuccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLoutPanelSuccess.Location = new System.Drawing.Point(0, 0);
            this.tbLoutPanelSuccess.Name = "tbLoutPanelSuccess";
            this.tbLoutPanelSuccess.RowCount = 3;
            this.tbLoutPanelSuccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.88235F));
            this.tbLoutPanelSuccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.11765F));
            this.tbLoutPanelSuccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tbLoutPanelSuccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbLoutPanelSuccess.Size = new System.Drawing.Size(461, 333);
            this.tbLoutPanelSuccess.TabIndex = 10;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.White;
            this.lblNote.Location = new System.Drawing.Point(159, 276);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(142, 29);
            this.lblNote.TabIndex = 6;
            this.lblNote.Text = "Succeed !";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 72F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(127, 123);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(206, 116);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "OK";
            // 
            // lblScanNumber
            // 
            this.lblScanNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblScanNumber.AutoSize = true;
            this.lblScanNumber.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanNumber.ForeColor = System.Drawing.Color.White;
            this.lblScanNumber.Location = new System.Drawing.Point(57, 28);
            this.lblScanNumber.Name = "lblScanNumber";
            this.lblScanNumber.Size = new System.Drawing.Size(346, 59);
            this.lblScanNumber.TabIndex = 4;
            this.lblScanNumber.Text = "CPF 052972";
            // 
            // ucSuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbLoutPanelSuccess);
            this.Name = "ucSuccess";
            this.Size = new System.Drawing.Size(461, 333);
            this.tbLoutPanelSuccess.ResumeLayout(false);
            this.tbLoutPanelSuccess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbLoutPanelSuccess;
        public System.Windows.Forms.Label lblNote;
        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Label lblScanNumber;
    }
}
