using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zivid_test
{
    [Serializable]
    public class Point3
    {
        /// <summary>
        /// Declares 3 floats X, Y and Z as vector points
        /// and initializes all of them to not a number
        /// </summary>
        public float X = float.NaN;
        public float Y = float.NaN;
        public float Z = float.NaN;


        /// <summary>
        /// Variable to store the squared of the error distance of
        /// a certain point to its equivalent point in the baseline
        /// </summary>
        public float errorDistanceSq = 0.0f;

        /// <summary>
        /// Constructor that assings x, y and z coordinates 
        /// to their respective variables
        /// </summary>
        /// <param name="xcoord"></param>
        /// <param name="ycoord"></param>
        /// <param name="zcoord"></param>
        public Point3(float xcoord, float ycoord, float zcoord)
        {
            this.X = xcoord;
            this.Y = ycoord;
            this.Z = zcoord;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Point3()
        {
            
        }
    }
}
