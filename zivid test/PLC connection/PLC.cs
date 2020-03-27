using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zivid_test
{
    public class PLC
    {
        public FileTransfer fileTransferer = new FileTransfer();
        public bool connectToPLC = true;
        public bool runOnce = true;
        public bool isConnected = true;
        public char str1 = '0';
        public Baseline blCylinderIn = new Baseline();
        public Baseline blCylinderOut = new Baseline();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public static float distance;
        public string fileName = "Threshold movement 1.csv";
        //public CameraFunctions cameraFunctions = new CameraFunctions();

        public async void RunServerAsync()  //Asyncronous task is started
        {
            if (runOnce)  //If true makes it so this code runs just once
            {
                runOnce = false;
                Int32 port = 2000; //Representing port as a 32bit number range -2,147,483,648 to 2,147,483,647
                IPAddress localAddr = IPAddress.Parse("128.39.141.190");
                var listner = new TcpListener(localAddr, port);
                listner.Start();
                Program.f.WriteTextSafe("Waiting for connection...");
                try
                {
                    while (connectToPLC) // while (J): connected to connect/disconnect buttons
                    {
                        await Accept(await listner.AcceptTcpClientAsync());  //Accepts a pending connection request
                        isConnected = false;                                           //as an asyncronous operation
                    }
                }
                finally // sets the variables back to original before closing listner-task
                {
                    listner.Stop();
                    runOnce = true;
                    isConnected = true;
                }
            }
        }

        const int packet_length = 32;  //User defined packet length

        async Task Accept(TcpClient client)
        {
            if (isConnected)  //If true only writes "connected" once
            {
                Program.f.WriteTextSafe("Connected");
            }
            await Task.Yield();  //Creates an awaitable task that asynchronously yields back to the current context when awaited.
            try
            {
                using (client)
                using (NetworkStream n = client.GetStream())
                {
                    byte[] data = new byte[packet_length];
                    int bytesRead = 0;
                    int chunkSize = 1;

                    while (bytesRead < data.Length && chunkSize > 0)  // så lenge data.length og chunksize er imellom 0 og bytesRead
                        bytesRead += chunkSize =
                            await n.ReadAsync(data, bytesRead, data.Length - bytesRead);

                    CameraFunctions functions = new CameraFunctions();
                    var dist = functions.snapshotDistance();  //A picture will be taken when something is recieved from the PLC
                    
                    // get data
                    string str = Encoding.Default.GetString(data);
                    Program.f.WriteTextSafe("[server] received :" + str[2]);
                    char str1 = str[2];
                    if (str1 == '1')    //This could be where we logg which baseline is currently running
                    {
                        zivid_test.Program.f.WriteTextSafe("1. Start position without delay");
                       
                    }
                    else if (str1 == '2')
                    {
                        zivid_test.Program.f.WriteTextSafe("2. End position without delay");
                       
                    }
                    else if (str1 == '3')
                    {
                        zivid_test.Program.f.WriteTextSafe("3. Start position with delay #1");
                    }
                    else if (str1 == '4')
                    {
                        zivid_test.Program.f.WriteTextSafe("4. End position with delay #1");
                    }
                    else if (str1 == '5')
                    {
                        zivid_test.Program.f.WriteTextSafe("4. Start position with delay #2");
                    }
                    else if (str1 == '6')
                    {
                        zivid_test.Program.f.WriteTextSafe("4. End position with delay #2");
                    }

                    if (/*CameraFunctions.distance*/ dist > 30000)  //If snapshot deviates from baseline, 
                    {                                               //then send a stop signal to PLC
                        string send_str = "Feil";
                        byte[] send_data = Encoding.ASCII.GetBytes(send_str);
                        await n.WriteAsync(send_data, 0, send_data.Length);
                        Program.f.WriteTextSafe("Errornumber: " + dist);
                        Program.f.WriteTextSafe("Picture deviates too much from Baseline");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.f.WriteTextSafe(ex.Message);
            }
        }
    }
}