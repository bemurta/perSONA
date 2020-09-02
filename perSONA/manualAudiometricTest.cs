using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using Newtonsoft.Json;
using System.IO;
using VA;

namespace perSONA
{
    public partial class manualAudiometricTest : Form
    {
        private readonly IvAInterface vAInterface;

        public bool calibratedSystem = Properties.Settings.Default.CALIBRATED_AUDIOMETRY;
        public double[] intensitRef = new double[11];

        public double currentVolumePower = 0;

        public VANet vA { get; private set; }

        //Sounds (pure tones)
        public Dictionary<double, string> freq_sound = new Dictionary<double, string>();
        //Noise (mask filter signals)
        public Dictionary<double, string> freq_noise = new Dictionary<double, string>();

        //left
        public List<double> leftFreqs = new List<double>();
        public Dictionary<double, double> leftFreqs_db = new Dictionary<double, double>();
        public Dictionary<double, double> leftFreqs_mask = new Dictionary<double, double>();
        public Dictionary<double, bool> leftFreqs_noReply = new Dictionary<double, bool>();

        //right
        public List<double> rightFreqs = new List<double>();
        public Dictionary<double, double> rightFreqs_db = new Dictionary<double, double>();
        public Dictionary<double, double> rightFreqs_mask = new Dictionary<double, double>();
        public Dictionary<double, bool> rightFreqs_noReply = new Dictionary<double, bool>();

        public Dictionary<int, double> mapFreqs = new Dictionary<int, double>(){{1, 125}, {2, 250}, {3, 500}, {4, 750}, {5, 1000}, {6, 1500},
                                                                                {7, 2000}, {8, 3000}, {9, 4000}, {10, 6000}, {11, 8000}};
        string[] subjects;

        int correctAnswers = 0;

        String Side = "Right";
        String Via = "Air";

        int freqID = 5;
        double currentFrequency = 1000;

        bool mask = false;
        double currentMask = 0;

        double currentdB = 50;
        bool currentNoReply = false;

        public manualAudiometricTest(IvAInterface ivAInterface, string[] subjects)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
            this.subjects = subjects;
            patientLabel.Text = subjects[1];
            applicatorLabel.Text = subjects[0];
            mask = true;
            showMask();
            mask = false;
            TonalAudiometryTest.bindGraph(leftGraph);
            TonalAudiometryTest.bindGraph(rightGraph);
        }
        private void manualAudiometricTest_Shown(object sender, EventArgs e)
        {
            foreach(double freq in mapFreqs.Values)
            {
                freq_sound.Add(freq, "data\\Sounds\\Audiometry\\" + freq.ToString() + ".wav");
                freq_noise.Add(freq, "data\\Sounds\\Audiometry\\mask" + freq.ToString() + ".wav");
            }
            //calibratedSystem = false;  //Só para testes

            if (!calibratedSystem)
            {
                calibrateEnable.BackColor = Color.Purple;
                leftGraph.Visible = false;
                rightGraph.Visible = false;
                const string message = "Olá, bem-vindo a interface de audiometria tonal do perSONA,\n" + "antes de realizar audiometrias é fundamental calibrar o sistema,\n" + "para isso, clique no botão \"Calibrate\".";
                const string caption = "Bem-vindo";
                MessageBox.Show(message, caption,
                    MessageBoxButtons.OK);
                calibrateEnable.BackColor = Color.Black;
            }
            else
            {
                calibrationPanel.Visible = false;
                volumePanel.Visible = false;
                intensitRef = Properties.Settings.Default.CALIBRATED_AUDIOMETRY_VALUES;
                updateVolumePower();
            }
        }

