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

namespace zivid_test
{
    public partial class Form1 : Form
    {
        private delegate void SafeCallDelegate(string text);
        public PointCloud avg = new PointCloud();
        public PointCloud pc = new PointCloud();
        public bool runBaseline = false;

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

        public void btn_snapshot_Click(object sender, EventArgs e)
        {
             void snapshot()
            {


                var snaps = new List<PointCloud>();
                var snap = ZividCAM.snapshot();  //Takes snapshot from camera and stores in snap
                var pointCloud = PointCloudHelpers.floatToPointCloud(snap);
                snaps.Add(pointCloud);
                pc = PointCloudHelpers.calcAvg(snaps);

                if (runBaseline)
                {
                    //var sw = new Stopwatch();
                    var distance = PointCloudHelpers.calculateDistance(pc.coordinate3d, avg.coordinate3d);
                    Console.WriteLine(distance);
                    //sw.Stop();
                    //var elapsedMs = sw.ElapsedMilliseconds;
                    //Console.WriteLine("Time taken:" + elapsedMs);
                }
                else
                {
                    Console.WriteLine("Have not taken baseline yet");
                }
                //runSnapshot = true;
                /* if (ZividCAM.snapshot())
                 {
                     LoggTXT.Text = "Successfully connected to camera";
                 }
                 else
                 {
                     LoggTXT.Text = "Warning: Must connect to camera before taking snapshot";
                 } */
                //ZividCAM.picture(); 
            }

        }

        private void connect_Click(object sender, EventArgs e)
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

            avg = PointCloudHelpers.calcAvg(snaps);  //Takes the average of all point clouds and creates a new point cloud
            runBaseline = true;

            //var baseline4 = new FileTransfer();
            //baseline4.writeToFile(avg.coordinate3d, "Baseline4.txt");

            //Consider making if or switch statements here that stores baselines in different txt file names
            //depending on which baseline situation we are in

            //Converts 3D-points from average/baseline into string
            //string json = JsonConvert.SerializeObject(avg.coordinate3d, Formatting.Indented);

            //Console.WriteLine(json);
            //Console.ReadKey();

            //Add functionality so that depending on which position the piston is in
            //the baseline is stored in different text files
            int a = 0;
            /*string fullDataPath = Path.Combine("C:\\Users\\alnav\\Desktop\\", "Baseline3.txt"); //Denotes the path and filename
            using (Stream stream = new FileStream(fullDataPath,  //Destination and filename
                                  FileMode.Create,  //Create file
                                  FileAccess.Write,  //Write to file
                                  FileShare.Write))  //Give access writing to file
            {
                IFormatter formatter = new BinaryFormatter(); 
                formatter.Serialize(stream, json);  //Serializes objects
                stream.Close();  //Closes stream
            }*/
            int b = 1;
            /*IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("C:\\Users\\alnav\\Desktop\\Baseline.txt", FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, avg);
            stream.Close();*/


            /*IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("C:\\Users\\alnav\\Desktop\\Baseline.txt", FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, avg);
            stream.Close();*/
            //var test = new PointCloudHelpers
            //Baseline.storeBaseline(avg);

            //int a = 1;

        }

        private void btn_connect_PLS_Click(object sender, EventArgs e)
        {
            PLC.j = true;
            PLC.cancel = false;
            PLC.plcListner();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            ZividCAM.dispose();
        }
    }
}
