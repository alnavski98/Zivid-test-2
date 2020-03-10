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
    public class CameraFunctions
    {
        private delegate void SafeCallDelegate(string text);
        public Baseline baselinePc = zivid_test.Program.f.baselinePc;
        public List<Baseline> baselines = new List<Baseline>();
        public PointCloud pc = new PointCloud();
        public PointCloud blCylinderOut = new PointCloud();
        public PointCloud blCylinderIn = new PointCloud();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public bool runBaseline = false;
        //private int baseLineCount = 0;
        public static float distance;
        public string fileName = "Threshold data 4.csv";
        public int inc = 0; // for counting the number og error numbers

        // takes snapshot, compares it whit baseline and gives distance as deviation
        public void snapshotDistance()
        {
            // for (int i = 0; i < 25; i++)
            // {
            var snaps = new List<PointCloud>();
            var snap = ZividCAM.snapshot();  //Takes snapshot from camera and stores in snap
            var pointCloud = PointCloudHelpers.floatToPointCloud(snap);
            pc = pointCloud;
            Program.f.Update();
            //if baseline is taken, calculate distance. else dont
            if (zivid_test.Program.f.runBaseline)
            {
                //var activeBaseline = baselines.Where(t => t.baseLineId.Equals(baselineIdSim)).ToList();
                //stance = PointCloudHelpers.calculateDistance(pc, activeBaseline.First());
                //FileTransfer.writeCSV(fileName, distance);
                Console.WriteLine(distance);
                inc++;
                Program.f.graph.errorChart();   // making a graph of errornumbers
            }
            else
            {
                Console.WriteLine("Have not taken baseline yet");
            }

            //new code
            //var snapshot = new FileTransfer();
            //snapshot.writeToFile(pc, blFileNames[2]);
            //var myPointcloud = snapshot.readFromFile(blFileNames[2]);
            //PointCloudHelpers.PointCloudToPicture(pc);
            //
            //Thread.Sleep(600);
            // }
        }
    }
}