        private void changeVia_Click(object sender, EventArgs e)
        {
            if (leftFreqs_db.Count == 0 & rightFreqs_db.Count == 0)
            {
                if (Via == "Air")
                {
                    viaLightAir.BackColor = Color.Silver;
                    viaLightBoneM.BackColor = Color.Yellow;
                    Via = "Bone (mastoid)";
                }
                else
                {
                    viaLightAir.BackColor = Color.Yellow;
                    viaLightBoneM.BackColor = Color.Silver;
                    Via = "Air";
                }
                updateCorrectAnswers(correctAnswers = 0);
            }
            else
            {
                const string message = "Não é possível mudar a via durante o teste,\n" + "para isso, reinicie o teste previamente.";
                const string caption = "Erro";
                MessageBox.Show(message, caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void activateMask_Click(object sender, EventArgs e)
        {
            updateCorrectAnswers(correctAnswers = 0);
            mask = !mask;
            if (mask) maskLight.BackColor = Color.Yellow;
            else
            {
                maskLight.BackColor = Color.Silver;
                currentMask = 0;
                showMask();
            }
        }

        private void changeSide_Click(object sender, EventArgs e)
        {
            if (Side == "Left")
            {
                sideLightLeft.BackColor = Color.Silver;
                sideLightRight.BackColor = Color.Yellow;
                Side = "Right";
            }
            else
            {
                sideLightLeft.BackColor = Color.Yellow;
                sideLightRight.BackColor = Color.Silver;
                Side = "Left";
            }
            updateCorrectAnswers(correctAnswers = 0);
        }
        private void reset_Click(object sender, EventArgs e)
        {
            resetAll(leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply, leftGraph);
            resetAll(rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply, rightGraph);
        }

        private void resetAll(List<double> freqs, Dictionary<double, double> db, Dictionary<double, double> mask, Dictionary<double, bool> noReply, ZedGraphControl graph)
        {
            graph.GraphPane.CurveList.Clear();
            freqs.Clear();
            db.Clear();
            mask.Clear();
            noReply.Clear();
            TonalAudiometryTest.updateGraph(graph);
            updateCorrectAnswers(correctAnswers = 0);
        }
        
        private void saveFrequency_Click(object sender, EventArgs e)
        {
            if (calibratedSystem)
            {
                if (Side == "Left")
                {
                    saveThisFrequency(leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply, leftGraph);
                }
                else
                {
                    saveThisFrequency(rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply, rightGraph);
                }
                updateCorrectAnswers(correctAnswers = 0);
            }
            else
            {
                intensitRef[freqID - 1] = volumeBar.Value + (decimalVolumeBar.Value / 100.0);
                if (freqID == 1)
                {
                    MessageBox.Show("Sistema calibrado.", "Sucesso", MessageBoxButtons.OK);
                    calibratedSystem = true;
                    Properties.Settings.Default.CALIBRATED_AUDIOMETRY_VALUES = intensitRef;
                    Properties.Settings.Default.CALIBRATED_AUDIOMETRY = calibratedSystem;
                    Properties.Settings.Default.Save();
                    vAInterface.concatText(Properties.Settings.Default.CALIBRATED_AUDIOMETRY_VALUES.ToString());
                    Close();
                }
                else
                {
                    freqID--;
                    showFreq();
                }
            }
        }
        private void saveThisFrequency(List<double> freqs, Dictionary<double, double> db, Dictionary<double, double> mask, Dictionary<double, bool> noReply, ZedGraphControl graph)
        {
            if (!freqs.Contains(currentFrequency))
            {
                double linearizedFreq = Math.Log(mapFreqs[freqID] / 125, 2) + 1;
                TonalAudiometryTest.drawSymbol(graph, linearizedFreq, currentdB, currentMask, false, Side, Via);
                freqs.Add(currentFrequency);
                db.Add(currentFrequency, currentdB);
                mask.Add(currentFrequency, currentMask);
                noReply.Add(currentFrequency, currentNoReply);
                TonalAudiometryTest.updateGraph(graph);
            }
            else
            {
                MessageBox.Show("Frequência já registrada, se desejar você pode reiniciar o teste", "Erro",
                                                                                MessageBoxButtons.OK,
                                                                                MessageBoxIcon.Error);
            }
        }
        
        private void End_Click(object sender, EventArgs e)
        {
            TonalAudiometryTest leftAudiometry = new TonalAudiometryTest();
            TonalAudiometryTest rightAudiometry = new TonalAudiometryTest();
            makeAudiometry(leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply, leftGraph, leftAudiometry, "Left");
            makeAudiometry(rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply, rightGraph, rightAudiometry, "Right");

            this.Close();
        }
        private void makeAudiometry(List<double> freqs, Dictionary<double, double> dicDB, Dictionary<double, double> dicMask, Dictionary<double, bool> dicNoReply, ZedGraphControl graph, TonalAudiometryTest Audiometry, string Side)
        {
            string portugueseSide;
            string portugueseVia;

            if (Side == "Left") portugueseSide = "esquerda";
            else portugueseSide = "direita";
            if (Via == "Air") portugueseVia = "aérea";
            else portugueseVia = "óssea (mastóide)";

            if (freqs.Count > 0)
            {
                List<double> db = new List<double>();
                List<double> mask = new List<double>();
                List<bool> noReply = new List<bool>();
                List<double> linearizedFreqs = new List<double>();

                freqs.Sort();

                foreach (double frequency in freqs)
                {
                    db.Add(dicDB[frequency]);
                    mask.Add(dicMask[frequency]);
                    noReply.Add(dicNoReply[frequency]);
                    linearizedFreqs.Add(Math.Log(frequency / 125, 2) + 1);
                }

                PointPairList audiometry = new PointPairList
                {
                    {linearizedFreqs.ToArray(), db.ToArray()}
                };
                LineItem audiometryCurve;

                Audiometry.Via = "Air";
                Audiometry.Side = Side;
                Audiometry.Freqs = freqs;
                Audiometry.dB = db;
                Audiometry.Masker = mask;
                Audiometry.NoReply = noReply;

                Audiometry.audiometryDate = DateTime.Today;

                string audiometryType = "";
                audiometryType = "Orelha " + portugueseSide;
                audiometryType = audiometryType + ", via " + portugueseVia;

                Audiometry.AudiometryType = audiometryType;

                audiometryCurve = graph.GraphPane.AddCurve(Audiometry.AudiometryType, audiometry, Audiometry.getColor(), SymbolType.None);
                audiometryCurve.IsX2Axis = true;
                Audiometry.changeLine(audiometryCurve.Line);
                TonalAudiometryTest.updateGraph(graph);

                vAInterface.addCompletedAudiometry(Audiometry, patientLabel.Text);

                MessageBox.Show("Audiometria da orelha " + portugueseSide + " salva", "Sucesso", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Orelha " + portugueseSide + " não salva", "Sucesso", MessageBoxButtons.OK);
            }
        }
        
        //Freqs, mask and db up/down
        private void freqUp_Click(object sender, EventArgs e)
        {
            freqID++;
            if (freqID > 11) freqID = 11;
            showFreq();
        }

        private void freqDown_Click(object sender, EventArgs e)
        {
            freqID--;
            if (freqID < 1) freqID = 1;
            showFreq();
        }
        private void showFreq()
        {
            freqLabel.Text = mapFreqs[freqID].ToString();
            currentFrequency = mapFreqs[freqID];
            freqProgressBar.Value = freqID;
            updateCorrectAnswers(correctAnswers = 0);
            updateVolumePower();
        }

        private void dBUp_Click(object sender, EventArgs e)
        {
            currentdB = currentdB + 5;
            if (currentdB > 120) currentdB = 120;
            showdB();
        }

        private void dBDown_Click(object sender, EventArgs e)
        {
            currentdB = currentdB - 5;
            if (currentdB < -10) currentdB = -10;
            showdB();
        }
        private void showdB()
        {
            dBLabel.Text = currentdB.ToString();
            dBProgressBar.Value = Convert.ToInt32(currentdB) + 10;
            updateCorrectAnswers(correctAnswers = 0);
            updateVolumePower();
        }
        private void maskUp_Click(object sender, EventArgs e)
        {
            if (mask)
            {
                currentMask = currentMask + 5;
                if (currentMask > 120) currentMask = 120;
            }
            else
            {
                MessageBox.Show("Ative o mascaramento para alterar a intensidade", "Erro",
                                                                    MessageBoxButtons.OK,
                                                                    MessageBoxIcon.Error);
            }
            showMask();
        }

        private void maskDown_Click(object sender, EventArgs e)
        {
            if (mask)
            {
                currentMask = currentMask - 5;
                if (currentMask < 0) currentMask = 0;
            }
            else
            {
                MessageBox.Show("Ative o mascaramento para alterar a intensidade", "Erro",
                                                                    MessageBoxButtons.OK,
                                                                    MessageBoxIcon.Error);
            }
            showMask();
        }
        private void showMask()
        {
            maskLabel.Text = currentMask.ToString();
            maskProgressBar.Value = Convert.ToInt32(currentMask);
            updateCorrectAnswers(correctAnswers = 0);
        }

        private void Sound_MouseUp(object sender, MouseEventArgs e)
        {
            soundLight.BackColor = Color.Silver;
            vAInterface.stopScene();
        }

        private void Sound_MouseDown(object sender, MouseEventArgs e)
        {
            soundLight.BackColor = Color.Yellow;

            double radius = 3;
            vA = vAInterface.getVa();

            vA.Reset();
            int receiverId = vA.CreateSoundReceiver("Subject");

            double xSides = 0;
            double zFront = 0;
            double yHeight = 1.7;


            VAVec3 receiverPosition = new VAVec3(xSides, yHeight, zFront);
            VAVec3 receiverOrientationV = new VAVec3(0, 0, -1);
            VAVec3 receiverOrientationU = new VAVec3(0, 1, 0);

            vA.SetSoundReceiverPosition(receiverId, receiverPosition);      //this receiver have position (xSides, yHeight, zFront)
            vA.SetSoundReceiverOrientationVU(receiverId, receiverOrientationV, receiverOrientationU); //this receive look ahead with the top of the head up
            vAInterface.concatText(string.Format("Receiver: {3} at position: {0},{1},{2}, looking forward ", xSides, zFront, yHeight, receiverId));

            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);

            string speechFile = freq_sound[currentFrequency];
            string noiseFile = freq_noise[currentFrequency];

            vAInterface.concatText(speechFile);

            if (calibratedSystem)
            {
                if (currentVolumePower < 100)
                {
                    if (Side == "Left")
                    {
                        vAInterface.createAcousticScene(speechFile, speechFile);
                        vAInterface.playScene(radius, 270, 40, currentVolumePower, intensitRef[freqID - 1]);
                    }
                    else
                    {
                        vAInterface.createAcousticScene(speechFile, speechFile);
                        vAInterface.playScene(radius, 90, 40, currentVolumePower, intensitRef[freqID - 1]);
                    }
                }
                else
                {
                    MessageBox.Show("Limite de resposta do audiômetro atingido", "Erro",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                }
            }
            else
            {
                vAInterface.createAcousticScene(speechFile, speechFile);
                vAInterface.playScene(radius, 0, 40, currentVolumePower, volumeBar.Value+(decimalVolumeBar.Value / 100.0));
            }
        }

        private void manualAudiometricTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 116 | e.KeyValue == 179)
            {
                updateCorrectAnswers(++correctAnswers);
            }
        }
        private void Correct_Click(object sender, EventArgs e)
        {
            updateCorrectAnswers(++correctAnswers);
        }
        private void Incorrect_Click(object sender, EventArgs e)
        {
            correctAnswers --;
            if (correctAnswers < 0) correctAnswers = 0;
            updateCorrectAnswers(correctAnswers);
        }
        private void updateCorrectAnswers(int correctAnswer)
        {
            correctAnswersLabel.Text = correctAnswer.ToString();
        }

        private void calibrateEnable_Click(object sender, EventArgs e)
        {
            dbPanel.Visible = false;
            soundDetectedPanel.Visible = false;
            resetPanel.Visible = End.Visible = false;
            maskPanel.Visible = onOffMaskPanel.Visible = false;
            viaPanel.Visible = false;
            sidePanel.Visible = false;
            freqUp.Visible = freqDown.Visible = false;
            currentdB = 0;
            freqID = 11;
            showFreq();
            showdB();

            calibrateLight.BackColor = Color.Yellow;
            MessageBox.Show("A calibração utilizará seu limiar auditivo como referência, assumindo que você tem uma audição saudável, " +
                "portanto, você precisa ajustar o volume em cada uma das frequências até a minima intensidade em que ainda seja possível ouvir.", "Calibração  1/5", MessageBoxButtons.OK);
            MessageBox.Show("Ajuste o volume do seu computador para 10%.", "Calibração  2/5", MessageBoxButtons.OK);
            volumeBar.BackColor = decimalVolumeBar.BackColor = Color.Purple;
            MessageBox.Show("Para cada uma das frequências que serão calibradas, ajuste o volume através das trackbars \"Volume Power\", o resultado desejado é o menor volume com som audível.", "Calibração  4/5", MessageBoxButtons.OK);
            volumeBar.BackColor = decimalVolumeBar.BackColor = SystemColors.Control;
            Sound.BackColor = Color.Purple;
            MessageBox.Show("O som correspondente a cada frequência pode ser gerando clicando no botão (\"Sound\").", "Calibração  4/5", MessageBoxButtons.OK);
            Sound.BackColor = Color.Black;
            saveFrequency.BackColor = Color.Purple;
            MessageBox.Show("Após calibrar salve o resultado do limiar para cada frequência clicando em (\"Save\").", "Calibração  5/5", MessageBoxButtons.OK);
            saveFrequency.BackColor = Color.Black;
        }

        private void updateVolumePower()
        {
            currentVolumePower = Math.Pow(10, currentdB / 20.0) * intensitRef[freqID - 1];

            if (currentVolumePower > 100) currentNoReply = true;
            else currentNoReply = false;
        }
        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            volumeLabel.Text = (volumeBar.Value + (decimalVolumeBar.Value/100.0)).ToString();
        }

        private void decimalVolumeBar_Scroll(object sender, EventArgs e)
        {
            volumeLabel.Text = (volumeBar.Value + (decimalVolumeBar.Value / 100.0)).ToString();
        }
    }
}