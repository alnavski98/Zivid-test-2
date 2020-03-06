using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace zivid_test
{
    [Serializable]
    public class PointCloud
    {
        public bool IsEmpty
        { 
            get 
            {
                if (this.getRowSize() == 0 || this.getColumnSize() == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }        
            } 
        }

        /// <summary>
        /// Raw data from camera
        /// </summary>
        public List<List<Point3>> coordinate3d = new List<List<Point3>>();

        /// <summary>
        /// Id of this pointcloud
        /// </summary>
        public string pointcloudId { get; set; } = "";

        /// <summary>
        /// Empty constructor
        /// </summary>
         public PointCloud()  
        {

        }

        /// <summary>
        /// Constructor for storing 3D data
        /// </summary>
        /// <param name="pointCloudData"></param>
        public PointCloud(List<List<Point3>> pointCloudData)
        {
            this.coordinate3d = pointCloudData;
        }

        /// <summary>
        /// Function to return the amount of elements
        /// in the row
        /// </summary>
        /// <returns></returns>
        public int getColumnSize()
        {
            if (coordinate3d[0].Count() == 0)
            {
                return 0;
            }
           return this.coordinate3d[0].Count();
        }

        /// <summary>
        /// Same but for column
        /// </summary>
        /// <returns></returns>
        public int getRowSize()
        {
            if (coordinate3d.Count() == 0)
            {
                return 0;
            }
            return this.coordinate3d.Count();
        }

        public float getMaxZ()
        {
            var maxValue = float.MinValue;

            for (int i = 0; i < this.coordinate3d.Count(); i++)
            {
                for (int j = 0; j < this.coordinate3d[i].Count(); j++)
                {
                    var pZ = this.coordinate3d[i][j].Z;
                    if (pZ > maxValue)
                    {
                        maxValue = pZ;
                    }

                }
            }
            return maxValue;
        }

        public float getMinZ()
        {
            var minValue = float.MaxValue;

            for (int i = 0; i < this.coordinate3d.Count(); i++)
            {
                for (int j = 0; j < this.coordinate3d[i].Count(); j++)
                {
                    var pZ = this.coordinate3d[i][j].Z;
                    if (pZ < minValue)
                    {
                        minValue = pZ;
                    }

                }
            }
            return minValue;
        }

        

    }

    /// <summary>
    /// A 3D array used for testing purposes
    /// </summary>
    public class PointCloudTest 
    {
        public int[,,] array3Da = new int[2, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } },
                                       { { 7, 8, 9 }, { 10, 11, 12 } } };
    }
}
