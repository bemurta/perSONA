using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VA;
using ZedGraph;

namespace perSONA
{
    public partial class testForm : Form
    {
        private readonly speechPerceptionTest test;
        private readonly IvAInterface vAInterface;
        public string[] speechFiles;
        public string currentFile;
        public bool currentStreak = false;
        private double actualSNR;
        double[] signalToNoiseArray;

        public VANet vA { get; private set; }

        public testForm(speechPerceptionTest test, IvAInterface vAInterface)
        {
            InitializeComponent();
            this.test = test;
            this.vAInterface = vAInterface;

            double[] radiusList = { test.RadiusSpeech, test.RadiusNoise };
            double[] angleList = { test.AngleSpeech, test.AngleNoise };

            vAInterface.plotGraph(zedGraphControl2.GraphPane, radiusList, angleList);

            detailsBox.Text = test.ToString();

            String[] filePaths = System.IO.Directory.GetFiles(test.SpeechFolder, "*.wav");
            speechFiles = filePaths.Select(System.IO.Path.GetFileName).ToArray();

            filenameList.DataSource = speechFiles;
            filenameList.SelectedIndex = 0;

            currentFile = System.IO.Path.Combine(test.SpeechFolder, filenameList.GetItemText(filenameList.SelectedItem));

            detailsBox.AppendText(currentFile);
            vAInterface.fillWords(currentFile, testWordsList);
            updatePercentage();
            computedAudioText.Text = (filenameList.SelectedIndex + 1).ToString();
            totalWordsText.Text = string.Format("{0}", filenameList.Items.Count);
            actualSNR = test.SignalToNoise;
            textBox3.Text = string.Format("{0}", actualSNR); 


            signalToNoiseArray = new double[] {actualSNR};
            updateIterationGraph(zedGraphControl1.GraphPane, signalToNoiseArray);




        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void all_correct_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < testWordsList.Items.Count; i++)
            {
                testWordsList.SetSelected(i, true);
            }
        }


        public void updatePercentage()
        {
            double answer = testWordsList.SelectedItems.Count;
            double totalWords = testWordsList.Items.Count;

            textBox1.Text = string.Format("{0}", answer);
            textBox2.Text = string.Format("{0}%", 100.0 * (answer / totalWords));

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePercentage();      
        }

        private void all_incorrect_Click(object sender, EventArgs e)
        {
            testWordsList.ClearSelected();
        }

        private void playCurrentScene_Click(object sender, EventArgs e)
        {
            vA = vAInterface.getVa();

            vA.Reset();
            int receiverId = vA.CreateSoundReceiver("Subject");

            double xSides = 0;
            double zFront = 0;
            double yHeight = 1.7;


            VAVec3 receiverPosition = new VAVec3(xSides, yHeight, zFront);
            VAVec3 receiverOrientationV = new VAVec3(0, 0, -1);
            VAVec3 receiverOrientationU = new VAVec3(0, 1, 0);

            vA.SetSoundReceiverPosition(receiverId, receiverPosition);
            vA.SetSoundReceiverOrientationVU(receiverId, receiverOrientationV, receiverOrientationU);
            vAInterface.concatText(String.Format("\r\nCreated Receiver: {3} at position: {0},{1},{2}, looking forward ",
                                     xSides, zFront, yHeight, receiverId));

            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);

            String speechFile = currentFile;
            vAInterface.concatText(speechFile);
            vAInterface.concatText(
                String.Format("Angle speech: {0}, Angle noise: {1}",test.AngleSpeech, test.AngleNoise));
            vAInterface.createAcousticScene(speechFile, test.NoiseFile);

            vAInterface.playScene(test.RadiusSpeech, test.AngleSpeech, actualSNR);


        }

        private void updateIterationGraph(GraphPane graph, double[] signalToNoiseArray)
        {

            ZedGraph.GraphPane myPane = graph;
            myPane.CurveList.Clear();
            PointPairList snrArray = new PointPairList();
            List<double> indexes = new List<double>();
            for (int i = 1; i < signalToNoiseArray.Length + 2; i++)
            {
                double value = i;
                indexes.Add(value);
            }

            snrArray.Add(indexes.ToArray(), signalToNoiseArray);
            LineItem snrCurve = myPane.AddCurve("Iterative SNR", snrArray, Color.Blue, SymbolType.XCross);
            snrCurve.Line.IsVisible = true;
            snrCurve.Line.Width = 2;
            snrCurve.Symbol.Size = 20;

            myPane.Legend.FontSpec.Size = 21;
            myPane.Legend.Border.IsVisible = false;
            myPane.Title.FontSpec.Size = 21;
            myPane.XAxis.Title.FontSpec.Size = 21;
            myPane.XAxis.Scale.FontSpec.Size = 21;

            myPane.YAxis.Title.FontSpec.Size = 21;
            myPane.YAxis.Scale.FontSpec.Size = 21;

            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.MinAuto = false;
            myPane.YAxis.Scale.Min = -30;
            myPane.YAxis.Scale.Max = 30;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = signalToNoiseArray.Length + 3;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private double getNextSNR(double currentSNR, double snrStep)
        {
            double answer = testWordsList.SelectedItems.Count;
            double totalWords = testWordsList.Items.Count;
            double nextSNR;

            if (answer / totalWords < test.AcceptanceRule)
            {
                nextSNR = currentSNR + snrStep;
                streakText.Text = "False";
            }
            else
            {
                if (currentStreak)
                {
                    nextSNR = currentSNR - snrStep;
                    currentStreak = false;
                    streakText.Text = "False";
                }
                else
                {
                    nextSNR = currentSNR;
                    currentStreak = true;
                    streakText.Text = "True";
                }
            }

            return nextSNR;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            updateIterationGraph(zedGraphControl1.GraphPane , signalToNoiseArray);
        }

        private void button1_Click(object sender, EventArgs e)
        {


            actualSNR = getNextSNR(actualSNR, test.SignalToNoiseStep);
            
            if (filenameList.SelectedIndex + 1 < filenameList.Items.Count)
            {
                filenameList.SelectedIndex = filenameList.SelectedIndex + 1;
                currentFile = System.IO.Path.Combine(test.SpeechFolder, filenameList.GetItemText(filenameList.SelectedItem));

                detailsBox.AppendText(currentFile);
                vAInterface.fillWords(currentFile, testWordsList);
                updatePercentage();

                computedAudioText.Text = (filenameList.SelectedIndex + 1).ToString();
                totalWordsText.Text = string.Format("{0}", filenameList.Items.Count);

                signalToNoiseArray =  signalToNoiseArray.Concat(new double[] { actualSNR }).ToArray();
                textBox3.Text = string.Format("{0}", actualSNR);
                updateIterationGraph(zedGraphControl1.GraphPane, signalToNoiseArray);
            }
            else
            {
                test.IterativeSNR = signalToNoiseArray;

                detailsBox.AppendText("/r/n Finished list");
                vAInterface.addCompletedTest(this.test);
                this.Close();
            }


        }
    }


}
