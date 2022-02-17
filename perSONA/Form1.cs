using DocumentFormat.OpenXml.Presentation;
using Newtonsoft.Json.Linq;
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
using ZedGraph;

namespace perSONA
{
    public partial class Form1 : Form, IvAInterface
    {
        VANet vA;
        private readonly Process process;
        ProcessStartInfo info;
        StreamWriter sw;
        StreamReader sr;

        List<speechPerceptionTest> completedTests = new List<speechPerceptionTest>();

        string speechFolder = "data\\Sounds\\Speech\\Alcaim1_\\F\\F0001";
        string testFolder = "data\\Sounds";
        string noiseFile = "data\\Sounds\\Noise\\4talker-babble_ISTS.wav";
        string noiseFolder = "data\\Sounds\\Noise";
        string confFile = "conf/VACore.ini";
        string speechSound;
        string noiseSound;
        int speechSource;
        int noiseSource;
        int selectedFolder = 0;
        int waitTimeMS = 3000;
        int sourceIndex;

        public Form1(string confFile, int sourceIndex)
        {
            InitializeComponent();

            resizeScreen();

            this.confFile = confFile;
            this.sourceIndex = sourceIndex;

            vA = new VANet();
            this.process = new Process
            {
                StartInfo = VAServerProcessInfo()
            };

            startServer();
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(waitTimeMS);
            connectToVA();

            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                label2.Text = string.Format("Volume: {0} %", Properties.Settings.Default.EARPHONE_VOLUME);
            }
            else
            {
                label2.Text = string.Format("Volume: {0} %", Properties.Settings.Default.SPEAKER_VOLUME);
            }

            string[] filePaths = Directory.GetFiles(@testFolder, "*.wav");
            string[] fileNames = filePaths.Select(Path.GetFileName).ToArray();
            listBox2.DataSource = fileNames;
            comboBox3.DataSource = Directory.GetFiles(@noiseFolder).Select(Path.GetFileName).ToArray();
            comboBox3.SelectedItem = comboBox3.Items.IndexOf("4talker-babble_ISTS.wav");
            plotSceneGraph(zedGraphControl1, new double[] { 2, 4 }, new double[] { 90, -90});
            textBox.Text = "Started perSONA";
            concatText("New VA Session started.");


            switch (confFile)
            {

                case "conf/VACore.ini":
                    concatText("Headphone binaural reproduction");
                    break;

                case "conf/VACore_CLINIC_CTC.ini":
                    concatText("Clinic 2-speaker array CTC reproduction");
                    break;

                case "conf/VACore_UFSC_CTC.ini":
                    concatText("LVA 8-Speaker Array N-CTC reproduction");
                    break;

                default:
                    concatText("Headphone binaural reproduction");
                    break;
            }
            updatePatientList();
            updateApplicatorList();
        }

        //??
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
            info.Arguments = string.Format("localhost:12340 {0}", confFile);
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

