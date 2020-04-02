using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace zivid_test
{
    public static class Program
    {
        public static Form1 f;
        public static Form2 f2;  //New code
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f = new Form1();
            f2 = new Form2();  //New code
            Application.Run(f);
        }     
    }
}