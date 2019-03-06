using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VA;

namespace perSONA
{
    public partial class Form1 : Form
    {
        VANet vA;
        Process p;
        ProcessStartInfo info;
        StreamWriter sw;
        int sourceId;
        string signalSourceId;
        int sourceId2;
        string signalSourceId2;

        public Form1()
        {
            InitializeComponent();
            vA = new VANet();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(1000);
            bool success = vA.Connect();
            Console.WriteLine(success.ToString());
            if (success)
            {
                buttonConnect.BackColor = Color.Green;
            }
            else
            {
                buttonConnect.BackColor = Color.Red;
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            buttonConnect.BackColor = Color.Gray;
            vA.Disconnect();
            //sw = p.StandardInput;
            sw.WriteLine("q");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            vA.Disconnect();

            sw = p.StandardInput;
            sw.WriteLine("q");
            
        }

        private void openServer_Click(object sender, EventArgs e)
        {
            p = new Process();
            info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            //info.RedirectStandardOutput = true;
            p.StartInfo = info;
            p.Start();

            sw = p.StandardInput;
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("cd ../../..");
                    sw.WriteLine("run_VAServer.bat");
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            vA.Reset();
        }

        private void createSource_Click(object sender, EventArgs e)
        {
            signalSourceId = vA.CreateSignalSourceBufferFromFile("../../../UFSC_Demos/audio/1.wav");
            sourceId = vA.CreateSoundSource("Numbers");
            vA.SetSoundSourcePosition(sourceId, new VAVec3(1, 1.7, 0));
            vA.SetSoundSourceSignalSource(sourceId, signalSourceId);
        }

        private void createReceiver_Click(object sender, EventArgs e)
        {
            int receiverId = vA.CreateSoundReceiver("source 1");
            VAVec3 receiverPosition = new VAVec3(0, 1.7, 0);
            vA.SetSoundReceiverPosition(receiverId, receiverPosition);
            vA.SetSoundReceiverOrientationVU(receiverId, new VAVec3(0, 0, -1), new VAVec3(0, 1, 0));
            //int hrirId = vA.CreateDirectivityFromFile("/conf/../data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            //int hrirId = vA.CreateDirectivityFromFile("../../../win32-x64.vc12/data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            int hrirId = vA.CreateDirectivityFromFile("$(DefaultHRIR)");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);
        }

        private void play_Click(object sender, EventArgs e)
        {
            vA.SetSignalSourceBufferPlaybackAction(signalSourceId, "play");
        }

        private void createSource2_Click(object sender, EventArgs e)
        {
            signalSourceId2 = vA.CreateSignalSourceBufferFromFile("../../../UFSC_Demos/audio/2.wav");
            sourceId2 = vA.CreateSoundSource("Numbers");
            vA.SetSoundSourcePosition(sourceId2, new VAVec3(0, 1.7, 1));
            vA.SetSoundSourceSignalSource(sourceId2, signalSourceId2);
        }

        private void play2_Click(object sender, EventArgs e)
        {
            vA.SetSignalSourceBufferPlaybackAction(signalSourceId2, "play");
        }
    }
}