        public void updatePatientList()
        {
            try
            {
                string patientDir = string.Format("{0}/patients", Properties.Settings.Default.RESULTS_FOLDER);
                string[] patients = Directory.GetFiles(patientDir, "*.json");
                string[] patientNames = patients.Select(Path.GetFileNameWithoutExtension).ToArray();
                patientBox.DataSource = patientNames;
            }
            catch (DirectoryNotFoundException e)
            {
                string patientDir = string.Format("{0}/patients", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(patientDir);
                updatePatientList();
            }
        }

        private void connectToVA()
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

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            connectToVA();
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
                Console.WriteLine("Closed application");
            }
            Application.Exit();
        }

        private void openServer_Click(object sender, EventArgs e)
        {
            startServer();
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(3000);
        }

        private void startServer()
        {
            this.process.Start();
            buttonConnect.Enabled = true;
            concatText("Started Server");
        }

        private void reset_Click(object sender, EventArgs e)
        {
            concatText("Reset scene");
            cond1.Checked = false;
            cond3.Checked = false;
            cond4.Checked = false;
            vA.Reset();
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
            concatText(string.Format("\r\nCreated Receiver: {3} at position: {0},{1},{2}, looking forward ",
                                     xSides, zFront, yHeight, receiverId));
            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);

            cond3.Checked = true;
            checkValidScene();
        }

        public void concatText(string textToAppend)
        {
            string timestamp = DateTime.Now.ToString(@"dd MMMM yyyy HH:mm:ss - ");

            textBox.Text = string.Concat(textBox.Text, "\r\n", timestamp);

            textBox.Text = string.Concat(textBox.Text, textToAppend);

            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
        }

        private void createSource2_Click(object sender, EventArgs e)
        {
            string[] filePaths;
            var fileNames = Directory.GetFiles(@"data").Select(Path.GetFileName);

            filePaths = Directory.GetFiles(@speechFolder, "*.wav");
            fileNames = filePaths.Select(Path.GetFileName);

            // Create a Random object  
            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(filePaths.Length);

            string speechFile = filePaths[index];


            createAcousticScene(speechFile, noiseFile);

            fillWords(speechFile, listBox1, true);
            cond1.Checked = true;
            checkValidScene();
            concatText(string.Format("Title: {0}, duration: {1}", getTitle(speechFile), getDuration(speechFile)));
        }

        private void play2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int angle = rnd.Next(360);
            int radius = 2;

            playScene(radius, angle, trackBar1.Value);
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value < 40)
                label1.Text = string.Format("SNR: {0} dB", trackBar1.Value);
            else
                label1.Text = string.Format("SNR: INF", trackBar1.Value);
        }

        public VANet getVa()
        {
            return vA;
        }

        private void getFolder_Click(object sender, EventArgs e)
        {
            string rootFolder = getDatabaseFolder();
            this.TopMost = true;
            string[] filePaths = Directory.GetFiles(@rootFolder, "*.wav");
            string[] fileNames = filePaths.Select(Path.GetFileName).ToArray();
            listBox2.DataSource = fileNames;
        }

        public string getDatabaseFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Directory.GetCurrentDirectory();
                DialogResult result = fbd.ShowDialog();

                string folder = speechFolder;

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    selectedFolder = 1;
                    folder = fbd.SelectedPath;
                    concatText("Files found: " + files.Length.ToString());
                }          
                return folder;
            }
        }

        public void playScene(double radius, double angle, double snr)
        {

            if (!cond4.Checked)
                {
                    concatText(String.Format("Scene not ok. Signal: {0}, Noise {1}, Receiver: {2}",
                                         cond1.Checked, cond2.Checked, cond3.Checked));                    
                }

            double[] radiusList = { radius, radius };
            double[] angleList = { angle, 0 };

            plotSceneGraph(zedGraphControl1, radiusList, angleList);

            double xSides = radius * Math.Sin(angle / 180 * Math.PI);
            double zFront = radius * Math.Cos(angle / 180 * Math.PI);
            double yHeight = 1.7;

            //double normalizationFactor = trackBar2.Value / 100.0;
            double normalizationFactor;
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                normalizationFactor = Properties.Settings.Default.EARPHONE_VOLUME / 100.0;
            }
            else 
            {
                normalizationFactor = Properties.Settings.Default.SPEAKER_VOLUME / 100.0;
            }
            double powerSpeech = 0.25 * normalizationFactor;

            double linRatio = Math.Pow(10.0, (snr / 20.0));
            double powerNoise = powerSpeech / linRatio;

            if (snr == 40)
            {
                powerNoise = 0;
            }

            vA.SetSoundSourcePosition(speechSource, new VAVec3(xSides, yHeight, zFront));
            vA.SetSoundSourcePosition(noiseSource, new VAVec3(0, 1.7, radius));

            vA.SetSoundSourceSoundPower(speechSource, powerSpeech);
            vA.SetSoundSourceSignalSource(speechSource, speechSound);

            vA.SetSoundSourceSoundPower(noiseSource, powerNoise);
            vA.SetSoundSourceSignalSource(noiseSource, noiseSound);

            //concatText(string.Format("Created Source: {3} at position: {0},{1},{2}, looking forward",
            //xSides, zFront, yHeight, speechSource));
            concatText("Selected Speech: " + Path.Combine(speechFolder, listBox2.GetItemText(listBox2.SelectedItem)));
            concatText(string.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1} - Volume: {4} %",
                       powerSpeech, powerNoise, linRatio, 20 * Math.Log10(linRatio), normalizationFactor * 100.0));
            concatText("Selected Noise: " + noiseFile);
            vA.SetSignalSourceBufferPlaybackAction(speechSound, "play");
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "play");
        }

        public void playScene(double radius, bool speechON, string speechFile, double currentSpeechPower, double speechAngle, bool noiseON, string noiseFile, double noiseAngle, double currentNoisePower)
        {

            if (speechON)
            {
                double xSides = radius * Math.Sin(speechAngle / 180 * Math.PI);
                double zFront = radius * Math.Cos(speechAngle / 180 * Math.PI);
                double yHeight = 1.7;

                double powerSpeech = (currentSpeechPower / 100000.0) * 0.25;
                speechSound = vA.CreateSignalSourceBufferFromFile(speechFile);
                speechSource = vA.CreateSoundSource("Speech");

                int humanDirectivity = vA.CreateDirectivityFromFile("data/Singer.v17.ms.daff");
                vA.SetSoundSourceDirectivity(speechSource, humanDirectivity);

                vA.SetSoundSourcePosition(speechSource, new VAVec3(xSides, yHeight, zFront));
                vA.SetSoundSourceSoundPower(speechSource, powerSpeech);
                vA.SetSoundSourceSignalSource(speechSource, speechSound);
            }

            if (noiseON)
            {
                double noisexSides = radius * Math.Sin(noiseAngle / 180 * Math.PI);
                double noisezFront = radius * Math.Cos(noiseAngle / 180 * Math.PI);
                double noiseyHeight = 1.7;

                double powerNoise = (currentNoisePower / 100000.0) * 0.25;
                noiseSound = vA.CreateSignalSourceBufferFromFile(noiseFile);
                noiseSource = vA.CreateSoundSource("Noise");

                int humanDirectivity = vA.CreateDirectivityFromFile("data/Singer.v17.ms.daff");
                vA.SetSoundSourceDirectivity(speechSource, humanDirectivity);

                vA.SetSoundSourcePosition(noiseSource, new VAVec3(noisexSides, noiseyHeight, noisezFront));
                vA.SetSoundSourceSoundPower(noiseSource, powerNoise);
                vA.SetSoundSourceSignalSource(noiseSource, noiseSound);
            }

            if (speechON) vA.SetSignalSourceBufferPlaybackAction(speechSound, "play");
            
            if (noiseON)
            {
                vA.SetSignalSourceBufferLooping(noiseSound, true);
                vA.SetSignalSourceBufferPlaybackAction(noiseSound, "play");
            }
        }

        public void stopScene(bool speechON, bool noiseON)
        {
            if (speechON)
            {
                vA.SetSignalSourceBufferPlaybackAction(speechSound, "pause");
                vA.SetSignalSourceBufferPlaybackAction(speechSound, "stop");
                vA.DeleteSignalSource(speechSound);
            }
            if (noiseON)
            {
                vA.SetSignalSourceBufferPlaybackAction(noiseSound, "pause");
                vA.SetSignalSourceBufferPlaybackAction(noiseSound, "stop");
                vA.DeleteSignalSource(noiseSound);
            }
        }

            private void speechLeft_Click(object sender, EventArgs e)
        {
            int angle = -90;
            int radius = 2;
            if (cond4.Checked)
            {
                playScene(radius, angle, trackBar1.Value);
            }
            else
            {
                _ = MessageBox.Show("Cena acústica incompleta", "Checar Sinal, Ruído e Receptor.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void speechRight_Click(object sender, EventArgs e)
        {
            int angle = 90;
            int radius = 2;
            if (cond4.Checked)
            {
                playScene(radius, angle, trackBar1.Value);
            }
            else
            {
                _ = MessageBox.Show("Cena acústica incompleta", "Checar Sinal, Ruído e Receptor.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void speechFront_Click(object sender, EventArgs e)
        {
            int angle = 0;
            int radius = 2;
            if (cond4.Checked)
            {
                playScene(radius, angle, trackBar1.Value);
            }
            else
            {
                _ = MessageBox.Show("Cena acústica incompleta", "Checar Sinal, Ruído e Receptor.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double answer = listBox1.SelectedItems.Count;
            double totalWords = listBox1.Items.Count;
            textBox2.Text = string.Format("Answer {0}/{1}= {2}% ", answer, totalWords, 100.0 * (answer / totalWords));
        }


        public void fillWords(string speechFile, ListBox listbox, bool test=false)
        {
            string title = getTitle(speechFile);

            if (!string.IsNullOrEmpty(title))
            {
                string[] words = title.Split(null);
                listbox.DataSource = words;
                listbox.ClearSelected();
                //concatText(String.Format("Title: {0}, duration: {1}", getTitle(speechFile), getDuration(speechFile)));
            }
            else
            {
                if (test)
                {
                    listbox.DataSource = new string[] { "Sinal de teste ou não cadastrado" };
                }
                else
                {
                    const string MSGINFO = "Sinal de fala não contém as palavras cadastradas. Utlize a Área de edição de arquivos de áudio, acessível pelo menu superior para configurar seus arquivos.";
                    const string message = MSGINFO;
                    const string caption = "Detectado erro de configuração!";
                    var result = MessageBox.Show(message, caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    concatText(string.Format("Wrong database format detected - tag: {0}, dur: {1}", getTitle(speechFile), getDuration(speechFile)));
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string speechFile = System.IO.Path.Combine(testFolder, listBox2.GetItemText(listBox2.SelectedItem));
            concatText(speechFile);
            createAcousticScene(speechFile, noiseFile);

            fillWords(speechFile, listBox1, true);
            cond1.Checked = true;
            checkValidScene();
        }

        public string getTitle(string speechFile)
        {
            TagLib.File tagFile = TagLib.File.Create(speechFile);

            int channels = tagFile.Properties.AudioChannels;
            int bitrate = tagFile.Properties.AudioBitrate;
            int sampleRate = tagFile.Properties.AudioSampleRate;

            concatText(string.Format("channels: {0}, bitrate: {1}, sampleRate:{2}, n-bits={3}",
                channels, bitrate, sampleRate, 1000 * bitrate / (sampleRate * channels)));
            string title = tagFile.Tag.Title;

            return title;
        }
        public TimeSpan getDuration(string speechFile)
        {
            TagLib.File tagFile = TagLib.File.Create(speechFile);
            TimeSpan duration = tagFile.Properties.Duration;

            //concatText(duration.ToString());

            return duration;
        }

        public void createAcousticScene(string speechFile, string noiseFile)
        {
            speechSound = vA.CreateSignalSourceBufferFromFile(speechFile);
            speechSource = vA.CreateSoundSource("Speech");

            noiseSound = vA.CreateSignalSourceBufferFromFile(noiseFile);
            noiseSource = vA.CreateSoundSource("Noise");
            int humanDirectivity = vA.CreateDirectivityFromFile("data/Singer.v17.ms.daff");
            vA.SetSoundSourceDirectivity(speechSource, humanDirectivity);

            concatText(string.Format("\r\nCreated Source Signals: {0} with file: {1}, {2} with file {3}",
                                     speechSource, Path.GetFileName(speechFile),
                                     noiseSource, Path.GetFileName(noiseFile)));
        }

        //????
        private void button2_Click(object sender, EventArgs e)
        {
            int angleSpeech = 45;
            int radiusSpeech = 2;

            int angleNoise = 0;
            int radiusNoise = 2;

            double[] radius = { radiusSpeech, radiusNoise };
            double[] angle = { angleSpeech, angleNoise };

            plotSceneGraph(zedGraphControl1, radius, angle);
        }

        public void plotSceneGraph(ZedGraphControl graph, double[] radius, double[] angle)
        {
            GraphPane myPane = graph.GraphPane;
            myPane.CurveList.Clear();

            PointPairList speechList = new PointPairList();
            PointPairList noiseList = new PointPairList();
            PointPairList circle = new PointPairList();
            PointPairList head = new PointPairList();
            PointPairList speakers = new PointPairList();
            PointPairList nose = new PointPairList();

            double roomLength = 5;
            double roomWidth = 5;
            double radiusSpekers = 0.2;

            switch (sourceIndex)
            {
                case 0:
                    for (double i = -Math.PI / 2; i <= Math.PI / 2; i += Math.PI)
                    {
                        speakers.Add(radiusSpekers * Math.Sin(i), radiusSpekers * Math.Cos(i));
                    }
                    break;

                case 1:
                    //roomLength = 1.81;
                    //roomWidth = 1.81;
                    radiusSpekers = 1;
                    for (double i = -Math.PI / 4; i <= Math.PI / 4; i += Math.PI / 2)
                    {
                        speakers.Add(radiusSpekers * Math.Sin(i), radiusSpekers * Math.Cos(i));
                    }
                    break;

                case 2:
                    //roomLength = 2.81;
                    //roomWidth = 3.43;
                    radiusSpekers = 1.3;
                    for (double i = 0; i < 2 * Math.PI; i += Math.PI / 4)
                    {
                        speakers.Add(radiusSpekers * Math.Sin(i), radiusSpekers * Math.Cos(i));
                    }
                    break;

            }

            speechList.Add(radius[0] * Math.Sin(angle[0] / 180 * Math.PI), radius[0] * Math.Cos(angle[0] / 180 * Math.PI));
            noiseList.Add(radius[1] * Math.Sin(angle[1] / 180 * Math.PI), radius[1] * Math.Cos(angle[1] / 180 * Math.PI));


            double radiusPerson = 0.22;
            for (double i = 0; i < 2 * Math.PI; i += Math.PI / 50)
            {

                head.Add(radiusPerson * Math.Sin(i), radiusPerson * Math.Cos(i));
            }

            nose.Add(-0.1, radiusPerson);
            nose.Add(0, radiusPerson + 0.1);
            nose.Add(0.1, radiusPerson);

            LineItem noseCurve = myPane.AddCurve("",
                   nose, Color.Black, SymbolType.None);
            LineItem headCurve = myPane.AddCurve("",
                   head, Color.Black, SymbolType.None);




            LineItem speakersCurve = myPane.AddCurve("AF-S",
                   speakers, Color.Black, SymbolType.Star);
            speakersCurve.Line.IsVisible = false;

            LineItem speechCurve = myPane.AddCurve("Fala",
                   speechList, Color.Blue, SymbolType.Diamond);
            speechCurve.Line.IsVisible = false;
            speechCurve.Symbol.Size = 10;

            ArrowObj arrowX = new ArrowObj(Color.Black, 25, -roomLength * 0.45, -roomWidth * 0.45, -roomLength * 0.35, -roomWidth * 0.45);
            TextObj raTextX = new TextObj("X", -roomLength * 0.3, -roomWidth * 0.45);
            myPane.GraphObjList.Add(arrowX);
            myPane.GraphObjList.Add(raTextX);
            raTextX.FontSpec.Border.IsVisible = false;
            raTextX.FontSpec.Size = 21;

            ArrowObj arrowZ = new ArrowObj(Color.Black, 25, -roomLength * 0.45, -roomWidth * 0.45, -roomLength * 0.45, -roomWidth * 0.35);
            TextObj raTextZ = new TextObj("Z", -roomLength * 0.45, -roomWidth * 0.3);
            myPane.GraphObjList.Add(arrowZ);
            myPane.GraphObjList.Add(raTextZ);
            raTextZ.FontSpec.Border.IsVisible = false;
            raTextZ.FontSpec.Size = 21;


            LineItem noiseCurve = myPane.AddCurve("Ruído",
                  noiseList, Color.Red, SymbolType.Circle);
            noiseCurve.Line.IsVisible = false;
            noiseCurve.Symbol.Size = 10;

            myPane.Legend.FontSpec.Size = 21;
            myPane.Legend.Border.IsVisible = false;

            myPane.Title.IsVisible = false;
            myPane.XAxis.IsVisible = false;
            myPane.YAxis.IsVisible = false;
            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.MinAuto = false;

            myPane.YAxis.Scale.Min = -roomWidth / 2;
            myPane.YAxis.Scale.Max = roomWidth / 2;
            myPane.XAxis.Scale.Min = -roomLength / 2;
            myPane.XAxis.Scale.Max = roomLength / 2;

            myPane.AxisChange();
            graph.Refresh();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            new dbForm(this).Show();
        }

        public double getMeanSRT(double[] iterativeSNR)
        {
            List<double> changed = new List<double>();
            bool currentInclination = false;
            bool lastInclination = false;
            for (int i = 0; i < iterativeSNR.Length - 2; i++)
            {

                if (iterativeSNR[i + 1] - iterativeSNR[i] >= 0)
                {
                    currentInclination = true;
                }
                else
                {
                    currentInclination = false;
                }

                if (lastInclination ^ currentInclination)
                {
                    changed.Add(iterativeSNR[i]);
                }

                lastInclination = currentInclination;

            }
            concatText(string.Format("{0} inversions, SRT: {1}.", changed.Count, changed.Sum() / changed.Count));

            return changed.Sum() / changed.Count;
        }

        public void addCompletedTest(speechPerceptionTest test)
        {
            completedTests.Add(test);
            concatText(string.Format("Teste {0} - {1}", completedTests.Count, test.Label));
            string iterativeString = "Iterative SNR: ";
            foreach (double d in test.IterativeSNR)
            {
                iterativeString += d.ToString() + ", ";
            }
            concatText(iterativeString);
            string[] logText = textBox.Text.Split('\n');
            string testJson = Newtonsoft.Json.JsonConvert.SerializeObject(test);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            concatText(string.Format("Saved logs at {0}", Properties.Settings.Default.RESULTS_FOLDER));
            concatText(testJson);

            updatePatientTest(test.PatientName, timestamp);

            try
            {
                File.WriteAllText(string.Format("{0}/tests/test-{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                timestamp), testJson);

            }
            catch (DirectoryNotFoundException)
            {
                string dir = string.Format("{0}/tests", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(dir);
                File.WriteAllText(string.Format("{0}/tests/test-{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                timestamp), testJson);
            }

            try
            {
                File.WriteAllLines(string.Format("{0}/logs/testlog-{1}.txt",
                                    Properties.Settings.Default.RESULTS_FOLDER,
                                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), logText);

            }
            catch (DirectoryNotFoundException)
            {
                string dir = string.Format("{0}/logs", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(dir);
                File.WriteAllLines(string.Format("{0}/logs/testlog-{1}.txt",
                                    Properties.Settings.Default.RESULTS_FOLDER,
                                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), logText);
            }
        }


        public void addCompletedAudiometry(TonalAudiometryTest Audiometry, string patientName)
        {
            string AudiometryJson = Newtonsoft.Json.JsonConvert.SerializeObject(Audiometry);
            string timestamp;


            if (Audiometry.Side == "Left")
            {
                timestamp = Audiometry.audiometryDate.ToString("dd-MM-yyyy") + " orelha esquerda,";
            }
            else
            {
                timestamp = Audiometry.audiometryDate.ToString("dd-MM-yyyy") + " orelha direita,";
            }

            if (Audiometry.Via == "Air")
            {
                timestamp = timestamp + " via aérea (" + patientName + ")";
            }
            else if (Audiometry.Via == "Bone (mastoid)")
            {
                timestamp = timestamp + " via óssea (mastóide) (" + patientName + ")";
            }
            else if (Audiometry.Via == "Bone (forehead)")
            {
                timestamp = timestamp + " via óssea (fronte) (" + patientName + ")";
            }
            else
            {
                timestamp = timestamp + " campo livre (" + patientName + ")";
            }

            concatText(AudiometryJson);

            try
            {
                File.WriteAllText(string.Format("{0}/audiometry/test-{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                timestamp), AudiometryJson);
            }
            catch (DirectoryNotFoundException)
            {
                string dir = string.Format("{0}/audiometry", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(dir);
                File.WriteAllText(string.Format("{0}/audiometry/test-{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                timestamp), AudiometryJson);
            }
            updatePatientAudiometry(patientName, timestamp);
        }

        public void updatePatientAudiometry(string patientName, string timestamp)
        {
            string jsonFile = string.Format("{0}/patients/{1}.json",
                                            Properties.Settings.Default.RESULTS_FOLDER,
                                            patientName);
            string json = File.ReadAllText(jsonFile);
            Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(json);


            List<string> tests = patient.Audiometrys.OfType<string>().ToList();

            tests.Add(timestamp);
            patient.Audiometrys = tests.ToArray();
            concatText("All audiometrys: " + string.Join(", ", tests.ToArray()));
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(patient);
            File.WriteAllText(jsonFile, output);
        }


        public void updatePatientTest(string patientName, string timestamp)
        {
            string jsonFile = string.Format("{0}/patients/{1}.json",
                                            Properties.Settings.Default.RESULTS_FOLDER,
                                            patientName);
            string json = File.ReadAllText(jsonFile);
            Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(json);


            List<string> tests = patient.Tests.OfType<string>().ToList();

            tests.Add(timestamp);
            patient.Tests = tests.ToArray();
            concatText("All tests: " + string.Join(", ", tests.ToArray()));
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(patient);
            File.WriteAllText(jsonFile, output);
        }


        //??
        private double checkDirection(bool left, bool front, bool right)
        {
            if (left)
            {
                return -90;
            }
            else if (front)
            {
                return 0;
            }
            else
            {
                return 90;
            }
        }

        private bool checkValidScene()
        {

            cond4.Checked = (cond1.Checked & cond2.Checked & cond3.Checked);

            return cond4.Checked;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            noiseFile = Path.Combine(noiseFolder, comboBox3.SelectedItem.ToString());

            concatText("Selected Noise: " + noiseFile);
        }

        private void testSetup_Click(object sender, EventArgs e)
        {
            openTestForm("Default");
        }

        private void audiometryManualTest_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["manualAudiometricTest"] == null)
            {
                try
                {
                    string[] subjects = { applicatorBox.SelectedItem.ToString(), patientBox.SelectedItem.ToString() };
                    new manualAudiometricTest(this, subjects).Show();
                }
                catch (Exception)
                {
                    const string message = "Selecione um paciente e um aplicador para prosseguir";
                    const string caption = "Erro";
                    var result = MessageBox.Show(message, caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        public void openTestForm(string testTipe)
        {
            if (Application.OpenForms["testSetup"] == null)
            {
                try
                {
                    string[] subjects = { applicatorBox.SelectedItem.ToString(), patientBox.SelectedItem.ToString() };
                    new testSetup(this, testTipe, subjects).Show();
                }
                catch (Exception)
                {
                    const string message = "Selecione um paciente e um aplicador para prosseguir";
                    const string caption = "Erro";
                    var result = MessageBox.Show(message, caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["helpForm"] == null)
            {
                new helpForm().Show();
            }
        }

        private void resultsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form2"] == null)
            {
                new Form2(this).Show();       
            }
        }

        private void audioDatabaseEditorAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["dbForm"] == null)
            {
                new dbForm(this).Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["patientManagement"] == null)
            {
                new patientManagement(this).Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["patientManagement"] == null)
            {
                string jsonFile = string.Format("{0}/patients/{1}.json",
                    Properties.Settings.Default.RESULTS_FOLDER,
                    patientBox.SelectedItem.ToString());
                concatText(jsonFile);

                var patientJson = File.ReadAllText(jsonFile);
                Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(patientJson);

                new patientManagement(this, patient).Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja deletar paciente?", patientBox.SelectedItem.ToString(),
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)

            {
                string jsonPatientFile = string.Format("{0}/patients/{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                patientBox.SelectedItem.ToString());
                string json = File.ReadAllText(jsonPatientFile);
                Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(json);

                List<string> tests = patient.Audiometrys.OfType<string>().ToList();

                //delete patient audiometrys
                foreach (string audiometry in tests)
                {
                    string jsonAudiometryFile = string.Format("{0}/audiometry/{1}.json",
                                                Properties.Settings.Default.RESULTS_FOLDER,
                                                "test-" + audiometry);
                    File.Delete(jsonAudiometryFile);
                }
                //delet patient
                File.Delete(jsonPatientFile);
                concatText(string.Format("Deleted patient: {0}", patientBox.SelectedItem.ToString()));
            }
            updatePatientList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (panel3.Visible)
            {
                panel3.Visible = false;
                textBox.Visible = false;

            }
            else
            {
                panel3.Visible = true;
                textBox.Visible = true;
            }
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Sende_mailForm"] == null)
            {
                new Sende_mailForm().Show();
            }
        }

        private void calibraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["calibrationExplanation"] == null)
            {
                new calibrationExplanation(this).Show();
            }
        }


        public void allSoundPlayersPlayScene(double radius, int numberOfSoundPlayers, string speechFile)
        {
            double soundPlayerAngle;
            double[] xSidesList = new double[numberOfSoundPlayers];
            double[] yFrontList = new double[numberOfSoundPlayers];
            double[] zHeightList = new double[numberOfSoundPlayers];
            string[] signalBufferList = new string[numberOfSoundPlayers];
            int[] soundSourceList = new int[numberOfSoundPlayers];

            for (int i = 0; i < numberOfSoundPlayers; i++)
            {
                signalBufferList[i] = vA.CreateSignalSourceBufferFromFile(speechFile);
                soundSourceList[i] = vA.CreateSoundSource("Speech" + i);
            }

            int humanDirectivity = vA.CreateDirectivityFromFile("data/Singer.v17.ms.daff");

            for (int i = 0; i < numberOfSoundPlayers; i++)
            {
                vA.SetSoundSourceDirectivity(soundSourceList[i], humanDirectivity);
            }

            for (int i = 0; i < numberOfSoundPlayers; i++)
            {
                soundPlayerAngle = 90 + (i * (360 / numberOfSoundPlayers));
                if (soundPlayerAngle >= 360)
                {
                    soundPlayerAngle = soundPlayerAngle - 360;
                }
                xSidesList[i] = radius * Math.Sin(soundPlayerAngle / 180 * Math.PI);
                yFrontList[i] = radius * Math.Cos(soundPlayerAngle / 180 * Math.PI);
                zHeightList[i] = 1.7;
            }

            double normalizationFactor;
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                normalizationFactor = Properties.Settings.Default.EARPHONE_VOLUME / 100.0;
            }
            else
            {
                normalizationFactor = Properties.Settings.Default.SPEAKER_VOLUME / 100.0;
            }
            double powerSpeech = 0.25 * normalizationFactor;


            for (int i = 0; i < numberOfSoundPlayers; i++)
            {
                vA.SetSoundSourcePosition(soundSourceList[i], new VAVec3(xSidesList[i], yFrontList[i], zHeightList[i]));
                vA.SetSoundSourceSoundPower(soundSourceList[i], powerSpeech);
                vA.SetSoundSourceSignalSource(soundSourceList[i], signalBufferList[i]);
            }

            for (int i = 0; i < numberOfSoundPlayers; i++)
            {
                vA.SetSignalSourceBufferPlaybackAction(signalBufferList[i], "play");
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                label2.Text = string.Format("Volume: {0} %", Properties.Settings.Default.EARPHONE_VOLUME);
            }
            else
            {
                label2.Text = string.Format("Volume: {0} %", Properties.Settings.Default.SPEAKER_VOLUME);
            }
        }

        private void CreateApplicator_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["applicatorManagementForm"] == null)
            {
                new applicatorManagementForm(this).Show();
            }
        }

        private void DeleteApplicator_Click(object sender, EventArgs e)
        {
            string jsonFile = string.Format("{0}/Applicators/{1}.json",
                                            Properties.Settings.Default.RESULTS_FOLDER,
                                            applicatorBox.SelectedItem.ToString());

            if (MessageBox.Show("Deseja deletar aplicador?", applicatorBox.SelectedItem.ToString(),
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                File.Delete(jsonFile);
                concatText(string.Format("Deleted applicator: {0}", applicatorBox.SelectedItem.ToString()));
            }
            updateApplicatorList();
        }
        public void updateApplicatorList()
        {
            try
            {
                string applicatorDir = string.Format("{0}/Applicators", Properties.Settings.Default.RESULTS_FOLDER);
                string[] applicators = Directory.GetFiles(applicatorDir, "*.json");
                string[] applicatorsNames = applicators.Select(Path.GetFileNameWithoutExtension).ToArray();
                applicatorBox.DataSource = applicatorsNames;
            }
            catch (DirectoryNotFoundException e)
            {
                string applicatorDir = string.Format("{0}/Applicators", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(applicatorDir);
                updateApplicatorList();
            }
        }

        private void ShowApplicatorData_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["applicatorManagementForm"] == null)
            {
                string jsonFile = string.Format("{0}/Applicators/{1}.json",
                    Properties.Settings.Default.RESULTS_FOLDER,
                    applicatorBox.SelectedItem.ToString());
                concatText(jsonFile);

                var applicatorJson = File.ReadAllText(jsonFile);
                Applicator applicator = Newtonsoft.Json.JsonConvert.DeserializeObject<Applicator>(applicatorJson);

                new applicatorManagementForm(this, applicator).Show();
            }
        }

        private void recalibrateAudiometry_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Você realmente deseja recalibrar o audiômetro (os limiares atuais serão perdidos).", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes) 
            {
                Properties.Settings.Default.CALIBRATED_AUDIOMETRY = false;
                Properties.Settings.Default.Save();
            }
        }

        private void resizeScreen() {
            double PCResolutionWidth = Screen.PrimaryScreen.Bounds.Width;
            double PCResolutionHeight = Screen.PrimaryScreen.Bounds.Height;

            double formWidth = this.Size.Width;
            double formHeight = this.Size.Height;

            if ((formWidth > PCResolutionWidth) | (formHeight > PCResolutionHeight*0.925))
            {
                int newWidth = Convert.ToInt32(PCResolutionWidth * 0.786);
                int newHeight = Convert.ToInt32(PCResolutionHeight * 0.9);
                this.Size = new Size(newWidth, newHeight);
            }
        }
    }
}