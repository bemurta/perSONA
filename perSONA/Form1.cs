﻿using System;
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
        private readonly Process process;
        ProcessStartInfo info;
        StreamWriter sw;
        StreamReader sr;
        int sourceId;
        string signalSourceId;
        int sourceId2;
        string speechSound;
        string noiseSound;
        int sourceId3;

        public Form1()
        {
            InitializeComponent();
            vA = new VANet();
            this.process = new Process
            {
                StartInfo = VAServerProcessInfo()
            };
        }

        ~Form1()
        {
            this.Form1_FormClosing(null, null);
        }

        private ProcessStartInfo VAServerProcessInfo()
        {
            info = new ProcessStartInfo();
            info.FileName = "bin/VAServer.exe";
            info.RedirectStandardInput = true;
            //info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.Arguments = "localhost:12340 conf/VACore.ini";
            return info;
        }

        private async Task<bool> UpdateLog()
        {
            try
            {
                textBox.Text += await sr.ReadToEndAsync();
                Thread.Sleep(1000);
                await UpdateLog();
                return true;
            }
            catch (Exception e)
            {
                sw.Write(e.Message);
                return false;
            }
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
            try
            {
                vA.Disconnect();
                this.sw?.WriteLine("q");
                this.sr?.Close();
                this.sw?.Close();
                this.vA.Disconnect();
                if (!this.process.CloseMainWindow())
                    this.process.Kill();
            }
            catch
            {
                this.process.Kill();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                vA.Disconnect();
                this.sw?.WriteLine("q");
                this.sr?.Close();
                this.sw?.Close();
                this.vA.Disconnect();
                if (!this.process.CloseMainWindow())
                    this.process.Kill();
            }
            catch
            {
                this.process.Kill();
            }
        }

        private void openServer_Click(object sender, EventArgs e)
        {
            this.process.Start();
            buttonConnect.Enabled = true;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            vA.Reset();
        }

        private void createSource_Click(object sender, EventArgs e)
        {
            signalSourceId = vA.CreateSignalSourceBufferFromFile("data/1.wav");
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
            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);
        }

        private void play_Click(object sender, EventArgs e)
        {
            vA.SetSignalSourceBufferPlaybackAction(signalSourceId, "play");
        }

        private void createSource2_Click(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int angle = rnd.Next(360);

            int radius = 2;

            speechSound = vA.CreateSignalSourceBufferFromFile("data/Sounds/Speech/Trainning/Lista 1A/Lista 1A_frase1.wav");
            sourceId2 = vA.CreateSoundSource("Speech");

            noiseSound = vA.CreateSignalSourceBufferFromFile("data/Sounds/Noise/4talker-babble_ISTS.wav");
            sourceId3 = vA.CreateSoundSource("Noise");

            vA.SetSoundSourcePosition(sourceId2, new VAVec3(radius*Math.Cos(angle), 1.7, radius * Math.Sin(angle)));
            
            

            vA.SetSoundSourcePosition(sourceId3, new VAVec3(0, 1.7, radius));
            
            
        }

        private void play2_Click(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int angle = rnd.Next(360);
            int radius = 2;
            vA.SetSoundSourcePosition(sourceId2, new VAVec3(radius * Math.Cos(angle), 1.7, radius * Math.Sin(angle)));

            double powerSpeech = 0.25;
            double linRatio = Math.Pow(10.0, (trackBar1.Value / 20.0));
            double powerNoise = powerSpeech/linRatio;

            textBox.Text = String.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1}", powerSpeech, powerNoise, linRatio, 20*Math.Log10(linRatio));

            vA.SetSoundSourceSoundPower(sourceId2, powerSpeech);
            vA.SetSoundSourceSignalSource(sourceId2, speechSound);

            vA.SetSoundSourceSoundPower(sourceId3, powerNoise);
            vA.SetSoundSourceSignalSource(sourceId3, noiseSound);

            vA.SetSignalSourceBufferPlaybackAction(speechSound, "play");
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "play");
            Thread.Sleep(3000);
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "stop");

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            label1.Text = String.Format("SNR: {0} dB", trackBar1.Value);
        }
    }
}
