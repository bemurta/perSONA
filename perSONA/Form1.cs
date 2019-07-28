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


            trackBar2.Value = Properties.Settings.Default.USERVOLUME;
            label2.Text = string.Format("Volume: {0} %", trackBar2.Value);

            string[] filePaths = Directory.GetFiles(@speechFolder, "*.wav");
            string[] fileNames = filePaths.Select(Path.GetFileName).ToArray();
            listBox2.DataSource = fileNames;
            comboBox3.DataSource = Directory.GetFiles(@noiseFolder).Select(Path.GetFileName).ToArray();
            comboBox3.SelectedItem = comboBox3.Items.IndexOf("4talker-babble_ISTS.wav");
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
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



            try
            {
                updatePatientList();
            }
            catch (Exception)
            {

                patientBox.Text = "No previously selected patients";

            }
            
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
            string patientDir = string.Format("{0}/patients", Properties.Settings.Default.RESULTS_FOLDER);
            string[] patients = Directory.GetFiles(patientDir, "*.json");
            string[] patientNames = patients.Select(Path.GetFileNameWithoutExtension).ToArray();
            patientBox.DataSource = patientNames;
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

            string title = getTitle(speechFile);
            string[] words = title.Split(null);

            listBox1.DataSource = words;
            listBox1.ClearSelected();
            concatText(string.Format("Title: {0}, duration: {1}", getTitle(speechFile), getDuration(speechFile)));
        }

        private void play2_Click(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int angle = rnd.Next(360);
            int radius = 2;

            playScene(radius, angle, trackBar1.Value);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value < 40)
                label1.Text = string.Format("SNR: {0} dB", trackBar1.Value);
            else
                label1.Text = string.Format("SNR: INF", trackBar1.Value);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        public VANet getVa()
        {
            return vA;
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = string.Format("Volume: {0} %", trackBar2.Value);
            Properties.Settings.Default.USERVOLUME = trackBar2.Value;
            concatText(string.Format("Changed default volume to {0} % ", trackBar2.Value));
        }

        private void getFolder_Click(object sender, EventArgs e)
        {
            speechFolder = getDatabaseFolder();
            this.TopMost = true;
            string[] filePaths = Directory.GetFiles(@speechFolder, "*.wav");
            string[] fileNames = filePaths.Select(Path.GetFileName).ToArray();
            listBox2.DataSource = fileNames;
        }

        public string getDatabaseFolder()
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


                return speechFolder;
            }

        }

        public void playScene(double radius, double angle, double snr)
        {
            double[] radiusList = { radius, radius };
            double[] angleList = { angle, 0 };

            plotSceneGraph(zedGraphControl1, radiusList, angleList);

            double xSides = radius * Math.Sin(angle / 180 * Math.PI);
            double zFront = radius * Math.Cos(angle / 180 * Math.PI);
            double yHeight = 1.7;

            double normalizationFactor = trackBar2.Value / 100.0;
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
            Thread.Sleep(3000);
            vA.SetSignalSourceBufferPlaybackAction(noiseSound, "stop");

        }

        private void speechLeft_Click(object sender, EventArgs e)
        {
            int angle = -90;
            int radius = 2;
            playScene(radius, angle, trackBar1.Value);
        }

        private void speechRight_Click(object sender, EventArgs e)
        {
            int angle = 90;
            int radius = 2;
            playScene(radius, angle, trackBar1.Value);

        }

        private void speechFront_Click(object sender, EventArgs e)
        {
            int angle = 0;
            int radius = 2;
            playScene(radius, angle, trackBar1.Value);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double answer = listBox1.SelectedItems.Count;
            double totalWords = listBox1.Items.Count;
            textBox2.Text = string.Format("Answer {0}/{1}= {2}% ", answer, totalWords, 100.0 * (answer / totalWords));

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void fillWords(string speechFile, ListBox listbox)
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
                const string MSGINFO = "Sinal de fala não contém as palavras cadastradas. Utlize a Área de edição de arquivos de áudio, acessível pelo menu superior para configurar seus arquivos.";
                const string message = MSGINFO;
                const string caption = "Detectado erro de configuração!";
                var result = MessageBox.Show(message, caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                concatText(string.Format("Wrong database format detected - tag: {0}, dur: {1}", getTitle(speechFile), getDuration(speechFile)));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string speechFile = System.IO.Path.Combine(speechFolder, listBox2.GetItemText(listBox2.SelectedItem));

            concatText(speechFile);
            createAcousticScene(speechFile, noiseFile);

            fillWords(speechFile, listBox1);



        }

        public string getTitle(string speechFile)
        {
            TagLib.File tagFile = TagLib.File.Create(speechFile);

            int channels = tagFile.Properties.AudioChannels;
            int bitrate = tagFile.Properties.AudioBitrate;
            int sampleRate = tagFile.Properties.AudioSampleRate;

            concatText(string.Format("channels: {0}, bitrate: {1}, sampleRate:{2}, n-bits={3}",
                channels, bitrate, sampleRate, 1000*bitrate / (sampleRate * channels)));
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

        private void button2_Click(object sender, EventArgs e)
        {

            int angleSpeech = 45;
            int radiusSpeech = 2;

            int angleNoise = 0;
            int radiusNoise = 2;

            double[] radius = { radiusSpeech, radiusNoise };
            double[] angle = { angleSpeech, angleNoise };

            plotSceneGraph(zedGraphControl1,radius, angle);


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
                    for (double i =-Math.PI/2; i <= Math.PI/2; i += Math.PI)
                    {
                        speakers.Add(radiusSpekers * Math.Sin(i), radiusSpekers * Math.Cos(i));
                    }
                    break;

                case 1:
                    //roomLength = 1.81;
                    //roomWidth = 1.81;
                    radiusSpekers = 1;
                    for (double i = -Math.PI/4; i <= Math.PI/4; i += Math.PI / 2)
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

            ArrowObj arrowZ = new ArrowObj(Color.Black, 25, -roomLength*0.45, -roomWidth*0.45, -roomLength*0.45, -roomWidth*0.35);
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


        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new dbForm(this).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            double[] angles = getSceneAngles();
            double[] radius = getSceneDistances();
            double angleSpeech = checkDirection(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked); ;
            double radiusSpeech = (double)numericUpDown1.Value;
            double angleNoise = checkDirection(radioButton4.Checked, radioButton6.Checked, radioButton5.Checked); ;
            double radiusNoise = (double)numericUpDown2.Value;
            double snr = (double)numericUpDown3.Value;

            speechPerceptionTest speechTest = new speechPerceptionTest(
                                                    angleSpeech, radiusSpeech, 
                                                    angleNoise, radiusNoise,
                                                    speechFolder, noiseFile, 
                                                    textBox1.Text, snr);

            concatText(string.Format("New test: {0}\r\nSpeech R:{1} A:{2}\r\nNoise R:{3} A:{4}",
                            speechTest.Label, radiusSpeech, angleSpeech, radiusNoise, angleNoise));
            new speechIterTestForm(speechTest, this).Show();

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

        public void updatePatientTest(string patientName, string timestamp)
        {
            string jsonFile = string.Format("{0}/patients/{1}.json",
                Properties.Settings.Default.RESULTS_FOLDER,
                patientBox.SelectedItem.ToString());
            string json = File.ReadAllText(jsonFile);
            Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(json);


            List<string> tests = patient.Tests.OfType<string>().ToList();

            tests.Add(timestamp);
            patient.Tests = tests.ToArray();
            concatText("All tests: "+string.Join(", ", tests.ToArray()));
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(patient);
            File.WriteAllText(jsonFile, output);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


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

        private double[] getSceneAngles()
        {
            double angleSpeech = checkDirection(radioButton1.Checked, radioButton2.Checked, radioButton3.Checked);
            double angleNoise = checkDirection(radioButton4.Checked, radioButton6.Checked, radioButton5.Checked);

            double[] angles = { angleSpeech, angleNoise };

            return angles;
        }

        private double[] getSceneDistances()
        {
            double radiusSpeech = (double)numericUpDown1.Value;
            double radiusNoise = (double)numericUpDown2.Value;

            double[] radius = { radiusSpeech, radiusNoise };

            return radius;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());        
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            noiseFile = Path.Combine(noiseFolder,comboBox3.SelectedItem.ToString());

            concatText("Selected Noise: " + noiseFile);
        }

        private void testSetup_Click(object sender, EventArgs e)
        {
            string testTipe = "Default";
            string[] subjects = { applicatorBox.Text, patientBox.SelectedItem.ToString() };
            new testSetup(this, testTipe, subjects).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string testTipe = "Speech Left";
            string[] subjects = { applicatorBox.Text, patientBox.SelectedItem.ToString() };
            new testSetup(this, testTipe, subjects).Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string testTipe = "Speech Front";
            string[] subjects = { applicatorBox.Text, patientBox.SelectedItem.ToString() };
            new testSetup(this, testTipe, subjects).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string testTipe = "Speech Right";
            string[] subjects = { applicatorBox.Text, patientBox.SelectedItem.ToString() };
            new testSetup(this, testTipe, subjects).Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form4().Show();
        }

        private void resultsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2(this).Show();
        }

        private void patientAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3(this).Show();
        }

        private void audioDatabaseEditorAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new dbForm(this).Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Form3(this).Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string jsonFile = string.Format("{0}/patients/{1}.json",
                Properties.Settings.Default.RESULTS_FOLDER,
                patientBox.SelectedItem.ToString());
            concatText(jsonFile);

            var patientJson = File.ReadAllText(jsonFile);
            Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(patientJson);

            new Form3(this, patient).Show();

        }
    }
}
