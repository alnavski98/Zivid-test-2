using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zivid_test
{
    public class Baseline
    {
        /// <summary>
        /// Declares a character to store ID for baselines
        /// and initializes it to a blank string
        /// </summary>
        public char baseLineId = ' ';

        /// <summary>
        /// Declares a PointCloud object 
        /// </summary>
        public PointCloud pc = new PointCloud();

        /// <summary>
        /// Declares a 2D-float array that stores the average distance  
        /// between associated points in multiple pointclouds and the 
        /// baseline (average pointcloud) point in a 1920x1200 ordered treshold map
        /// </summary>
        public float[,] thresholdMap;
    }
}
