using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


namespace SCADA.Web
{
    public partial class StatisticsPage : System.Web.UI.Page
    {

        private DateTime dateValue = new DateTime(2000, 1, 1);

        protected void Page_Load(object sender, EventArgs e)
        {



            //Color 故障 = Color.Red;
            //Color 超高 = Color.Blue;
            //Color 超低 = Color.Yellow;


            //Chart1.Series.Clear();
            //Chart1.Titles.Clear();
            //Chart1.ChartAreas.Clear();


            //string topStr = "统计分析";
            //this.Chart1.Titles.Add(topStr);
            //this.Chart1.ChartAreas.Add(topStr);


            //this.Chart1.ChartAreas[0].AxisX.Margin = false;
            ////this.Chart1.ChartAreas[0].AxisY.Margin = false;
            //this.Chart1.ChartAreas[0].AxisY.Interval = 1;
            //this.Chart1.ChartAreas[0].AxisY.IntervalType = DateTimeIntervalType.Days;
            //this.Chart1.ChartAreas[0].AxisY.LabelStyle.Format = "dd";

            //this.Chart1.Legends[0].Enabled = false;



            //string sername = "故障1";
            //this.Chart1.Series.Add(sername);
            //this.Chart1.Series[sername].Type = SeriesChartType.Gantt;


            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(0), dateValue.AddDays(0.3));
            //this.Chart1.Series[sername].Points[0].Color = 故障;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(13), dateValue.AddDays(15));
            //this.Chart1.Series[sername].Points[1].Color = 超高;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(18), dateValue.AddDays(20));
            //this.Chart1.Series[sername].Points[2].Color = 故障;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(26), dateValue.AddDays(29));
            //this.Chart1.Series[sername].Points[3].Color = 故障;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(6), dateValue.AddDays(12));
            //this.Chart1.Series[sername].Points[4].Color = 超高;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(16), dateValue.AddDays(17));
            //this.Chart1.Series[sername].Points[5].Color = 超低;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(21), dateValue.AddDays(25));
            //this.Chart1.Series[sername].Points[6].Color = 超高;

            //this.Chart1.Series[sername].Points.AddXY(0, dateValue.AddDays(30), dateValue.AddDays(31));
            //this.Chart1.Series[sername].Points[7].Color = 超低;



            //Chart1.Series[sername].Points[0].AxisLabel = "设备1";


            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(1), dateValue.AddDays(3));
            //this.Chart1.Series[sername].Points[8].Color = 故障;
            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(8), dateValue.AddDays(10));
            //this.Chart1.Series[sername].Points[9].Color = 故障;
            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(15), dateValue.AddDays(17));
            //this.Chart1.Series[sername].Points[10].Color = 故障;
            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(22), dateValue.AddDays(24));
            //this.Chart1.Series[sername].Points[11].Color = 故障;

            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(4), dateValue.AddDays(7));
            //this.Chart1.Series[sername].Points[12].Color = 超高;

            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(11), dateValue.AddDays(14));
            //this.Chart1.Series[sername].Points[13].Color = 超高;

            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(18), dateValue.AddDays(21));
            //this.Chart1.Series[sername].Points[14].Color = 超低;

            //this.Chart1.Series[sername].Points.AddXY(1, dateValue.AddDays(25), dateValue.AddDays(31));
            //this.Chart1.Series[sername].Points[15].Color = 超低;

            //Chart1.Series[sername].Points[8].AxisLabel = "设备2";





            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(1), dateValue.AddDays(3));
            //this.Chart1.Series[sername].Points[16].Color = 超高;
            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(8), dateValue.AddDays(10));
            //this.Chart1.Series[sername].Points[17].Color = 超高;
            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(15), dateValue.AddDays(17));
            //this.Chart1.Series[sername].Points[18].Color = 故障;
            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(22), dateValue.AddDays(24));
            //this.Chart1.Series[sername].Points[19].Color = 故障;

            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(4), dateValue.AddDays(7));
            //this.Chart1.Series[sername].Points[20].Color = 超低;

            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(11), dateValue.AddDays(14));
            //this.Chart1.Series[sername].Points[21].Color = 超低;

            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(18), dateValue.AddDays(21));
            //this.Chart1.Series[sername].Points[22].Color = 超低;

            //this.Chart1.Series[sername].Points.AddXY(2, dateValue.AddDays(25), dateValue.AddDays(31));
            //this.Chart1.Series[sername].Points[23].Color = 超高;

            //Chart1.Series[sername].Points[16].AxisLabel = "设备3";



            //this.Chart1.Series[sername].Points.AddXY(3, dateValue.AddDays(1), dateValue.AddDays(10));
            //this.Chart1.Series[sername].Points[24].Color = 超高;
            //this.Chart1.Series[sername].Points.AddXY(3, dateValue.AddDays(10), dateValue.AddDays(20));
            //this.Chart1.Series[sername].Points[25].Color = 超低;
            //this.Chart1.Series[sername].Points.AddXY(3, dateValue.AddDays(20), dateValue.AddDays(31));
            //this.Chart1.Series[sername].Points[26].Color = 故障;

            //Chart1.Series[sername].Points[24].AxisLabel = "设备4";



            //this.Chart1.Series[sername].Points.AddXY(4, dateValue.AddDays(1), dateValue.AddDays(5));
            //this.Chart1.Series[sername].Points[27].Color = 超高;
            //this.Chart1.Series[sername].Points.AddXY(4, dateValue.AddDays(5), dateValue.AddDays(10));
            //this.Chart1.Series[sername].Points[28].Color = 超低;
            //this.Chart1.Series[sername].Points.AddXY(4, dateValue.AddDays(10), dateValue.AddDays(15));
            //this.Chart1.Series[sername].Points[29].Color = 故障;
            //this.Chart1.Series[sername].Points.AddXY(4, dateValue.AddDays(15), dateValue.AddDays(20));
            //this.Chart1.Series[sername].Points[30].Color = 超高;
            //this.Chart1.Series[sername].Points.AddXY(4, dateValue.AddDays(20), dateValue.AddDays(25));
            //this.Chart1.Series[sername].Points[31].Color = 超低;
            //this.Chart1.Series[sername].Points.AddXY(4, dateValue.AddDays(25), dateValue.AddDays(31));
            //this.Chart1.Series[sername].Points[31].Color = 故障;

            //Chart1.Series[sername].Points[27].AxisLabel = "设备5";


        }

    }
}



