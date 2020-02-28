using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zivid_test.PLC_connection
{
    class Threshold
    {
        public void thresholdTask()
        {
            Task thr = Task.Factory.StartNew(() =>
            {
              /*  while (true)
                {
                    // if snapshot deviates from baseline, then send a stop signal to PLC
                                if (zivid_test.Form1.)
                                {

                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("feil");
                                    
                                    // Send back a response.
                                    PLC.Stream.Write(msg, 0, msg.Length);
                                    zivid_test.Program.f.WriteTextSafe("Sent: feil ");
                                }
                }*/
            });
        }
    }
}
