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
        public bool J = true;
        public bool K = true;
        public bool L = true;
        public bool M = true;
        public char str1 = '0';
        public Baseline blCylinderIn = new Baseline();
        public Baseline blCylinderOut = new Baseline();
        public List<string> blFileNames = new List<string>() { "cylinderIn.txt", "cylinderOut.txt" };
        public static float distance;
        //public CameraFunctions cameraFunctions = new CameraFunctions();
        // asyncronous task is started
        public async void RunServerAsync()
        {
            //(if K): makes it so this code run just one at the time (spamming connect PLS button)
            if (K)
            {
                K = false;
                Int32 port = 2000; //Representing port as a 32bit number range -2,147,483,648 to 2,147,483,647
                IPAddress localAddr = IPAddress.Parse("128.39.141.190");
                var listner = new TcpListener(localAddr, port);
                listner.Start();
                Program.f.WriteTextSafe("Waiting for connection...");
                try
                {
                    while (J) // while (J): connected to connect/disconnect buttons
                    {
                        //accepts a pending connection request as an asyncronous operation
                        await Accept(await listner.AcceptTcpClientAsync());
                        M = false;
                    }
                }
                finally // sets the variables back to original before closing listner-task
                {
                    listner.Stop();
                    K = true;
                    M = true;
                }
            }

        }

        const int packet_length = 32;  // user defined packet length

        async Task Accept(TcpClient client)
        {
            // (if M) only writes "connected" once
            if (M)
            {
                Program.f.WriteTextSafe("Connected");
            }
            // Creates an awaitable task that asynchronously yields back to the current context when awaited.
            await Task.Yield();
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

                    // a picture will be taken when something is recieved from the PLC
                    CameraFunctions functions = new CameraFunctions();
                    functions.snapshotDistance();
                    // get data
                    string str = Encoding.Default.GetString(data);
                    Program.f.WriteTextSafe("[server] received :" + str[2]);
                    char str1 = str[2];
                    if (str1 == '1')    //this could be where we logg whitch baseline is currently running
                    {
                        zivid_test.Program.f.WriteTextSafe("1. Start position without delay");
                        //blCylinderIn = fileTransferer.readFromFile(blFileNames[0]);
                        //distance = PointCloudHelpers.calculateDistance(functions.pc, blCylinderIn);
                    }
                    else if (str1 == '2')
                    {
                        zivid_test.Program.f.WriteTextSafe("2. End position without delay");
                        //blCylinderOut = fileTransferer.readFromFile(blFileNames[1]);
                        //distance = PointCloudHelpers.calculateDistance(functions.pc, blCylinderOut);
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

                    // To do
                    // ...

                    // if snapshot deviates from baseline, then send a stop signal to PLC
                    if (CameraFunctions.distance > 30000)
                    {
                        string send_str = "Feil";
                        byte[] send_data = Encoding.ASCII.GetBytes(send_str);
                        await n.WriteAsync(send_data, 0, send_data.Length);
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