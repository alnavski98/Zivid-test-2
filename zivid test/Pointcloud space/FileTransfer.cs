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
        /// Writes baseline object to txt file by converting to string
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="fileName"></param>
        public void writeToFile(Baseline pc, string fileName)
        {
            try  //Create fullDataPath file, if not exists
            {
                var fullDataPath = Path.Combine("C:\\Users\\Joel PersonalCompuer", fileName);
                if (!File.Exists(fullDataPath))  //If fullDataPath doesn't exist 
                {                                //write pointcloud to file

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
        /// Reads string from txt file and "translate" it to a baseline object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Baseline readFromFile(string fileName)
        {
            var set = new Baseline();
            try
            {
                String line = "";
                string setPath = Path.Combine("C:\\Users\\Joel PersonalCompuer", fileName);
                using (StreamReader sr = new StreamReader(setPath))
                {
                    line = sr.ReadToEnd();  // Read the stream to a string, and write the string to the console.
                }
                set = JsonConvert.DeserializeObject<Baseline>(line);
            }
            catch(Exception ex) 
            {
            }
            return set;
        }

        /// <summary>
        /// Writes the body to a specified filename 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="body"></param>
        public static void writeCSV(string fileName, float body)
        {
            string h = "Error; ";  // check if file exists
            try
            {
                var dataPath = Path.Combine("C:\\Users\\Joel PersonalCompuer", fileName);
                if (!File.Exists(dataPath))
                {
                    using (StreamWriter sw = File.CreateText(dataPath))
                    {
                        sw.WriteLine(
                                h
                        );
                    }
                }
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
        }
    }
}