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
        string speechFolder;
        int selectedFolder = 0;

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
                concatText("Got a connection to VA");
            }
            else
            {
                buttonConnect.BackColor = Color.Red;
                concatText("Couldn't get a connection to VA");
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
            concatText("Started Server");
        }

        private void reset_Click(object sender, EventArgs e)
        {
            textBox.Text = "Reset scene";

            vA.Reset();
        }

        private void createSource_Click(object sender, EventArgs e)
        {


            double xSides = 1;
            double zFront = 0;
            double yHeight = 1.7;



            signalSourceId = vA.CreateSignalSourceBufferFromFile("data/1.wav");
            sourceId = vA.CreateSoundSource("Numbers");
            vA.SetSoundSourcePosition(sourceId, new VAVec3(xSides, yHeight, zFront));
            vA.SetSoundSourceSignalSource(sourceId, signalSourceId);
            concatText(String.Format("\r\nCreated Source: {3} at position: {0},{1},{2}", 
                                    xSides, zFront, yHeight, sourceId));
        }

        private void createReceiver_Click(object sender, EventArgs e)
        {
            int receiverId = vA.CreateSoundReceiver("Subject");

            double xSides = 0;
            double zFront = 0;
            double yHeight = 1.7;
   

            VAVec3 receiverPosition = new VAVec3(xSides, yHeight, zFront);
            VAVec3 receiverOrientationV = new VAVec3(0, 0, -1);
            VAVec3 receiverOrientationU = new VAVec3(0, 1, 0);

            vA.SetSoundReceiverPosition(receiverId, receiverPosition);
            vA.SetSoundReceiverOrientationVU(receiverId, receiverOrientationV, receiverOrientationU);
            concatText(String.Format("\r\nCreated Receiver: {3} at position: {0},{1},{2}, looking forward ",
                                     xSides, zFront, yHeight, receiverId));
            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);
        }

        private void concatText(String textToAppend)
        {
            textBox.Text = String.Concat(textBox.Text, "\r\n");

            textBox.Text = String.Concat(textBox.Text, textToAppend);

        }


        private void play_Click(object sender, EventArgs e)
        {
            vA.SetSignalSourceBufferPlaybackAction(signalSourceId, "play");
        }

        private void createSource2_Click(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int angle = rnd.Next(360);
            String[] filePaths;
            var fileNames = Directory.GetFiles(@"data").Select(Path.GetFileName);
            int radius = 2;


            if (selectedFolder == 0)
            {
                filePaths = Directory.GetFiles(@"data/Sounds/Speech/Trainning/Lista 1A/", "*.wav");
                fileNames = filePaths.Select(Path.GetFileName);
            }else
            {
                filePaths = Directory.GetFiles(@speechFolder, "*.wav");
                fileNames = filePaths.Select(Path.GetFileName);
            }
    
            // Create a Random object  
            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(filePaths.Length);

            String speechFile = filePaths[index];
            String noiseFile = "data/Sounds/Noise/4talker-babble_ISTS.wav";

            TagLib.File tagFile = TagLib.File.Create(speechFile);
            string title = tagFile.Tag.Title;
            TimeSpan duration = tagFile.Properties.Duration;
            concatText(String.Format("Title: {0}, duration: {1}",title, duration));
            String[] words = title.Split(null);
            listBox1.DataSource = words;
            listBox1.ClearSelected();

            speechSound = vA.CreateSignalSourceBufferFromFile(speechFile);
            sourceId2 = vA.CreateSoundSource("Speech");

            noiseSound = vA.CreateSignalSourceBufferFromFile(noiseFile);
            sourceId3 = vA.CreateSoundSource("Noise");

            vA.SetSoundSourcePosition(sourceId2, new VAVec3(radius*Math.Cos(angle), 1.7, radius * Math.Sin(angle)));

            vA.SetSoundSourcePosition(sourceId3, new VAVec3(0, 1.7, radius));
            
            
            concatText(String.Format("\r\nCreated Source Signals: {0} with file: {1}, {2} with file {3}", 
                                     sourceId2, Path.GetFileName(speechFile), 
                                     sourceId3, Path.GetFileName(noiseFile)));
        }

        private void play2_Click(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int angle = rnd.Next(360);
            int radius = 2;
            double xSides = radius * Math.Sin(angle);
            double zFront = radius * Math.Cos(angle);
            double yHeight = 1.7;


            vA.SetSoundSourcePosition(sourceId2, new VAVec3(xSides, yHeight,zFront));

            double normalizationFactor = trackBar2.Value / 100.0;
            double powerSpeech = 0.25 * normalizationFactor;
            double linRatio = Math.Pow(10.0, (trackBar1.Value / 20.0));
            double powerNoise = powerSpeech/linRatio;

            concatText(String.Format("\r\nCreated Source: {3} at position: {0},{1},{2}, looking forward", xSides, zFront, yHeight, sourceId2));

            concatText(String.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1} - Volume: {4} %", powerSpeech, powerNoise, linRatio, 20*Math.Log10(linRatio),normalizationFactor*100.0));

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

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = String.Format("Volume: {0} %", trackBar2.Value);
        }

        private void getFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Directory.GetCurrentDirectory();
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    selectedFolder = 1;
                    speechFolder = fbd.SelectedPath;
                    concatText("Files found: " + files.Length.ToString());
                }

                String[] filePaths = Directory.GetFiles(@speechFolder, "*.wav");
                var fileNames = filePaths.Select(Path.GetFileName);

                textBox1.Text = String.Join("\r\n", fileNames);

            }
        }

        private void speechLeft_Click(object sender, EventArgs e)
        {
            int angle = -90;
            int radius = 2;
            double xSides = radius * Math.Sin(angle);
            double zFront = radius * Math.Cos(angle);
            double yHeight = 1.7;


            vA.SetSoundSourcePosition(sourceId2, new VAVec3(xSides, yHeight, zFront));

            double normalizationFactor = trackBar2.Value / 100.0;
            double powerSpeech = 0.25 * normalizationFactor;
            double linRatio = Math.Pow(10.0, (trackBar1.Value / 20.0));
            double powerNoise = powerSpeech / linRatio;

            concatText(String.Format("\r\nCreated Source: {3} at position: {0},{1},{2}, looking forward",
                       xSides, zFront, yHeight, sourceId2));

            concatText(String.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1} - Volume: {4} %", 
                       powerSpeech, powerNoise, linRatio, 20 * Math.Log10(linRatio), normalizationFactor * 100.0));

            vA.SetSoundSourceSoundPower(sourceId2, powerSpeech);
            vA.SetSoundSourceSignalSource(sourceId2, speechSound);

            vA.SetSoundSourceSoundPower(sourceId3, powerNoise);
            vA.SetSoundSourceSignalSource(sourceId3, noiseSound);

            vA.SetSignalSourceBufferPlaybackAction(speechSound, "play");
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "play");
            Thread.Sleep(3000);
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "stop");
        }

        private void speechRight_Click(object sender, EventArgs e)
        {
            int angle = 90;
            int radius = 2;
            double xSides = radius * Math.Sin(angle);
            double zFront = radius * Math.Cos(angle);
            double yHeight = 1.7;


            vA.SetSoundSourcePosition(sourceId2, new VAVec3(xSides, yHeight, zFront));

            double normalizationFactor = trackBar2.Value / 100.0;
            double powerSpeech = 0.25 * normalizationFactor;
            double linRatio = Math.Pow(10.0, (trackBar1.Value / 20.0));
            double powerNoise = powerSpeech / linRatio;

            concatText(String.Format("\r\nCreated Source: {3} at position: {0},{1},{2}, looking forward", xSides, zFront, yHeight, sourceId2));

            concatText(String.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1} - Volume: {4} %", powerSpeech, powerNoise, linRatio, 20 * Math.Log10(linRatio), normalizationFactor * 100.0));

            vA.SetSoundSourceSoundPower(sourceId2, powerSpeech);
            vA.SetSoundSourceSignalSource(sourceId2, speechSound);

            vA.SetSoundSourceSoundPower(sourceId3, powerNoise);
            vA.SetSoundSourceSignalSource(sourceId3, noiseSound);

            vA.SetSignalSourceBufferPlaybackAction(speechSound, "play");
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "play");
            Thread.Sleep(3000);
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "stop");
        }

        private void speechFront_Click(object sender, EventArgs e)
        {
            int angle = 0;
            int radius = 2;
            double xSides = radius * Math.Sin(angle);
            double zFront = radius * Math.Cos(angle);
            double yHeight = 1.7;


            vA.SetSoundSourcePosition(sourceId2, new VAVec3(xSides, yHeight, zFront));

            double normalizationFactor = trackBar2.Value / 100.0;
            double powerSpeech = 0.25 * normalizationFactor;
            double linRatio = Math.Pow(10.0, (trackBar1.Value / 20.0));
            double powerNoise = powerSpeech / linRatio;

            concatText(String.Format("\r\nCreated Source: {3} at position: {0},{1},{2}, looking forward", xSides, zFront, yHeight, sourceId2));

            concatText(String.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1} - Volume: {4} %", powerSpeech, powerNoise, linRatio, 20 * Math.Log10(linRatio), normalizationFactor * 100.0));

            vA.SetSoundSourceSoundPower(sourceId2, powerSpeech);
            vA.SetSoundSourceSignalSource(sourceId2, speechSound);

            vA.SetSoundSourceSoundPower(sourceId3, powerNoise);
            vA.SetSoundSourceSignalSource(sourceId3, noiseSound);

            vA.SetSignalSourceBufferPlaybackAction(speechSound, "play");
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "play");
            Thread.Sleep(3000);
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "stop");

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double answer = listBox1.SelectedItems.Count;
            double totalWords =  listBox1.Items.Count;
            textBox2.Text = String.Format("Answer {0}/{1}= {2}% ", answer, totalWords, 100.0*(answer/totalWords));
            
            //foreach (var item in listBox1.SelectedItems)
            //{
            //    textBox2.Text += "," + item.ToString();
            //  textBox2.Text = textBox2.Text.Substring(1, textBox2.Text.Length - 1);
            //}
        }
    }
}
