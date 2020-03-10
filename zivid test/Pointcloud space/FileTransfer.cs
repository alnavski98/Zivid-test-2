using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace zivid_test
{
    public class FileTransfer
    {
        public PointCloud coordinates;

        /// <summary>
        /// Writes pointcloud object to txt file by converting to string
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="fileName"></param>
        public void writeToFile(PointCloud pc, string fileName)
        {
            // create fullDataPath file, if not exists
            try
            {
                var fullDataPath = Path.Combine("C:C:\\Users\\alnav\\Desktop", fileName);
                if (!File.Exists(fullDataPath))  //If fullDataPath doesn't exist write pointcloud to file 
                {
                   
                    string json = JsonConvert.SerializeObject(pc, Formatting.Indented);  //Serialize pointcloud to file
                    using (StreamWriter sw = File.CreateText(fullDataPath))  
                    {
                        sw.WriteLine(json);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Writes pointcloud object to txt file by converting to string
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="fileName"></param>
        public void writeToFile(Baseline pc, string fileName)
        {
            // create fullDataPath file, if not exists
            try
            {
                var fullDataPath = Path.Combine("C:C:\\Users\\alnav\\Desktop", fileName);
                if (!File.Exists(fullDataPath))  //If fullDataPath doesn't exist write pointcloud to file 
                {

                    string json = JsonConvert.SerializeObject(pc, Formatting.Indented);  //Serialize pointcloud to file
                    using (StreamWriter sw = File.CreateText(fullDataPath))
                    {
                        sw.WriteLine(json);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Reads string from txt file and "translate" it to a pointcloud object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public PointCloud readFromFile(string fileName)
        {
                var sett = new PointCloud();
                try
                {
                    String line = "";
                string settpath = Path.Combine("C:\\Users\\alnav\\Desktop", fileName);
                using (StreamReader sr = new StreamReader(settpath))
                    {
                        // Read the stream to a string, and write the string to the console.
                        line = sr.ReadToEnd();
                    }
                    sett = JsonConvert.DeserializeObject<PointCloud>(line);
                }
                catch(Exception ex) 
            {

            }
            return sett;
        }

        /*public static void writeCSV(string fileName, float body)
        {
            string h = "Error; ";
            // check if file exists
            try
            {
                var dataPath = Path.Combine("C:\\Users\\alnav\\source\\repos\\ZividPLCconnection\\zivid test\\bin\\debug", fileName);
                if (!File.Exists(dataPath))
                {
                    using (StreamWriter sw = File.CreateText(dataPath))
                    {
                        sw.WriteLine(
                                h
                        );
                    }
                }
                // append to end of file
               // string time = DateTime.Now.ToString("dd-MM-yy") + ";" + DateTime.Now.ToShortTimeString() + ";";
                using (StreamWriter sw = File.AppendText(dataPath))
                {
                    sw.WriteLine(body);
                    sw.Close();
                }
            }
            catch
            {
                throw new Exception("CSV-write error (file open?).");
            }
        }*/

    }
}
