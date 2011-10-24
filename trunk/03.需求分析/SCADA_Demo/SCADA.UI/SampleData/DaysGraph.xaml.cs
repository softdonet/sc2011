using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SCADA.UI.SampleData
{
    public partial class DaysGraph : UserControl
    {


        public DaysGraph()
        {
            InitializeComponent();

            DateTime startTime = new DateTime(2011, 1, 1, 0, 0, 0);

            RadChart1.DefaultView.ChartArea.AxisX.AutoRange = false;
            RadChart1.DefaultView.ChartArea.AxisX.MinValue = startTime.ToOADate();
            RadChart1.DefaultView.ChartArea.AxisX.MaxValue = startTime.AddSeconds(86500).ToOADate();
            RadChart1.DefaultView.ChartArea.AxisX.Step = 1d / 24d ;

            this.RadChart1.ItemsSource = GenerateData(startTime);
        }


        private static List<TestData> GenerateData(DateTime startTime)
        {
            List<TestData> data = new List<TestData>();

            Random rand = new Random();

            data.Add(new TestData(startTime, 18));
            for (int i = 1; i < 864; i += 60)
            {
                //if ((i < 20) || (i > 30 && i < 50) || i > 70)
                    data.Add(new TestData(startTime.AddSeconds(i * 100), rand.Next(15, 30)));
                //else
                //    data.Add(new TestData(startTime.AddSeconds(i * 100), rand.Next(15, 30)));
            }

            return data;
        }

       

    }


    public class TestData
    {
        private DateTime _time;
        private int _rpm;

        public TestData(DateTime time, int rpm)
        {
            this.Time = time;
            this.RPM = rpm;
        }
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }
        public int RPM
        {
            get
            {
                return _rpm;
            }
            set
            {
                _rpm = value;
            }
        }
    }


}
