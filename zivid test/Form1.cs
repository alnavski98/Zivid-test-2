using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Windows.Forms.DataVisualization.Charting;


namespace zivid_test
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(string text);
        public PointCloud avgPc = new PointCloud();
        public List<PointCloud> baselines = new List<PointCloud>();
        public PointCloud pc = new PointCloud();
        public PointCloud blCylinderOut = new PointCloud();
        public PointCloud blCylinderIn = new PointCloud();
        public PointCloud blCylinderOutSnap = new PointCloud();
        public PointCloud blCylinderInSnap = new PointCloud();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public bool runBaseline = false;
        //private int baseLineCount = 0;
        public string fileName = "Threshold data 3.csv";
        public float distance;
        PLC plc = new PLC();

        Form2 f2 = new Form2();
        int inc = 0;    //increment for adding new points in error chart. 
        int a=47;

        public Form1()
        {     
            InitializeComponent(); //Initializes form
        }
        //noe +1
        public void WriteTextSafe(string text)
        {
            if (LoggTXT.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                LoggTXT.Invoke(d, new object[] { text });
            }
            else
            {
                LoggTXT.AppendText(Environment.NewLine + text);
            }
        }
       
        private void btn_snapshot_Click(object sender, EventArgs e)
        {
            //CameraFunctions functions = new CameraFunctions();
            //functions.snapshotDistance();
            var snaps = new List<PointCloud>();
            var snap = ZividCAM.snapshot();  //Takes snapshot from camera and stores in snap
            var pointCloud = PointCloudHelpers.floatToPointCloud(snap);
            snaps.Add(pointCloud);
            pc = PointCloudHelpers.calcAvg(snaps);

            //if baseline is taken, calculate distance. else dont
            if (runBaseline)
            {
                distance = PointCloudHelpers.calculateDistance(pc.coordinate3d, avgPc.coordinate3d);
                //FileTransfer.writeCSV(fileName, distance);
                Console.WriteLine(distance);
                errorChart();   // making a graph of errornumbers
            }
            else
            {
                Console.WriteLine("Have not taken baseline yet");
            }

            var errorPicture = PointCloudHelpers.PointCloudToPicture(pc);
       
            f2.Show();
            f2.displayPicture(errorPicture);
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
           var connected = ZividCAM.connect();  //Connects to camera
            if (connected)
            {
                LoggTXT.Text = "Successfully connected to camera";
            }
            else
            {
                LoggTXT.Text = "Warning: No cameras found";
            }
        }

        private void btn_assist_mode_Click_1(object sender, EventArgs e)
        {
            ZividCAM.assistMode();  //Takes picture in assisted mode
            if (ZividCAM.assistMode())
            {
                LoggTXT.Text = "Picture taken in assist mode";
            }
            else
            {
                LoggTXT.Text = "Warning: Must connect to camera before using assist mode";
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            ZividCAM.setExposure(int.Parse(ExposureTXT.Text)); //Sets exposure time manually from textbox for exposure 
            ZividCAM.setIris(ulong.Parse(IrisTXT.Text)); //Sets iris manually from textbox for iris  
            if (ZividCAM.setExposure(int.Parse(ExposureTXT.Text)) || ZividCAM.setIris(ulong.Parse(IrisTXT.Text)))
            {
                LoggTXT.Text = "Successfully applied exposure time and/or iris";

            }
            else
            {
                LoggTXT.Text = "Warning: Must connect to camera before applying settings";
            }
        }
       

        private void pictureCntTXT_ValueChanged(object sender, EventArgs e)
        {

        }

        public void btn_baseline_Click(object sender, EventArgs e)
        {
            var snaps = new List<PointCloud>();  //Creates list of objects from PointCloud class

            for (int i = 0; i < int.Parse(pictureCntTXT.Text); i++)  //Loops as many times as the
            {                                                        //user requests a picture          
                var snap = ZividCAM.snapshot();  //Stores array of a point cloud
                var pointCloud = PointCloudHelpers.floatToPointCloud(snap);  //Converts point cloud array to list
                snaps.Add(pointCloud);  //Adds point cloud to list of point clouds
                Thread.Sleep(100);  //Pauses for 100ms
            }

            avgPc = PointCloudHelpers.calcAvg(snaps);  //Stores one baseline in avgPc
            //avgPc.pointcloudId = "Baseline nr. " + baseLineCount;  //String.Format("BaseLineNr{0}", baseLineCount); // = "BaseLineNr" + baseLineCoubt.ToString();, gives ID to a baseline
            //baselines.Add(avgPc);  //Stores baselines in a list
            runBaseline = true;


            //if(baseLineCount == 0)  //If baseline count is 0 store pointcloud in cylinderIn.txt
            //{
                var baseline = new FileTransfer();
                baseline.writeToFile(avgPc, blFileNames[0]);
                //baseLineCount++;
            //}
            /*else if(baseLineCount == 1)  //If baseline count is 1 store pointcloud in cylinderOut.txt
            {
                var baseline = new FileTransfer();
                baseline.writeToFile(avgPc, blFileNames[1]);
                baseLineCount = 0;
            }*/

            // var currentBaseLine = baselines.Where(t => t.pointCloudId = "BaseLineNr0").ToList();
         
            //Consider making if or switch statements here that stores baselines in different txt file names
            //depending on which baseline situation we are in


        }

        private void btn_connect_PLS_Click(object sender, EventArgs e)
        {

            PLC.j = true;
            //PLC.cancel = false;
            plc.RunServerAsync();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            ZividCAM.dispose();
        }

        private void btn_load_baselines_Click(object sender, EventArgs e)
        {
            try
            {
                var fileTransferer = new FileTransfer();
                blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);
                //blCylinderOut = fileTransferer.readFromFile(blFileNames[1]);
            } catch (Exception ex)
            {

                // could not load file
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var fileTransferer = new FileTransfer();
            //var myPointcloud = fileTransferer.readFromFile(blFileNames[0]);
            //PointCloudHelpers.PointCloudToPicture(myPointcloud);
        }

        private void btn_apply_median_filter_Click(object sender, EventArgs e)
        {
            //PointCloudHelpers.MedianFiltering();
        }

        private void Disconnect_PLS_Click(object sender, EventArgs e)
        {
            plc.J = false;
            //PLC.cancel = true;
            //plc.plcListner();
            
        }

        private void errorChart()   //making a graph of errornumbers
        {   
            inc++;
            var chart = chart2.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "Antall feiltall";              //Naming axes
            chart.AxisY.LabelStyle.Format = "Størrelsen på feiltallene";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 1;        //determining where the axes start from and end at
            chart.AxisX.Maximum = 100;
            chart.AxisY.Minimum = 0;
            chart.AxisY.Maximum = 150000;
            chart.AxisX.Interval = 1;       // determining  the amount to step one unit on the axis.
            chart.AxisY.Interval = 15000;

            chart2.Series.Add("Feiltall");
            chart2.Series["Feiltall"].ChartType = SeriesChartType.Line; //type of chart, lines between points
            chart2.Series["Feiltall"].Color = Color.Red;
            chart2.Series[0].IsValueShownAsLabel = false;

            chart2.Series["Feiltall"].Points.AddXY(inc, CameraFunctions.distance);  //adding new points in chart
        }
    }
}
