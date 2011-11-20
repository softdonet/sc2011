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
        public ucGridView()
        {
            InitializeComponent();
            SetGridViewStyle();
        }

        Queue<ScanDataModel> queue = new Queue<ScanDataModel>();
        //List<ScanDataModel> listScanDataModel = new List<ScanDataModel>();

        public void InsertNewData(ScanDataModel newScanData)
        {
            if (newScanData != null && queue.SingleOrDefault(e => e.BODYNO == newScanData.BODYNO) != null)
            {
                return;
            }
            if (queue.Count > 4)
            {
                queue.Dequeue();
            }
            queue.Enqueue(newScanData);
            this.dataGrdView.DataSource = queue.OrderByDescending(e => e.ScanTime).ToList();
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
            dataGrdView.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
            dataGrdView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrdView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 40F, FontStyle.Bold);
            dataGrdView.AllowUserToResizeColumns = true;
            dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGrdView.ColumnHeadersHeight = 80;
           
            

            dataGrdView.RowsDefaultCellStyle.Font = new Font("Verdana", 50F, FontStyle.Bold | FontStyle.Bold);

            dataGrdView.AllowUserToResizeRows = true;
            dataGrdView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;// .DisplayedCellsExceptHeaders;
            dataGrdView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGrdView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; ;
            dataGrdView.RowTemplate.Height = 120;
            dataGrdView.RowTemplate.MinimumHeight = 120;

            dataGrdView.RowHeadersVisible = false;
            //this.dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            //  this.dataGrdView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }
    }
}
