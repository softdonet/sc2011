using System;
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
      
        Queue<ScanDataModel> queue=new Queue<ScanDataModel>();
        //List<ScanDataModel> listScanDataModel = new List<ScanDataModel>();

        public void GetNewData(ScanDataModel newScanData)
        {
            if (queue.Count > 4)
            {
                queue.Dequeue();
            }
            queue.Enqueue(newScanData);
            this.dataGrdView.DataSource = queue.OrderByDescending(e=>e.ScanTime).ToList();

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
            dataGrdView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 25F, FontStyle.Bold);
            dataGrdView.AllowUserToResizeColumns = true;
            dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         
            dataGrdView.RowsDefaultCellStyle.Font = new Font("Verdana", 20F, FontStyle.Bold | FontStyle.Bold);


            dataGrdView.AllowUserToResizeRows = true;
            dataGrdView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;// .DisplayedCellsExceptHeaders;
            //this.dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            //  this.dataGrdView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }
    }
}
