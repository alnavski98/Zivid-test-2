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

        public bool J = true;
        public bool L = true;
        public bool M = true;
        bool K = true;

        // asyncronous task is started
        public async void RunServerAsync()
        {
            //(if K): makes it so this code can run just one at the time
            if (K)
            {
                K = false;
                Int32 port = 2000; //Representing port as a 32bit number range -2,147,483,648 to 2,147,483,647
                IPAddress localAddr = IPAddress.Parse("128.39.141.190");
                var listner = new TcpListener(localAddr, port);
                listner.Start();
                Program.f.WriteTextSafe("started listening...");
                try
                {
                    while (J)
                    {
                        //accepts a pending connection request as an asyncronous operation
                        await Accept(await listner.AcceptTcpClientAsync());
                        M = false;
                    }


                }
                finally
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
            // (if M) only writes "connected" ones
            if (M)
            {
                Program.f.WriteTextSafe("connected");
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

                    while (bytesRead < data.Length && chunkSize > 0)
                        bytesRead += chunkSize =
                            await n.ReadAsync(data, bytesRead, data.Length - bytesRead);

                    // get data
                    string str = Encoding.Default.GetString(data);
                    Program.f.WriteTextSafe("[server] received :" + str[2]);
                    char str1 = str[2];
                    if (str1 == '1')    //this could be where we logg whitch baseline is currently running
                    {
                        zivid_test.Program.f.WriteTextSafe("1. Startposisjon uten delay");
                    }
                    else if (str1 == '2')
                    {
                        zivid_test.Program.f.WriteTextSafe("2. Sluttposisjon uten delay.");
                    }
                    else if (str1 == '3')
                    {
                        zivid_test.Program.f.WriteTextSafe("3. Startposisjon med delay nr1");
                    }
                    else if (str1 == '4')
                    {
                        zivid_test.Program.f.WriteTextSafe("4. Sluttposisjon med delay nr1");
                    }
                    else if (str1 == '5')
                    {
                        zivid_test.Program.f.WriteTextSafe("4. Startposisjon med delay nr2");
                    }
                    else if (str1 == '6')
                    {
                        zivid_test.Program.f.WriteTextSafe("4. Sluttposisjon med delay nr2");
                    }

                    // To do
                    // ...

                    // if snapshot deviates from baseline, then send a stop signal to PLC
                    if (CameraFunctions.distance > 30000)
                    {
                        string send_str = "feil";
                        byte[] send_data = Encoding.ASCII.GetBytes(send_str);
                        await n.WriteAsync(send_data, 0, send_data.Length);
                        Program.f.WriteTextSafe("feil");
                    }


                }
            }
            catch (Exception ex)
            {
                Program.f.WriteTextSafe(ex.Message);
            }
        }



        //____________________________________________________________________________________________________________________________________________
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public static bool j;
        public static bool cancel = false;
        public PointCloud pc = new PointCloud();
        public static CancellationTokenSource source1 = new CancellationTokenSource();
        public CancellationToken token1 = source1.Token;
        public static bool test=true;

        public void plcListner()
        {

            //makes it posible to cancel the task
            CancellationTokenSource source1 = new CancellationTokenSource();
            CancellationToken token1 = source1.Token;


            if (j)
            {


                // Starts a work in parallel       
                // Starts a work in parallel   
                Task t = Task.Factory.StartNew(() =>
                {
                    TcpListener server = null;
                    try
                    {

                        // Set the TcpListener on port 2000.
                        Int32 port = 2000; //Representing port as a 32bit number range -2,147,483,648 to 2,147,483,647
                        IPAddress localAddr = IPAddress.Parse("128.39.141.190");

                        // TcpListener server = new TcpListener(port);
                        server = new TcpListener(localAddr, port);

                        // Start listening for client requests.
                        server.Start();

                        // Buffer for reading data
                        Byte[] bytes = new Byte[256];  //Creates 256 "instances" of the Byte struct called "bytes"
                        String data = null;




                        // Enter the listening loop.
                        while (true)
                        {

                            if (L)
                            {
                                zivid_test.Program.f.WriteTextSafe("Waiting for a Connection");
                            }
                            

                            // Perform a blocking call to accept requests.
                            // You could also user server.AcceptSocket() here.
                            TcpClient client = server.AcceptTcpClient();            // her venter coden på connection 
                            zivid_test.Program.f.WriteTextSafe("Connected");

                            data = null;

                            // Get a stream object for reading and writing
                            NetworkStream stream = client.GetStream();

                            int i;
                            // Starts one work in parallel
                            if (test)
                            {
                                test = false;
                                Task threshold = Task.Factory.StartNew(() =>
                                {
                                    float j = 0;

                                    // Enter the listening loop.
                                    while (true)
                                    {

                                        // if snapshot deviates from baseline, then send a stop signal to PLC
                                        if (CameraFunctions.distance > 30000)
                                        {
                                            if (j != CameraFunctions.distance)
                                            {
                                                // the stop signal
                                                byte[] msg = System.Text.Encoding.ASCII.GetBytes("feil");

                                                // Sends the stop signal. (Stoping the automation system)
                                                stream.Write(msg, 0, msg.Length);
                                                zivid_test.Program.f.WriteTextSafe("Sent: feil ");
                                                // currently it sends a stop signal every 1 sec, but we should see if sending just one is needed
                                                j = CameraFunctions.distance;
                                            }


                                        }
                                        if (cancel)
                                        {

                                            client.Close();
                                            server.Stop();
                                            zivid_test.Program.f.WriteTextSafe("Disconected PLC ");
                                            source1.Cancel();
                                            cancel = false;
                                            test = true;
                                            Thread.Sleep(10);
                                            if (token1.IsCancellationRequested)
                                            {
                                                token1.ThrowIfCancellationRequested();
                                            }


                                        }
                                    }
                                }, token1);
                                if (cancel)
                                {
                                    try
                                    {
                                        Task.WaitAny();
                                    }
                                    catch (OperationCanceledException)
                                    {

                                    }

                                }
                            }

                            // Loop to receive all the data sent by the client.
                            try
                            {


                                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) //True while message is not 0 
                                {
                                    // a picture will be taken when something is recieved from the PLC
                                    CameraFunctions functions = new CameraFunctions();
                                    functions.snapshotDistance();

                                    // Translate data bytes from PLC to an ASCII string.
                                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                                    // writes out the deviation number called distance to logg window
                                    zivid_test.Program.f.WriteTextSafe("baseline: " + data + Environment.NewLine + CameraFunctions.distance);



                                    char Number = data[2];


                                    if (Number == '1')    //this could be where we logg whitch baseline is currently running
                                    {
                                        zivid_test.Program.f.WriteTextSafe("1. Startposisjon uten delay");
                                    }
                                    else if (Number == '2')
                                    {
                                        zivid_test.Program.f.WriteTextSafe("2. Sluttposisjon uten delay.");
                                    }
                                    else if (Number == '3')
                                    {
                                        zivid_test.Program.f.WriteTextSafe("3. Startposisjon med delay nr1");
                                    }
                                    else if (Number == '4')
                                    {
                                        zivid_test.Program.f.WriteTextSafe("4. Sluttposisjon med delay nr1");
                                    }
                                    else if (Number == '5')
                                    {
                                        zivid_test.Program.f.WriteTextSafe("4. Startposisjon med delay nr2");
                                    }
                                    else if (Number == '6')
                                    {
                                        zivid_test.Program.f.WriteTextSafe("4. Sluttposisjon med delay nr2");
                                    }
                                }
                            }
                            catch(Exception p)
                            {

                            }
                            // Shutdown and end connection
                            client.Close();
                        }




                    }
                    catch (SocketException e)
                    {

                        zivid_test.Program.f.WriteTextSafe("SocketException: " + e);
                    }
                    finally
                    {
                        // Stop listening for new clients.
                        server.Stop();
                    }


                    Console.Read();

                },token1);



                if (cancel)
                {
                    try
                    {
                           Task.WaitAny();
                    }
                    catch (OperationCanceledException)
                    {

                    }

                }
            }

        }
    }
}