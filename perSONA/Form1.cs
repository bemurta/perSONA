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

        string speechFolder = "data/Sounds/Speech/Alcaim1_/F/F0001";
        string noiseFile = "data/Sounds/Noise/4talker-babble_ISTS.wav";
        string noiseFolder = "data/Sounds/Noise";
        string speechSound;
        string noiseSound;
        int speechSource;
        int noiseSource;
        int selectedFolder = 0;

        public Form1()
        {
            InitializeComponent();
            vA = new VANet();
            this.process = new Process
            {
                StartInfo = VAServerProcessInfo()
            };

            String[] filePaths = Directory.GetFiles(@speechFolder, "*.wav");
            String[] fileNames = filePaths.Select(Path.GetFileName).ToArray();
            listBox2.DataSource = fileNames;
            comboBox3.DataSource = Directory.GetFiles(@noiseFolder).Select(Path.GetFileName).ToArray();
            comboBox3.SelectedItem = comboBox3.Items.IndexOf("4talker-babble_ISTS.wav");
            plotSceneGraph(zedGraphControl1, getSceneDistances(), getSceneAngles());
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
                Console.WriteLine("Closed application");
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

        public void concatText(String textToAppend)
        {

            string timestamp = DateTime.Now.ToString(@"dd MMMM yyyy HH:mm:ss - ");

            textBox.Text = String.Concat(textBox.Text, "\r\n", timestamp);

            textBox.Text = String.Concat(textBox.Text, textToAppend);

            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();

        }

        private void createSource2_Click(object sender, EventArgs e)
        {


            String[] filePaths;
            var fileNames = Directory.GetFiles(@"data").Select(Path.GetFileName);
           
            filePaths = Directory.GetFiles(@speechFolder, "*.wav");
            fileNames = filePaths.Select(Path.GetFileName);

            // Create a Random object  
            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(filePaths.Length);

            String speechFile = filePaths[index];
            String noiseFile = "data/Sounds/Noise/4talker-babble_ISTS.wav";

            createAcousticScene(speechFile, noiseFile);

            string title = getTitle(speechFile);
            String[] words = title.Split(null);

            listBox1.DataSource = words;
            listBox1.ClearSelected();
            concatText(String.Format("Title: {0}, duration: {1}", getTitle(speechFile), getDuration(speechFile)));
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

            label1.Text = String.Format("SNR: {0} dB", trackBar1.Value);
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
            label2.Text = String.Format("Volume: {0} %", trackBar2.Value);
        }

        private void getFolder_Click(object sender, EventArgs e)
        {
            speechFolder = getDatabaseFolder();
            this.TopMost = true;
            String[] filePaths = Directory.GetFiles(@speechFolder, "*.wav");
            String[] fileNames = filePaths.Select(Path.GetFileName).ToArray();
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

            vA.SetSoundSourcePosition(speechSource, new VAVec3(xSides, yHeight, zFront));
            vA.SetSoundSourcePosition(noiseSource, new VAVec3(0, 1.7, radius));

            vA.SetSoundSourceSoundPower(speechSource, powerSpeech);
            vA.SetSoundSourceSignalSource(speechSource, speechSound);

            vA.SetSoundSourceSoundPower(noiseSource, powerNoise);
            vA.SetSoundSourceSignalSource(noiseSource, noiseSound);

            concatText(String.Format("\r\nCreated Source: {3} at position: {0},{1},{2}, looking forward",
                       xSides, zFront, yHeight, speechSource));

            concatText(String.Format("linear ratio: {2} ({3} dB), speech power: {0}, noise power: {1} - Volume: {4} %",
                       powerSpeech, powerNoise, linRatio, 20 * Math.Log10(linRatio), normalizationFactor * 100.0));

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
            textBox2.Text = String.Format("Answer {0}/{1}= {2}% ", answer, totalWords, 100.0 * (answer / totalWords));

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void fillWords(string speechFile, ListBox listbox)
        {
            string title = getTitle(speechFile);

            if (!String.IsNullOrEmpty(title))
            {
                String[] words = title.Split(null);
                listbox.DataSource = words;
                listbox.ClearSelected();
                //concatText(String.Format("Title: {0}, duration: {1}", getTitle(speechFile), getDuration(speechFile)));
            }
            else
            {
                const string message =
                    "Wrong set up of database wav files. Please refer to database edit module to fix and use it in your tests.";
                const string caption = "Incorrect database format. Metadata required!";
                var result = MessageBox.Show(message, caption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                concatText(String.Format("Wrong database format detected - tag: {0}, dur: {1}", getTitle(speechFile), getDuration(speechFile)));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String speechFile = System.IO.Path.Combine(speechFolder, listBox2.GetItemText(listBox2.SelectedItem));
            String noiseFile = "data/Sounds/Noise/4talker-babble_ISTS.wav";
            concatText(speechFile);
            createAcousticScene(speechFile, noiseFile);

            fillWords(speechFile, listBox1);



        }

        public string getTitle(string speechFile)
        {
            TagLib.File tagFile = TagLib.File.Create(speechFile);
            
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

            concatText(String.Format("\r\nCreated Source Signals: {0} with file: {1}, {2} with file {3}",
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

            double roomLength = 5;
            double roomWidth = 5;


            ZedGraph.GraphPane myPane = graph.GraphPane;

            myPane.CurveList.Clear();


            PointPairList speechList = new PointPairList();
            PointPairList noiseList = new PointPairList();
            PointPairList circle = new PointPairList();
            PointPairList head = new PointPairList();
            PointPairList speakers = new PointPairList();
            PointPairList nose = new PointPairList();

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

            double radiusSpekers = 1.3;
            for (double i = 0; i < 2 * Math.PI; i += Math.PI / 4)
            {
                speakers.Add(radiusSpekers * Math.Sin(i), radiusSpekers * Math.Cos(i));
            }

            LineItem speakersCurve = myPane.AddCurve("Speakers",
                   speakers, Color.Black, SymbolType.Star);
            speakersCurve.Line.IsVisible = false;

            LineItem speechCurve = myPane.AddCurve("Speech",
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


            LineItem noiseCurve = myPane.AddCurve("Noise",
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

            concatText(String.Format("New test: {0}\r\nSpeech R:{1} A:{2}\r\nNoise R:{3} A:{4}",
                            speechTest.Label, radiusSpeech, angleSpeech, radiusNoise, angleNoise));
            new speechIterTestForm(speechTest, this).Show();

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
            concatText(String.Format("Saved logs at {0}", Properties.Settings.Default.RESULTS_FOLDER));
            File.WriteAllLines(@String.Format("{0}/testlog-{1}.txt", 
                                Properties.Settings.Default.RESULTS_FOLDER,DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")), logText);
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
            noiseFile = Path.Combine(noiseFolder,comboBox3.SelectedText.ToString()); 
        }

        private void testSetup_Click(object sender, EventArgs e)
        {
            string testTipe = "Default";
            new testSetup(this, testTipe).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string testTipe = "Speech Left";
            new testSetup(this, testTipe).Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string testTipe = "Speech Front";
            new testSetup(this, testTipe).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string testTipe = "Speech Right";
            new testSetup(this, testTipe).Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resultsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2(this).Show();
        }
    }
}
