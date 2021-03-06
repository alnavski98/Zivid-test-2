﻿using System;
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
        //public Baseline baselinePc;
        public List<Baseline> baselines = new List<Baseline>();
        public PointCloud pc = new PointCloud();
        public FileTransfer fileTransferer = new FileTransfer();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public bool runBaseline = false;
        public string fileName = "Height_differences.csv";
        public float distance;
        public PLC plc = new PLC();
        public Graph graph = new Graph();
        public bool camConnected = false;
        public bool camDisconnected; 
        public bool graphErrorChart = false;

        public int errorNumberIn;
        public int errorNumberOut;
           
        Form2 f2 = new Form2();
        CameraFunctions functions = new CameraFunctions();

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
            CameraFunctions cameraFunctions = new CameraFunctions();
            //for (int i = 0; i < 10; i++)
            //{ 

            /*var snaps = new List<PointCloud>();
            var snap = ZividCAM.snapshot();  //Takes snapshot from camera and stores in snap
            var pointCloud = PointCloudHelpers.floatToPointCloud(snap);
            pc = pointCloud; // PointCloudHelpers.calcBaseline(snaps);*/

            var pc = cameraFunctions.snapshotUniversal();

            //if baseline is taken, calculate distance. else dont
            //if (baselines.Count() > 0)  //If amount of baselines in list > 0 run this
            //{
            //var activeBaseline = baselines.Where(t => t.baseLineId.Equals(baselineIdSim)).ToList();
            /*if (activeBaseline.Count() == 1)
            {
                //distance = PointCloudHelpers.calculateDistance(pc.coordinate3d, activeBaseline.First().pc.coordinate3d);
                distance = PointCloudHelpers.calculateDistance(pc, activeBaseline.First());
            }*/
            /*var fileTransferer = new FileTransfer();
            blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);*/

            //distance = functions.snapshotDistance(baselinePc);
            distance = PointCloudHelpers.calculateDistance(pc, baselinePc);
            //distance = PointCloudHelpers.calculateDistance(pc, baselinePc);

            //}
            FileTransfer.writeCSV(fileName, distance);
            //Console.WriteLine(distance);
            WriteTextSafe("Errorpoints: " + distance);
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
            CameraFunctions cameraFunctions = new CameraFunctions();
            int a;
            for (int i = 0; i < int.Parse(pictureCntTXT.Text); i++)  //Loops as many times as the
            {                                                        //user requests a picture          
                /*var snap = ZividCAM.snapshot();  //Stores array of a point cloud
                var pointCloud = PointCloudHelpers.floatToPointCloud(snap);  //Converts point cloud array to PointCloud object
                snaps.Add(pointCloud);  //Adds point cloud to list of point clouds
                Thread.Sleep(100);  //Pauses for 100ms */
                snaps.Add(cameraFunctions.snapshotUniversal());
                
            }

            baselinePc = PointCloudHelpers.calcBaseline(snaps);  //Stores one baseline in baselinePc
            //baselinePc.baseLineId = baselineId.Text.ToString();
            baselines.Add(baselinePc);
            //avgPc.pointcloudId = "Baseline nr. " + baseLineCount;  //String.Format("BaseLineNr{0}", baseLineCount); // = "BaseLineNr" + baseLineCoubt.ToString();, gives ID to a baseline
            //baselines.Add(avgPc);  //Stores baselines in a list
            //runBaseline = true;

            var baseline = new FileTransfer();
            baseline.writeToFile(baselinePc, blFileNames[0]);

            // var currentBaseLine = baselines.Where(t => t.pointCloudId = "BaseLineNr0").ToList();
         
            //Consider making if or switch statements here that stores baselines in different txt file names
            //depending on which baseline situation we are in


        }

        private void btn_connect_PLS_Click(object sender, EventArgs e)
        {

            plc.connectToPLC = true;
            
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
                var fileTransferer = new FileTransfer();
                plc.blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);
                plc.blCylinderOut = fileTransferer.readFromFile(blFileNames[1]);
            } catch (Exception ex)
            {
                // could not load file
            }
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            //var fileTransferer = new FileTransfer();
            //var myBaseline = fileTransferer.readFromFile(blFileNames[0]);
            //PointCloudHelpers.PointCloudToPicture(myBaseline.pc);
        }*/

        /*private void btn_apply_median_filter_Click(object sender, EventArgs e)
        {
            //PointCloudHelpers.MedianFiltering();
        }*/

        private void Disconnect_PLS_Click(object sender, EventArgs e)
        {
            plc.connectToPLC = false;
            //PLC.cancel = true;
            //plc.plcListner(); 
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btn_set_errornumber_Click(object sender, EventArgs e)
        {
            errorNumberIn = int.Parse(errorNumberInTXT.Text);
            errorNumberOut = int.Parse(errorNumberOutTXT.Text);
        }
    }
}