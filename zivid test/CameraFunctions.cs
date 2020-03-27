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
using zivid_test.PLC_connection;

namespace zivid_test
{
    public class CameraFunctions
    {
        private delegate void SafeCallDelegate(string text);
        public Baseline baselinePc = zivid_test.Program.f.baselinePc;
        public List<Baseline> baselines = new List<Baseline>();
        public PointCloud pc = new PointCloud();
        //public PointCloud blCylinderOut = new PointCloud();
        //public PointCloud blCylinderIn = new PointCloud();
        public Baseline blCylinderIn = new Baseline();
        public Baseline blCylinderOut = new Baseline();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public FileTransfer fileTransferer = new FileTransfer();
        public bool runBaseline = false;
        //private int baseLineCount = 0;
        public static float distance;
        public string fileName = "Threshold movement 1.csv";
        public int inc = 0; // for counting the number og error numbers
        public PLC plc = new PLC();

        //Takes snapshot, compares it with baseline and gives distance from baseline point
        public float snapshotDistance(Baseline correctBaseline)
        {
            // for (int i = 0; i < 25; i++)
            // {
            var snaps = new List<PointCloud>();
            var snap = ZividCAM.snapshot();  //Takes snapshot from camera and stores in snap
            var pointCloud = PointCloudHelpers.floatToPointCloud(snap);
            pc = pointCloud;
            Program.f.Update();
            //if baseline is taken, calculate distance. else don't
            //if (zivid_test.Program.f.runBaseline)
            //{
            /*while (plc.str1 != 1 || plc.str1 != 2)
            {
                if (plc.str1 == 1)
                {
                    blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);
                    distance = PointCloudHelpers.calculateDistance(pc, blCylinderIn);
                }
                else if (plc.str1 == 2)
                {
                    blCylinderOut = fileTransferer.readFromFile(blFileNames[1]);
                    distance = PointCloudHelpers.calculateDistance(pc, blCylinderOut);
                }
            }*/
            //var activeBaseline = baselines.Where(t => t.baseLineId.Equals(baselineIdSim)).ToList();
            //FileTransfer.writeCSV(fileName, distance);
            distance = PointCloudHelpers.calculateDistance(pc, correctBaseline /*Program.f.baselines[0]*/);
            Console.WriteLine(distance);
                FileTransfer.writeCSV(fileName, distance);
                Program.f.WriteTextSafe("Errornumber: " + distance);
                inc++;
                Program.f.graph.errorChart();   // making a graph of errornumbers
            Program.f.graph.update(distance);
            return distance;
            //}
            //else
            //{
            //    Console.WriteLine("Have not taken baseline yet");
            //}

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
