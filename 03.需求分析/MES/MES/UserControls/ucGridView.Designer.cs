namespace MES.UserControls
{
    partial class ucGridView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGrdView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrdView
            // 
            this.dataGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrdView.ColumnHeadersHeight = 30;
            this.dataGrdView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrdView.Location = new System.Drawing.Point(0, 0);
            this.dataGrdView.Name = "dataGrdView";
            this.dataGrdView.RowHeadersWidth = 40;
            this.dataGrdView.RowTemplate.Height = 30;
            this.dataGrdView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrdView.Size = new System.Drawing.Size(491, 290);
            this.dataGrdView.TabIndex = 2;
            // 
            // ucGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrdView);
            this.Name = "ucGridView";
            this.Size = new System.Drawing.Size(491, 290);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGrdView;

    }
}
