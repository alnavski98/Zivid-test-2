using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace zivid_test
{
    /* public class Baseline
     {
         public List<List<Point3>> baseline = new List<List<Point3>>();
         public Baseline(List<List<Point3>> avgPc)
         {
             baseline = avgPc;
         }

         public Baseline()
         {

         }
     }*/

    //PointCloud avg = new PointCloud;
    /// <summary>
    /// Helping class for point cloud
    /// </summary>
    public static class PointCloudHelpers
    {
        
        
        /// <summary>
        /// Converting 3D float array to point cloud
        /// </summary>
        /// <param name="zividPointCloud">3D float array</param>
        /// <returns>List of PointCloud instances</returns>
        public static PointCloud floatToPointCloud(float[,,] zividPointCloud)
        {
            int xcnt = zividPointCloud.GetLength(0); //Stores the amount of columns from array
            int ycnt = zividPointCloud.GetLength(1); //Stores the amount of rows from array

            var newPointCloud = new List<List<Point3>>(); //Makes a 3D list

            for (int i = 0; i < xcnt; i++)
            {
                var innerPointCloud = new Point3[ycnt]; //Makes a list to later put in newPointCloud

                for (int j = 0; j < ycnt; j++)
                {
                    var x = zividPointCloud[i, j, 0]; //Stores x-component of point cloud
                    var y = zividPointCloud[i, j, 1]; //Same for y-component
                    var z = zividPointCloud[i, j, 2]; //Same for z-component
                    var p = new Point3(x,y,z);
                    //innerPointCloud.Add(p); //Stores 3D-points in a list
                    innerPointCloud[j] = p; //Stores 3D-points
                }
                newPointCloud.Add(innerPointCloud.ToList()); //Adds innerPointCloud to the "outer" list
            }                                                //by first converting array to list
            var ret = new PointCloud(); //Initializes new PointCloud object
            ret.coordinate3d = newPointCloud; //Stores newPointCloud 3D-list in ret's coordinate3d      
            return ret; 
        }
       
        /*public static float[,,] pointCloudToFloat(PointCloud zividFloat)
        {

        }*/

        /// <summary>
        /// Calculates baseline from the pictures taken, assumes equal sizes 
        /// on all point clouds and that they are rectangular (amount of rows = amount of columns)
        /// </summary>
        /// <param name="pc">List of PointCloud</param>
        /// <returns>List of PointCloud instances</returns>
        public static PointCloud calcAvg(List<PointCloud> pc)
        {

            var pointCloudAvg = new List<List<Point3>>();  
            if (pc.Count() == 0)  //Run if no pictures are detected
            {
                throw new Exception("Zero point clouds detected in calcAvg"); //Error due to no point clouds
            }
            if (pc.Count() > 1)  //Run if more than 1 picture is taken
            {
                for (int i = 0; i < pc[0].coordinate3d.Count(); i++)  //Loops through the outer list of coordinate3d
                {
                    var innerCloudAvg = new List<Point3>();
                    for (int j = 0; j < pc[0].coordinate3d[0].Count(); j++)  //Loops through the inner list of the same
                    {
                        var xList = new List<float>();  //Initializes xList, yList and zList
                        var yList = new List<float>();  //as a list of floats
                        var zList = new List<float>();
                        int a = 0;
                        Parallel.For(
                                0, pc.Count(), k =>
                                {
                                    lock (xList)
                                    {
                                        xList.Add(pc[k].coordinate3d[i][j].X); //Stores x-components in xList
                                    }
                                    lock (yList)
                                    {
                                        yList.Add(pc[k].coordinate3d[i][j].Y);  //Same for y-components
                                    }
                                    lock (zList)
                                    {
                                        zList.Add(pc[k].coordinate3d[i][j].Z);  //Same for z-components
                                    }
                                }
                        );  //Loops through all point cloud instances
                        /*for(int k = 0; k < pc.Count(); k++)
                        {
                            //lock (xList)
                            //{
                                xList.Add(pc[k].coordinate3d[i][j].X); //Stores x-components in xList
                            //}
                            //lock (yList)
                            //{
                                yList.Add(pc[k].coordinate3d[i][j].Y);  //Same for y-components
                            //}
                            //lock (zList)
                            //{
                                zList.Add(pc[k].coordinate3d[i][j].Z);  //Same for z-components
                            //}
                        }*/
                        int b = 1;
                        //{
                            
                        //}
                        xList = xList.Where(t => !float.IsNaN(t)).ToList();  //Removes all invalid points from xList
                        yList = yList.Where(t => !float.IsNaN(t)).ToList();  //Same for yList
                        zList = zList.Where(t => !float.IsNaN(t)).ToList();  //Same for zList

                        if (xList.Count() > 0 && yList.Count() > 0 && zList.Count > 0)  //Run if more than zero x, y and z
                        {                                                               //points are not NaN 
                            innerCloudAvg.Add(new Point3(xList.Average(), yList.Average(), zList.Average()));  //Make new point cloud
                        }                                                                                      //that's the average
                        else  //Return point clouds with NaN points if no valid points are "detected"
                        {
                            innerCloudAvg.Add(new Point3(float.NaN, float.NaN, float.NaN));
                        }
                    }
                    pointCloudAvg.Add(innerCloudAvg);  //Put list of 3D points in another list
                }                                      //which will be returned as a point cloud

                var returnCloud = new PointCloud(pointCloudAvg);
                /*IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("C:\\Users\\alnav\\Desktop\\Baseline.txt", FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, returnCloud);
                stream.Close();*/
                return returnCloud;  //Returns average of point clouds as an
            }                        //instance of the PointCloud class
            else
            {
                return pc.First(); //Returns one point cloud (the only one present)
            }     
        }

        public static float p2pLengthSquared(Point3 coordinate1, Point3 coordinate2)
        {
            return ((coordinate2.X - coordinate1.X) * (coordinate2.X - coordinate1.X) + (coordinate2.Y - coordinate1.Y) * (coordinate2.Y - coordinate1.Y) + (coordinate2.Z - coordinate1.Z) * (coordinate2.Z - coordinate1.Z));
        }

        public static float point2pointDistance(Point3 coordinate1, Point3 coordinate2)
        {
            return (float)Math.Sqrt(p2pLengthSquared(coordinate1, coordinate2));
        }



        /// <summary>
        /// Calculates distance between snapshot pointcloud
        /// and baseline pointcloud
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="baseline"></param>
        /// <returns></returns>
        public static float calculateDistance(List<List<Point3>> pc, List<List<Point3>> baseline)
        {
            int a = 0;
            var totDist = 0.0f;
            for (int i = 0; i < pc.Count(); i++)
            {
                float[] length = new float[pc[0].Count()];
                //float[] y = new float[pc[0].Count()];
                //float[] z = new float[pc[0].Count()];
                Parallel.For(
                        0, pc[0].Count(), j =>
                        {
                            //x[j] = (pc[i][j].X - baseline[i][j].X) * (pc[i][j].X - baseline[i][j].X);
                            //y[j] = (pc[i][j].Y - baseline[i][j].Y) * (pc[i][j].Y - baseline[i][j].Y);
                            //z[j] = (pc[i][j].Z - baseline[i][j].Z) * (pc[i][j].Z - baseline[i][j].Z);

                            length[j] = p2pLengthSquared(pc[i][j], baseline[i][j]);
                        }
                        
                );
                
                /*for (int j = 0; j < pc[0].Count(); j++)  
                {
                    x[j] = (pc[i][j].X - baseline[i][j].X) * (pc[i][j].X - baseline[i][j].X);   
                    y[j] = (pc[i][j].Y - baseline[i][j].Y) * (pc[i][j].Y - baseline[i][j].Y);
                    z[j] = (pc[i][j].Z - baseline[i][j].Z) * (pc[i][j].Z - baseline[i][j].Z);
                }    */                                             
                var nn = length.Where(t => !float.IsNaN(t));
                //var nn2 = y.Where(t => !float.IsNaN(t));
                //var nn3 = z.Where(t => !float.IsNaN(t));
                totDist += nn.Sum(); // + nn2.Sum() + nn3.Sum();
            }
            return (float)Math.Sqrt(totDist); 
            //Return square root of totDist (for now returns
        }             //a non zero number even with baseline being the same as the picture)

        /*public static float calcOutliers(List<List<Point3>> pc)
        {
            var threshold = 3.0f; //Change value for threshold depending on testresult 
        }*/

        /// <summary>
        /// Converts a pointcloud object to a 2D "heatmap" 
        /// in black and white
        /// </summary>
        /// <param name="pc"></param>
        public static void PointCloudToPicture(PointCloud pc)
        {
            var xDim = pc.getColumnSize();
            var yDim = pc.getRowSize();

            var scale = 255.0f / (pc.getMaxZ() - pc.getMinZ());
            var translationMin = (0 - pc.getMinZ());
            //var translationMax = Math.Abs(255 - pc.getMaxZ());

            var zValues = new List<float>();

            for (int i = 0; i < pc.coordinate3d.Count(); i++)
            {
                Parallel.For(
                        0, pc.coordinate3d[0].Count(), j =>
                        {
                            zValues.Add(pc.coordinate3d[i][j].Z);
                        }
                    );
            }

            try
            {
                Bitmap bmp = new Bitmap(yDim, xDim);

                for (int i = 0; i < yDim; i++)
                {

                    for (int j = 0; j < xDim; j++)
                    {

                        var matte = (int)Math.Round(pc.coordinate3d[i][j].Z * (scale) + translationMin, 0);
                        if(matte == float.NaN)
                        {
                            matte = 0;
                        }
                        if (matte > 255 || matte < 0)
                        {
                            matte = 0;
                            int bp = 0;
                        }

                        Color c = new Color();
                        c = Color.FromArgb(255, matte, matte, matte);
                        bmp.SetPixel(i, j, c);
                    }

                } // end for
                bmp.Save("minfil3.png", ImageFormat.Png);
            }
            catch(Exception ex)
            {
                int a = 1;
            }
              




        }

    }
}
