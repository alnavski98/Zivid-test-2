using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace zivid_test
{
    public static class Program
    {
        public static Form1 f;
        //public static List<List<Point3>> avg = new List<List<Point3>>();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //List<List<Point3>> baseline = new List<List<Point3>>();
            //public PointCloud avg = new PointCloud();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f = new Form1();
            Application.Run(f);
        }
        
    }
}
