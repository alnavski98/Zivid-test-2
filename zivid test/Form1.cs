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
using zivid_test.PLC_connection;

namespace zivid_test
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(string text);
        public Baseline baselinePc = new Baseline();
        public List<Baseline> baselines = new List<Baseline>();
        public PointCloud pc = new PointCloud();
        /*
        public PointCloud blCylinderOut = new PointCloud();
        public PointCloud blCylinderIn = new PointCloud();
        public PointCloud blCylinderOutSnap = new PointCloud();
        public PointCloud blCylinderInSnap = new PointCloud();
        */
        public Baseline blCylinderIn = new Baseline();
        public Baseline blCylinderOut = new Baseline();
        public FileTransfer fileTransferer = new FileTransfer();
        //public Baseline blCylinderIn = new Baseline();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public bool runBaseline = false;
        //private int baseLineCount = 0;
        public string fileName = "Threshold data 5.csv";
        public float distance;
        public PLC plc = new PLC();
        //CameraFunctions functions = new CameraFunctions();
        public Graph graph = new Graph();
        public bool camConnected = false;
        public bool camDisconnected; //= false;
        public bool graphErrorChart = false;

        Form2 f2 = new Form2();
        
        public Form1()
        {     
            InitializeComponent(); //Initializes form
        }

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
            //for (int i = 0; i < 10; i++)
            //{ 
                //functions.snapshotDistance();
            var snaps = new List<PointCloud>();
            var snap = ZividCAM.snapshot();  //Takes snapshot from camera and stores in snap
            var pointCloud = PointCloudHelpers.floatToPointCloud(snap);
            pc = pointCloud; // PointCloudHelpers.calcBaseline(snaps);
            int a = 1;
            while(plc.str1 != 1 || plc.str1 != 2)
            {
                if(plc.str1 == 1)
                {
                    blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);
                    distance = PointCloudHelpers.calculateDistance(pc, blCylinderIn);
                }
                else if(plc.str1 == 2)
                {
                    blCylinderOut = fileTransferer.readFromFile(blFileNames[1]);
                    distance = PointCloudHelpers.calculateDistance(pc, blCylinderOut);
                }
                //else
                //{
                //    WriteTextSafe("Invalid baseline ID, try again");
                //}
            }
            //if baseline is taken, calculate distance. else dont
            //if (baselines.Count() > 0)  //If amount of baselines in list > 0 run this
            //{
            //var activeBaseline = baselines.Where(t => t.baseLineId.Equals(baselineIdSim)).ToList();
            /*if (activeBaseline.Count() == 1)
            {
                //distance = PointCloudHelpers.calculateDistance(pc.coordinate3d, activeBaseline.First().pc.coordinate3d);
                distance = PointCloudHelpers.calculateDistance(pc, activeBaseline.First());
            }*/
            //if (plc.str1 == '1')  //If signal from plc is 1, calculate distance between pointcloud
            //{                     //of picture taken with the "first" baseline pointcloud
            /*var fileTransferer = new FileTransfer();
            blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);*/
            distance = PointCloudHelpers.calculateDistance(pc, baselinePc);
            //}
            /*else if (plc.str1 == '2')  //Same but with "second" baseline pointcloud
            {
                distance = PointCloudHelpers.calculateDistance(pc, baselines[1]);
            }
            else  //Else give message for invalid baseline ID
            {
                WriteTextSafe("Invalid baseline ID, cannot calculate distance." + Environment.NewLine
                               + Environment.NewLine + "Use value of 1 or 2.");
            }*/
            //FileTransfer.writeCSV(fileName, distance);
            //Console.WriteLine(distance);
            WriteTextSafe("Errornumber: " + distance);
            //}
            /*else
            {
                WriteTextSafe("Have not taken baseline yet");
            }*/
            graph.update(distance);
            //}
            if(!String.IsNullOrEmpty(BitmapTXT.Text))  //If Bitmap filename is NOT empty write to
            {                                          //png file and show "heatmap"
            var errorPicture = PointCloudHelpers.PointCloudToPicture(pc, BitmapTXT.Text);

            f2.Show();
            f2.displayPicture(errorPicture);
            }
            else  //Else say that bitmap filename has not been given
            {
                WriteTextSafe("Filename for bitmap has not been given yet, please try again.");
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (!camConnected)  //If camera is not already connected, connect camera
            {
                var connected = ZividCAM.connect();  //Connects to camera
                camConnected = true;
                camDisconnected = false;
                if (connected)
                {
                    WriteTextSafe("Successfully connected to camera");
                }
                else
                {
                    WriteTextSafe("Warning: No cameras found");
                }
                if(!graphErrorChart)
                {
                    graph.errorChart();
                    graphErrorChart = true;
                }
            }
            else  
            {
                WriteTextSafe("Camera is already connected");
            }
        }

        private void btn_assist_mode_Click_1(object sender, EventArgs e)
        {
            ZividCAM.assistMode();  //Takes picture in assisted mode
            if (ZividCAM.assistMode())
            {
                WriteTextSafe( "Picture taken in assist mode");
            }
            else
            {
                WriteTextSafe( "Warning: Must connect to camera before using assist mode");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            ZividCAM.setExposure(int.Parse(ExposureTXT.Text)); //Sets exposure time manually from textbox for exposure 
            ZividCAM.setIris(ulong.Parse(IrisTXT.Text)); //Sets iris manually from textbox for iris  
            if (ZividCAM.setExposure(int.Parse(ExposureTXT.Text)) || ZividCAM.setIris(ulong.Parse(IrisTXT.Text)))
            {
                WriteTextSafe( "Successfully applied exposure time and/or iris");

            }
            else
            {
                WriteTextSafe( "Warning: Must connect to camera before applying settings");
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
                var pointCloud = PointCloudHelpers.floatToPointCloud(snap);  //Converts point cloud array to PointCloud object
                snaps.Add(pointCloud);  //Adds point cloud to list of point clouds
                Thread.Sleep(100);  //Pauses for 100ms
            }

            baselinePc = PointCloudHelpers.calcBaseline(snaps);  //Stores one baseline in baselinePc
            //baselinePc.baseLineId = baselineId.Text.ToString();
            //baselines.Add(baselinePc);
            //avgPc.pointcloudId = "Baseline nr. " + baseLineCount;  //String.Format("BaseLineNr{0}", baseLineCount); // = "BaseLineNr" + baseLineCoubt.ToString();, gives ID to a baseline
            //baselines.Add(avgPc);  //Stores baselines in a list
            //runBaseline = true;

            /*if (plc.str1 == '0')
            {
                 WriteTextSafe("No baseline taken, sensor did not trigger");
            }
            else if (plc.str1 == '1')
            {
                baselines[0] = PointCloudHelpers.calcBaseline(snaps);
                baselines[0].baseLineId = plc.str1; //Compare with baseline while cylinder is in
            }
            else if (plc.str1 == '2') 
            {
                baselines[1] = PointCloudHelpers.calcBaseline(snaps);
                baselines[1].baseLineId = plc.str1; //Compare with baseline while cylinder is out
            }*/
            int a = 1;
            var baseline = new FileTransfer();
            baseline.writeToFile(baselinePc, blFileNames[1]);
            int b = 2;
            //if(baseLineCount == 0)  //If baseline count is 0 store pointcloud in cylinderIn.txt
            //{
                //var baseline = new FileTransfer();
               // baseline.writeToFile(avgPc, blFileNames[0]);
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

            plc.J = true;
            //PLC.cancel = false;
            plc.RunServerAsync();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            if (!camDisconnected)  //If camera is not disconnected, disconnect camera
            {
                camDisconnected = true;
                camConnected = false;
                ZividCAM.dispose();
            }
            else
            {
                WriteTextSafe("Camera is already disconnected");
            }
        }

        private void btn_load_baselines_Click(object sender, EventArgs e)
        {
            try
            {
                //var fileTransferer = new FileTransfer();
                //blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);
                //blCylinderOut = fileTransferer.readFromFile(blFileNames[1]);
            } catch (Exception ex)
            {
                // could not load file
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var fileTransferer = new FileTransfer();
            //var myBaseline = fileTransferer.readFromFile(blFileNames[0]);
            //PointCloudHelpers.PointCloudToPicture(myBaseline.pc);
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
    }
}