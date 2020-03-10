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
        /// Declares a PointCloud object 
        /// </summary>
        public PointCloud pc = new PointCloud();

        /// <summary>
        /// Declares a 2D-float array to store standard deviation for each
        /// point in a 1920x1200 ordered pointcloud as a threshold map
        /// </summary>
        public float[,] thresholdMap;

        /// <summary>
        /// Declares a string to store ID for baselines
        /// and initializes it to a blank string
        /// </summary>
        public string baseLineId = "";
    }
}
