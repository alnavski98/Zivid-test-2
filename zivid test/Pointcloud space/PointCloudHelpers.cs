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
using LinqStatistics;

namespace zivid_test
{
    /// <summary>
    /// Helping class for point cloud
    /// </summary>
    public static class PointCloudHelpers
    {
        public static float[,] pointCloudMap;

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
                    innerPointCloud[j] = p; //Stores 3D-points in array
                }
                newPointCloud.Add(innerPointCloud.ToList()); //Adds innerPointCloud to the "outer" list
            }                                                //by first converting array to list
            var ret = new PointCloud(); //Initializes new PointCloud object
            ret.coordinate3d = newPointCloud; //Stores newPointCloud 3D-list in ret's coordinate3d      
            return ret; 
        }

        /// <summary>
        /// Calculates baseline from the pictures taken, assumes equal sizes 
        /// on all point clouds and that they are rectangular (amount of rows = amount of columns)
        /// </summary>
        /// <param name="pc">List of PointCloud</param>
        /// <returns>List of PointCloud instances</returns>
        public static Baseline calcBaseline(List<PointCloud> pc)
        {
            var pointCloudAvg = new List<List<Point3>>();
            var returnBaseline = new Baseline();
            if (pc.Count() == 0)  //Run if no pictures are detected
            {
                throw new Exception("Zero point clouds detected in calcAvg"); //Error due to no point clouds
            }
            if (pc.Count() > 1)  //Run if more than 1 picture is taken
            {
                pointCloudMap = new float[pc.First().getRowSize(), pc.First().getColumnSize()];
                for (int i = 0; i < pc.First().getRowSize(); i++)  //Loops through the outer list of coordinate3d
                {
                    var innerCloudAvg = new List<Point3>();
                    for (int j = 0; j < pc.First().getColumnSize(); j++)  //Loops through the inner list of the same
                    {
                        var xList = new List<float>();  //Initializes xList, yList and zList
                        var yList = new List<float>();  //as a list of floats
                        var zList = new List<float>();
                        var pointList = new List<Point3>();  //Initializes pointList as a list of Point3 objects 
                        var distanceList = new List<float>();  //Initializes distanceList as a list of floats

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
                                    lock (pointList)
                                    {
                                        pointList.Add(pc[k].coordinate3d[i][j]);  //Stores all 3D-coordinates in pointList
                                    }
                                }
                        );  //Loops through all point cloud instances

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
                        for (int l = 0; l < pointList.Count(); l++)  //Stores all distances squared between average points in
                        {                                            //pointcloud and equivalent points from pointList
                            distanceList.Add(p2pLengthSquared(innerCloudAvg.Last(), pointList[l]));
                        }
                        distanceList = distanceList.Where(t => !float.IsNaN(t)).ToList(); //Removes NaN points from distanceList
                        if (distanceList.Count() > 1)  //If amount of points in distanceList > 1 calculate average to put in pointCloudMap
                        {
                            pointCloudMap[i, j] = distanceList.Average() + 2 * distanceList.StandardDeviation();
                        }
                        else  //Else put 0.0f in pointCloudMap
                        {
                            pointCloudMap[i, j] = 0.0f;
                        }
                    }
                    pointCloudAvg.Add(innerCloudAvg);  //Put list of 3D points in another list
                }                                      //which will be returned as a point cloud
                var returnCloud = new PointCloud(pointCloudAvg);
                
                returnBaseline.pc = returnCloud;  
                returnBaseline.thresholdMap = pointCloudMap;
            }                        
            else
            {
                returnBaseline.pc = pc.First();
            }
            return returnBaseline;  //Returns average of point clouds as an
        }                           //instance of the PointCloud class

        public static float[,] thresholdMapPLC(Baseline baseline)
        {
            var plcThresholdMap = baseline.thresholdMap;
            return plcThresholdMap;
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
        /// <returns>float value of total distance</returns>
        public static float calculateDistance(PointCloud pc, Baseline baseline)
        {
            List<Point3> point3 = new List<Point3>();
            int badPoints = 0;
            for (int i = 0; i < pc.coordinate3d.Count(); i++)
            {
                float[] length = new float[pc.coordinate3d[0].Count()];
                int[] temp = new int[pc.coordinate3d[0].Count()];
                Parallel.For(
                        0, pc.coordinate3d[0].Count(), j =>
                        {
                            length[j] = p2pLengthSquared(pc.coordinate3d[i][j], baseline.pc.coordinate3d[i][j]);
                            if(length[j] > baseline.thresholdMap[i, j])
                            {
                                temp[j] = 1;
                            }
                            pc.coordinate3d[i][j].errorDistanceSq = length[j];
                        }      
                );
                badPoints += temp.Sum();
            }                        //taken with equivalent points in baseline "image"
            return badPoints; //Return square root of totDist (for now returns
        }                 //a non zero number even with baseline being the same as the picture)

        /*public static float calculateDistance(PointCloud pc, Baseline baseline)
        {
            var totDist = 0.0f;
            List<Point3> point3 = new List<Point3>();
            for (int i = 0; i < pc.coordinate3d.Count(); i++)
            {
                float[] length = new float[pc.coordinate3d[0].Count()];
                Parallel.For(
                        0, pc.coordinate3d[0].Count(), j =>
                        {
                            length[j] = p2pLengthSquared(pc.coordinate3d[i][j], baseline.pc.coordinate3d[i][j]);
                            pc.coordinate3d[i][j].errorDistanceSq = length[j];
                        }
                );
                var nn = length.Where(t => !float.IsNaN(t));  //Removes NaN points from nn
                totDist += nn.Sum(); //Sums up distances between points in "image"
            }                        //taken with equivalent points in baseline "image"
            return (float)Math.Sqrt(totDist); //Return square root of totDist (for now returns
        }                 //a non zero number even with baseline being the same as the picture)
        */
        /// <summary>
        /// Converts a pointcloud object to a 2D "heatmap" 
        /// in black and white
        /// </summary>
        /// <param name="pc"></param>
        public static Bitmap PointCloudToPicture(PointCloud pc, string filename)
        {
            var colDim = pc.getColumnSize();
            var rowDim = pc.getRowSize();
            Bitmap bmp = new Bitmap(colDim, rowDim);

            var zValues = new List<float>();

            for (int i = 0; i < pc.coordinate3d.Count(); i++)
            {
                Parallel.For(
                        0, pc.coordinate3d[0].Count(), j =>
                        {
                            lock (zValues)
                            {
                                zValues.Add(pc.coordinate3d[i][j].Z);  //Makes list of z-values
                            }
                        }
                    );
            }
            zValues = zValues.Where(t => !float.IsNaN(t)).ToList();
            var q2 = zValues.Median();
            var q3 = zValues.Where(t => t > q2).Median() * 1.5f;  //Interquartile Range
            var q1 = zValues.Where(t => t < q2).Median() * 1.5f;

            var scale = 255.0f / (q3 - q1);  //Scales upper and lower z-value to RGB color scale
            var translation = (0 - q1);  //Adjusts so that the lowest z-value is the lowest RGB-value
                                         //and the highest z-value to be the highest RGB-value
            //zValues = zValues.Where(t => !float.IsNaN(t)).ToList();
            
            try
            {
                for (int i = 0; i < rowDim; i++)
                {
                    for (int j = 0; j < colDim; j++)
                    {
                        var p = pc.coordinate3d[i][j];
                        var rgbMap = (int)Math.Round(p.Z * (scale) + translation, 0);
                        if(rgbMap == float.NaN)  
                        {
                            rgbMap = 0;
                        }
                        else if (rgbMap > 255)
                        {
                            rgbMap = 255;
                        }
                        else if (rgbMap < 0)
                        {
                            rgbMap = 0;
                        }
                        Color c = new Color();  //Makes color object
                        if (p.errorDistanceSq > pointCloudMap[i, j])  //For every point in the single snapshot pointcloud where distance from the
                        {                                             //single snapshot and baseline is greater than the "natural variation" in baseline
                            c = Color.FromArgb(255, 255, 0, 0);  //Color red
                        }
                        else  
                        {
                            c = Color.FromArgb(255, rgbMap, rgbMap, rgbMap);  //Color in scale of black and white
                        }
                        bmp.SetPixel(j, i, c);  //Color each pixel with scale of black and white,
                    }                           //or highlight errors with red
                } // end for
                bmp.Save(Path.Combine("C:\\Users\\alnav", filename) + ".png", ImageFormat.Png);
            }
            catch(Exception ex)
            {
            }
            return bmp;
        }

        /// <summary>
        /// Create bitmap file from PLC photo
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Bitmap plcPointCloudToPicture(PointCloud pc, float[,] plcPointCloudMap, string filename)
        {
            var colDim = pc.getColumnSize();
            var rowDim = pc.getRowSize();
            Bitmap bmp = new Bitmap(colDim, rowDim);

            var zValues = new List<float>();

            for (int i = 0; i < pc.coordinate3d.Count(); i++)
            {
                Parallel.For(
                        0, pc.coordinate3d[0].Count(), j =>
                        {
                            lock (zValues)
                            {
                                zValues.Add(pc.coordinate3d[i][j].Z);  //Makes list of z-values
                            }
                        }
                    );
            }

            var q2 = zValues.Median();
            var q3 = zValues.Where(t => t > q2).Median() * 1.5f;  //Interquartile Range
            var q1 = zValues.Where(t => t < q2).Median() * 1.5f;

            var scale = 255.0f / (q3 - q1);  //Scales upper and lower z-value to RGB color scale
            var translation = (0 - q1);  //Adjusts so that the lowest z-value is the lowest RGB-value
                                         //and the highest z-value to be the highest RGB-value
            zValues = zValues.Where(t => !float.IsNaN(t)).ToList();

            try
            {
                for (int i = 0; i < rowDim; i++)
                {
                    for (int j = 0; j < colDim; j++)
                    {
                        var p = pc.coordinate3d[i][j];
                        var rgbMap = (int)Math.Round(p.Z * (scale) + translation, 0);
                        if (rgbMap == float.NaN)
                        {
                            rgbMap = 0;
                        }
                        else if (rgbMap > 255)
                        {
                            rgbMap = 255;
                        }
                        else if (rgbMap < 0)
                        {
                            rgbMap = 0;
                        }
                        Color c = new Color();  //Makes color object
                        if (p.errorDistanceSq > plcPointCloudMap[i, j])  //For every point in the single snapshot pointcloud where distance from the
                        {                                             //single snapshot and baseline is greater than the "natural variation" in baseline
                            c = Color.FromArgb(255, 255, 0, 0);  //Color red

                        }
                        else
                        {
                            c = Color.FromArgb(255, rgbMap, rgbMap, rgbMap);  //Color in scale of black and white
                        }
                        bmp.SetPixel(j, i, c);  //Color each pixel with scale of black and white,
                    }                           //or highlight errors with red
                } // end for
                bmp.Save(Path.Combine("C:\\Users\\Trym", filename) + ".png", ImageFormat.Png);
            }
            catch (Exception ex)
            {
                int a = 1;
            }
            return bmp;
        }

        public static Point3 calcCoG(List<Point3> points)
        {
            return new Point3(points.Select(t => t.X).Average(), points.Select(t => t.Y).Average(), points.Select(t => t.Z).Average());
        }

        public static List<Point3> orderToChaos(List<List<Point3>> points)
        {
            var noNaNPoints = new List<Point3>();
            Parallel.For(
                   0, points.Count(), i => {
                       var nn = points[i].Where(t => point3IsNaN(t));  // If point is not NaN, add to unordered list
                       lock (noNaNPoints)
                       {
                           noNaNPoints.AddRange(nn);
                       }
                   }
            );
            return noNaNPoints;
        }

        public static bool point3IsNaN(Point3 v)
        {
            return (float.IsNaN(v.X) || float.IsNaN(v.Y) || float.IsNaN(v.Z));
        }
    }
}