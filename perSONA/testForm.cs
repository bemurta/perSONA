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

namespace perSONA
{
    public partial class testForm : Form
    {
        private readonly speechPerceptionTest test;
        private readonly IvAInterface vAInterface;
        public string[] speechFiles;
        public string currentFile;

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

            listBox2.DataSource = speechFiles;
            listBox2.SelectedIndex = 0;


            currentFile = System.IO.Path.Combine(test.SpeechFolder, listBox2.GetItemText(listBox2.SelectedItem));

            detailsBox.AppendText(currentFile);
            vAInterface.fillWords(currentFile, listBox1);
            updatePercentage();



            totalWordsText.Text = string.Format("{0}", listBox2.Items.Count);
           


            textBox3.Text = string.Format("{0}", test.SignalToNoise);
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void all_correct_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SetSelected(i, true);
            }
        }


        public void updatePercentage()
        {
            double answer = listBox1.SelectedItems.Count;
            double totalWords = listBox1.Items.Count;

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
            listBox1.ClearSelected();
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

            vAInterface.createAcousticScene(speechFile, test.NoiseFile);

            vAInterface.playScene(test.AngleSpeech, test.AngleNoise, test.SignalToNoise);
        }
    }


}
