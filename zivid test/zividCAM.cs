using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duration = Zivid.NET.Duration;

namespace zivid_test
{

    public class ZividCAM
    {
        private static Zivid.NET.Camera CAM;
        private static Zivid.NET.Application zivid = new Zivid.NET.Application();

        /// <summary>
        /// Sets exposure time for camera
        /// </summary>
        /// <param name="exposure">exosure in microseconds</param>
        public static bool setExposure(int exposure) 
        {
            bool setExposureSuccessfull = false;
            try
            {
                CAM.UpdateSettings(s =>
                {
                    s.ExposureTime = Duration.FromMicroseconds(exposure);

                });
                setExposureSuccessfull = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Must connect to camera before applying settings");
                Environment.ExitCode = 1;
            }
            return setExposureSuccessfull;
        }

        /// <summary>
        /// Sets iris for camera
        /// </summary>
        /// <param name="iris">e</param>
        public static bool setIris(ulong iris)
        {
            bool setIrisSuccessfull = false;
            try
            {
                CAM.UpdateSettings(s =>
                {
                    s.Iris = iris;

                });
                setIrisSuccessfull = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Must connect to camera before applying settings");
                Environment.ExitCode = 1;
            }
            return setIrisSuccessfull;
        }

        /// <summary>
        /// Connects to Zivid camera and boots it up.
        /// Uses a default value for exposure time and iris
        /// </summary>
        public static bool connect()
        {
            bool connectSuccessfull;
            try
            {
                Console.WriteLine("Connecting to camera");
                CAM = zivid.ConnectCamera();

                Console.WriteLine("Adjusting the iris");
                CAM.UpdateSettings(s =>
                {
                    s.Iris = 22;
                    s.ExposureTime = Duration.FromMicroseconds(8333);
                    s.Filters.Outlier.Enabled = true;
                    s.Filters.Outlier.Threshold = 5;
                });
                connectSuccessfull = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Environment.ExitCode = 1;
                connectSuccessfull = false;
            }
            return connectSuccessfull;
        }

        /// <summary>
        /// Disconnects Zivid camera
        /// </summary>
        public static void dispose()
        {
            try
            {
                CAM.Disconnect();
                CAM.Dispose();
                zivid.Dispose();
            }
            catch (System.AccessViolationException)
            {
                System.Environment.Exit(99);
            }
        }
        /// <summary>
        /// Takes pictures using manually adjusted settings
        /// </summary>
        public static float[,,] snapshot()
        {
            //bool snapshotSuccessfull = false;

            try 
            {
                //var resultFile = "resultSnapshot.zdf";
                Console.WriteLine("Capture a frame");
                var frame = CAM.Capture();
                var capturePointCloud = frame.GetPointCloud();
                var captureArray = capturePointCloud.ToArray();
                //frame.Save(resultFile);
                //snapshotSuccessfull = true;
                return captureArray;
            }
            catch
            {
                Console.WriteLine("Must connect to camera before taking snapshot");
                Environment.ExitCode = 1;
                return new float[0, 0, 0];
            }
        }

        /// <summary>
        /// Takes pictures using automatically adjusted settings
        /// </summary>
        public static bool assistMode()
        {
            bool assistModeSuccessfull = false;
            try
            {
               
                var suggestSettingsParameters = new Zivid.NET.CaptureAssistant.SuggestSettingsParameters(Duration.FromMilliseconds(1200), Zivid.NET.CaptureAssistant.AmbientLightFrequency.none);
               // Console.WriteLine("Running Capture Assistant with parameters: {0}", suggestSettingsParameters);
                var settingsList = Zivid.NET.CaptureAssistant.SuggestSettings(CAM, suggestSettingsParameters);

               /* Console.WriteLine("Suggested settings are:");
                foreach (var settings in settingsList)
                {
                    Console.WriteLine(settings);
                }
                */
                //Console.WriteLine("Capture (and merge) frames using automatically suggested settings");
                var hdrFrame = Zivid.NET.HDR.Capture(CAM, settingsList);

                string resultFile = "assistResult.zdf";
                //Console.WriteLine("Saving frame to file: " + resultFile);
                hdrFrame.Save(resultFile);
                assistModeSuccessfull = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Environment.ExitCode = 1;
            }
            return assistModeSuccessfull;
        }

        public static void picture()
        {
            try
            {
                Console.WriteLine("Setting up visualization");
                var visualizer = new Zivid.NET.CloudVisualizer();
                zivid.DefaultComputeDevice = visualizer.ComputeDevice;

                var Filename = "result4.zdf";
                Console.WriteLine("Reading " + Filename + " point cloud");
                var frame = new Zivid.NET.Frame("../" + Filename);

                Console.WriteLine("Displaying the frame");
                visualizer.Show(frame);
                visualizer.ShowMaximized();
                visualizer.ResetToFit();

                Console.WriteLine("Running the visualizer. Blocking until the window closes");
                visualizer.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Environment.ExitCode = 1;
            }
        }
    }
}
