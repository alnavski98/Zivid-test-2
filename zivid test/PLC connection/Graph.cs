using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;


namespace zivid_test.PLC_connection
{
    public class Graph
    {
        public int[] distance = { 100, 200, 500, 1000, 5000, 10000 };
        public int inc;

        public void update(float errorNumber)
        {
            var chart = Program.f.chart2.ChartAreas[0];
            //Random y = new Random();
            //int rInt = y.Next(500, 10000);
            chart.AxisX.Minimum = (inc - 50);        //determining where the axes start from and end at
            chart.AxisX.Maximum = (inc);
            Program.f.chart2.Series["Errornumber"].Points.AddXY(inc, errorNumber);  //adding new points in chart
            inc++;
        }

        public void errorChart()   //making a graph of errornumbers
        {
            if (Program.f.plc.K)
            {
                var chart = Program.f.chart2.ChartAreas[0];

                chart.AxisX.IntervalType = DateTimeIntervalType.Number;

                chart.AxisX.LabelStyle.Format = "";              //Naming axes
                chart.AxisY.LabelStyle.Format = "";
                chart.AxisY.LabelStyle.IsEndLabelVisible = true;

                chart.AxisX.Minimum = inc - 15;        //determining where the axes start from and end at
                chart.AxisX.Maximum = inc;
                chart.AxisY.Minimum = 0;
                chart.AxisY.Maximum = 10000;
                chart.AxisX.Interval = 10;       // determining  the amount to step one unit on the axis.
                chart.AxisY.Interval = 1000;

                Program.f.chart2.Series.Add("Errornumber");
                Program.f.chart2.Series["Errornumber"].ChartType = SeriesChartType.Line; //type of chart, lines between points
                Program.f.chart2.Series["Errornumber"].Color = Color.Red;
                Program.f.chart2.Series[0].IsValueShownAsLabel = false;

                Program.f.chart2.Series["Errornumber"].Points.AddXY(inc, 0);  //adding new points in chart
            }
        }
    }
}