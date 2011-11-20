﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MES.CommClass;
using System.Collections;

namespace MES.UserControls
{
    public partial class ucGridView : UserControl
    {

        Control mainForm = null;
        public ucGridView(Control ctl)
        {
            InitializeComponent();
            mainForm = ctl;
            SetGridViewStyle();
        }

        public Queue<ScanDataModel> queue = new Queue<ScanDataModel>();
        //List<ScanDataModel> listScanDataModel = new List<ScanDataModel>();

        public void InsertNewData(ScanDataModel newScanData)
        {
            if (newScanData != null && queue.SingleOrDefault(e => e.BODYNO == newScanData.BODYNO) != null)
            {
                return;
            }
            //if (queue.Count > 5)
            //{
            //    queue.Dequeue();
            //}
            queue.Enqueue(newScanData);
            this.dataGrdView.DataSource = queue.OrderByDescending(e => int.Parse(e.SEQ)).ToList();
            //dataGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;

          

            this.dataGrdView.Columns["SEQ"].Width = Convert.ToInt32(mainForm.Width * 0.58 * 6 / 18);
            this.dataGrdView.Columns["BODYNO"].Width = Convert.ToInt32(mainForm.Width * 0.58 * 12 / 18);
            this.dataGrdView.Columns["ScanTime"].Width = Convert.ToInt32(mainForm.Width * 0.42);
            this.dataGrdView.Columns[0].HeaderText = "序 号";
            this.dataGrdView.Columns[1].HeaderText = "车 身 号";
            this.dataGrdView.Columns[2].HeaderText = "扫描时间";
          
            //if (listScanDataModel.Count > 4)
            //{
            //    listScanDataModel.RemoveAt(4);
            //}
            //listScanDataModel.Add(newScanData);
            //this.dataGrdView.DataSource = listScanDataModel.ToList().OrderByDescending(e=>e.ScanTime).ToList();
        }

        private void SetGridViewStyle()
        {

            dataGrdView.ReadOnly = true;
            dataGrdView.EnableHeadersVisualStyles = false;
            dataGrdView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkRed;
            dataGrdView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrdView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 40F, FontStyle.Bold);
            dataGrdView.AllowUserToResizeColumns = true;
            dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGrdView.ColumnHeadersHeight = 80;



            dataGrdView.RowsDefaultCellStyle.Font = new Font("Times New Roman", 45F, FontStyle.Bold | FontStyle.Bold);

            dataGrdView.AllowUserToResizeRows = true;
            dataGrdView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;// .DisplayedCellsExceptHeaders;
            dataGrdView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGrdView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; ;
            dataGrdView.RowTemplate.Height = 120;
            dataGrdView.RowTemplate.MinimumHeight = 95;

            dataGrdView.RowHeadersVisible = false;
            //this.dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            //  this.dataGrdView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }
    }
}
