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
        public double[] maskIntensitRef = new double[11];

        public double currentVolumePower = 0;
        public double currentMaskVolumePower = 0;
        public bool isVAConfigured = false;

        public VANet vA { get; private set; }

        //Undo
        public List<string> modifications = new List<string>();

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

        //leftBone
        public List<double> leftFreqsB = new List<double>();
        public Dictionary<double, double> leftFreqs_dbB = new Dictionary<double, double>();
        public Dictionary<double, double> leftFreqs_maskB = new Dictionary<double, double>();
        public Dictionary<double, bool> leftFreqs_noReplyB = new Dictionary<double, bool>();

        //rightBone
        public List<double> rightFreqsB = new List<double>();
        public Dictionary<double, double> rightFreqs_dbB = new Dictionary<double, double>();
        public Dictionary<double, double> rightFreqs_maskB = new Dictionary<double, double>();
        public Dictionary<double, bool> rightFreqs_noReplyB = new Dictionary<double, bool>();

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

        double radius;
        public manualAudiometricTest(IvAInterface ivAInterface, string[] subjects)
        {
            InitializeComponent();
            resizeScreen();
            this.vAInterface = ivAInterface;
            this.subjects = subjects;
            patientLabel.Text = subjects[1];
            applicatorLabel.Text = subjects[0];

            maskLabel.Text = currentMask.ToString();
            maskProgressBar.Value = Convert.ToInt32(currentMask);

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

            if (!calibratedSystem)
            {
                calibrateEnable.BackColor = Color.Purple;
                graphsPanel.Visible = false;
                const string message = "Olá, bem-vindo a interface de audiometria tonal limiar do perSONA,\n" + "antes de realizar audiometrias é fundamental fazer a calibração do audiômetro, para isso, clique no botão \"Calibrate\".";
                const string caption = "Bem-vindo";
                MessageBox.Show(message, caption,
                    MessageBoxButtons.OK);
                calibrateEnable.BackColor = Color.Black;
            }
            else
            {
                allCalibrationPanel.Visible = false;
                allPowerPanel.Visible = false;

                //Speech reference sound power
                intensitRef = Properties.Settings.Default.CALIBRATED_AUDIOMETRY_VALUES;

                //Noise reference sound power
                int i = 0;
                foreach (double freq in intensitRef)
                {
                    if (i < 3) maskIntensitRef[i] = Math.Pow(10, 4 / 20.0) * freq;              //125, 250, 500                    
                    else if (i < 4 | i > 7) maskIntensitRef[i] = Math.Pow(10, 5 / 20.0) * freq; //750, 4000, 6000, 8000
                    else maskIntensitRef[i] = Math.Pow(10, 6 / 20.0) * freq;                    //1000, 1500, 2000, 3000
                    i++;
                    vAInterface.concatText(freq.ToString());
                }
                updateVolumePower();
                MessageBox.Show("Lembre-se de ajustar o volume do computador em 100.", "Aviso", MessageBoxButtons.OK);
            }
        }

        private void VaConfig()
        {
            if (!isVAConfigured)
            {
                isVAConfigured = true;
                radius = 3;
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

                int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
                vA.SetSoundReceiverDirectivity(receiverId, hrirId);
            }
        }

        private void changeVia_Click(object sender, EventArgs e)
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
        private void activateMask_Click(object sender, EventArgs e)
        {
            updateCorrectAnswers(correctAnswers = 0);
            if (!mask)
            {
                maskLight.BackColor = Color.Yellow;
                PlayNoise();
                mask = !mask;
            }
            else
            {
                turnOffMask();
            }
        }

        private void turnOffMask(){
                maskLight.BackColor = Color.Silver;
                currentMask = 0;
                showMask();
                vAInterface.stopScene(false, true);
                mask = !mask;
        }
        
        private void PlayNoise()
        {
            //if (isVAConfigured)
            //{
            //    vAInterface.stopScene(false, true);
            //}
            VaConfig();
            showMask();

            string speechFile = freq_sound[currentFrequency];
            string noiseFile = freq_noise[currentFrequency];

            if (currentMaskVolumePower < 200000)
            {
                if (Side == "Left") vAInterface.playScene(radius, false, speechFile, 0, 0, true, noiseFile, 90, currentMaskVolumePower); 
                else vAInterface.playScene(radius, false, speechFile, 0, 0, true, noiseFile, 270, currentMaskVolumePower);
            }
            else
            {
                soundLight.BackColor = Color.Silver;
                MessageBox.Show("Limite de resposta do audiômetro atingido", "Erro",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                
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
        
        private void saveFrequency_Click(object sender, EventArgs e)
        {
            if (calibratedSystem)
            {
                if (Side == "Left" && Via == "Air")
                {
                    saveThisFrequency(leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply, leftGraph);
                }
                else if (Side == "Right" && Via == "Air")
                {
                    saveThisFrequency(rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply, rightGraph);
                }
                else if (Side == "Left" && Via == "Bone (mastoid)")
                {
                    saveThisFrequency(leftFreqsB, leftFreqs_dbB, leftFreqs_maskB, leftFreqs_noReplyB, leftGraph);
                }
                else if (Side == "Right" && Via == "Bone (mastoid)")
                {
                    saveThisFrequency(rightFreqsB, rightFreqs_dbB, rightFreqs_maskB, rightFreqs_noReplyB, rightGraph);
                }
                updateCorrectAnswers(correctAnswers = 0);
            }
            else
            {
                intensitRef[freqID - 1] = Decimal.ToDouble(volume.Value);
                if (freqID == 1)
                {
                    MessageBox.Show("Audiômetro calibrado.", "Sucesso", MessageBoxButtons.OK);
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
                TonalAudiometryTest.drawSymbol(graph, linearizedFreq, currentdB, currentMask, currentNoReply, Side, Via);
                vAInterface.concatText(linearizedFreq.ToString());
                modifications.Add(Side+","+Via);
                vAInterface.concatText(modifications[modifications.Count() - 1]);
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
            TonalAudiometryTest leftAudiometryA = new TonalAudiometryTest();
            TonalAudiometryTest rightAudiometryA = new TonalAudiometryTest();
            TonalAudiometryTest leftAudiometryB = new TonalAudiometryTest();
            TonalAudiometryTest rightAudiometryB = new TonalAudiometryTest();
            makeAudiometry(leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply, leftGraph, leftAudiometryA, "Left", "Air");
            makeAudiometry(rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply, rightGraph, rightAudiometryA, "Right", "Air");
            makeAudiometry(leftFreqsB, leftFreqs_dbB, leftFreqs_maskB, leftFreqs_noReplyB, leftGraph, leftAudiometryA, "Left", "Bone (mastoid)");
            makeAudiometry(rightFreqsB, rightFreqs_dbB, rightFreqs_maskB, rightFreqs_noReplyB, rightGraph, rightAudiometryA, "Right", "Bone (mastoid)");

            End.Visible = false;
            //this.Close();
        }
        private void makeAudiometry(List<double> freqs, Dictionary<double, double> dicDB, Dictionary<double, double> dicMask, Dictionary<double, bool> dicNoReply, ZedGraphControl graph, TonalAudiometryTest Audiometry, string Side, string Via)
        {
            string portugueseSide;
            string portugueseVia;

            if (Side == "Left") portugueseSide = "esquerda";
            else portugueseSide = "direita";
            if (Via == "Air") portugueseVia = "aérea";
            else portugueseVia = "óssea (mastóide)";

            if (freqs.Any())
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

                Audiometry.Via = Via;
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

                if(Via == "Air")
                {
                    LineItem audiometryCurve;
                    audiometryCurve = graph.GraphPane.AddCurve(Audiometry.AudiometryType, audiometry, Audiometry.getColor(), SymbolType.None);
                    audiometryCurve.IsX2Axis = true;
                    Audiometry.changeLine(audiometryCurve.Line);
                }
                TonalAudiometryTest.updateGraph(graph);

                vAInterface.addCompletedAudiometry(Audiometry, patientLabel.Text);

                MessageBox.Show("Audiometria da orelha " + portugueseSide + " via " + portugueseVia + " salva", "Sucesso", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Orelha " + portugueseSide + " via " + portugueseVia + " não salva", "Sucesso", MessageBoxButtons.OK);
            }
        }
        
        //Freqs, mask and db up/down
        private void freqUp_Click(object sender, EventArgs e)
        {
            freqID++;
            if (freqID > 11) freqID = 11;
            showFreq();
            if (mask) turnOffMask();
        }

        private void freqDown_Click(object sender, EventArgs e)
        {
            freqID--;
            if (freqID < 1) freqID = 1;
            showFreq();
            if (mask) turnOffMask();
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
                vAInterface.stopScene(false, true);
                PlayNoise();
            }
            else
            {
                MessageBox.Show("Ative o mascaramento para alterar a intensidade", "Erro",
                                                                    MessageBoxButtons.OK,
                                                                    MessageBoxIcon.Error);
            }
        }

        private void maskDown_Click(object sender, EventArgs e)
        {
            if (mask)
            {
                currentMask = currentMask - 5;
                if (currentMask < 0) currentMask = 0;
                vAInterface.stopScene(false, true);
                PlayNoise();
            }
            else
            {
                MessageBox.Show("Ative o mascaramento para alterar a intensidade", "Erro",
                                                                    MessageBoxButtons.OK,
                                                                    MessageBoxIcon.Error);
            }
        }
        private void showMask()
        {
            maskLabel.Text = currentMask.ToString();
            maskProgressBar.Value = Convert.ToInt32(currentMask);
            updateCorrectAnswers(correctAnswers = 0);
            currentMaskVolumePower = Math.Pow(10, currentMask / 20.0) * maskIntensitRef[freqID - 1];
        }

        private void Sound_MouseUp(object sender, MouseEventArgs e)
        {
            soundLight.BackColor = Color.Silver;
            vAInterface.stopScene(true, false);
        }

        private void Sound_MouseDown(object sender, MouseEventArgs e)
        {
            soundLight.BackColor = Color.Yellow;

            VaConfig();

            string speechFile = freq_sound[currentFrequency];
            string noiseFile = freq_noise[currentFrequency];

            if (calibratedSystem)
            {
                if (currentVolumePower < 200000)
                {
                    if (Side == "Left") vAInterface.playScene(radius, true, speechFile, currentVolumePower, 270, false, noiseFile, 0, 0); 
                    else vAInterface.playScene(radius, true, speechFile, currentVolumePower, 90, false, noiseFile, 0, 0);
                }
                else
                {
                    soundLight.BackColor = Color.Silver;
                    MessageBox.Show("Limite de resposta do audiômetro atingido", "Erro",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                }
            }
            else
            {
                if (Side == "Left") vAInterface.playScene(radius, true, speechFile, Decimal.ToDouble(volume.Value), 270, false, noiseFile, 0, 0);                
                else vAInterface.playScene(radius, true, speechFile, Decimal.ToDouble(volume.Value), 90, false, noiseFile, 0, 0);                
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
            alldBPanel.Visible = allMaskPanel.Visible = allResetFinishPanel.Visible = graphsPanel.Visible = false;
            soundDetectedPanel.Visible = onOffMaskPanel.Visible = viaPanel.Visible = false;
            freqUp.Visible = freqDown.Visible = false;

            currentdB = 0;
            freqID = 11;
            showFreq();
            showdB();

            calibrateLight.BackColor = Color.Yellow;
            MessageBox.Show("A calibração utilizará seu limiar auditivo como referência, assumindo que você tem uma audição saudável, " +
                "portanto, você precisa ajustar o volume em cada uma das frequências até a minima intensidade em que ainda seja possível ouvir.", "Calibração  1/6", MessageBoxButtons.OK);
            MessageBox.Show("Ajuste o volume do seu computador para 100.", "Calibração  2/6", MessageBoxButtons.OK);
            volume.BackColor = Color.Purple;
            MessageBox.Show("Para cada uma das frequências que serão calibradas, ajuste a potência sonora com o controle numérico \"Power\", o resultado desejado é a menor potência em que o som seja audível.", "Calibração  3/6", MessageBoxButtons.OK);
            volume.BackColor = SystemColors.Control;
            Sound.BackColor = Color.Purple;
            MessageBox.Show("O som correspondente a cada frequência, será gerado enquanto o botão \"Sound\" estiver pressionado.", "Calibração  4/6", MessageBoxButtons.OK);
            Sound.BackColor = Color.Black;
            changeSide.BackColor = Color.Purple;
            MessageBox.Show("Para melhor resultado, teste cada frenquência e cada potência em ambas as orelhas, dando prioridade para a orelha com melhor audição (a orelha que percebe o estímulo de menor potência).", "Calibração  5/6", MessageBoxButtons.OK);
            changeSide.BackColor = Color.Black;
            saveFrequency.BackColor = Color.Purple;
            MessageBox.Show("Após calibrar, salve o resultado do limiar para cada frequência clicando em \"Save\".", "Calibração  6/6", MessageBoxButtons.OK);
            saveFrequency.BackColor = Color.Black;
        }

        private void updateVolumePower()
        {
            currentVolumePower = Math.Pow(10, currentdB / 20.0) * intensitRef[freqID - 1];

            if (currentVolumePower > 200000) currentNoReply = true;
            else currentNoReply = false;
        }

        private void undo_Click(object sender, EventArgs e)
        {
            if (modifications.Any())
            {
                string lastModificationSideVia = modifications[modifications.Count() - 1];

                if (lastModificationSideVia == "Left,Air")
                {
                    undoAction(leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply, leftGraph, leftFreqsB, leftFreqs_dbB, leftFreqs_maskB, leftFreqs_noReplyB);
                }
                else if (lastModificationSideVia == "Right,Air")
                {
                    undoAction(rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply, rightGraph, rightFreqsB, rightFreqs_dbB, rightFreqs_maskB, rightFreqs_noReplyB);
                }
                else if (lastModificationSideVia == "Left,Bone (mastoid)")
                {                    
                    undoAction(leftFreqsB, leftFreqs_dbB, leftFreqs_maskB, leftFreqs_noReplyB, leftGraph, leftFreqs, leftFreqs_db, leftFreqs_mask, leftFreqs_noReply);
                }
                else if (lastModificationSideVia == "Right,Bone (mastoid)")
                {
                    undoAction(rightFreqsB, rightFreqs_dbB, rightFreqs_maskB, rightFreqs_noReplyB, rightGraph, rightFreqs, rightFreqs_db, rightFreqs_mask, rightFreqs_noReply);
                }
            }
            else 
            {
                MessageBox.Show("Realize alguma ação antes de retroceder", "Erro",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
            }
        }

        private void undoAction(List<double> freqsToClear, Dictionary<double, double> dbToClear, Dictionary<double, double> maskToClear, Dictionary<double, bool> noReplyToClear, 
            ZedGraphControl graph, List<double> freqs, Dictionary<double, double> db, Dictionary<double, double> mask, Dictionary<double, bool> noReply)
        {
            string otherVia = " ";
            string lastModificationSideVia = modifications[modifications.Count() - 1];
            string[] lastSideLastVia = lastModificationSideVia.Split(',');
            string lastModificationSide = lastSideLastVia[0];
            string lastModificationVia = lastSideLastVia[1];

            if (lastModificationVia == "Air") 
            {
                otherVia = "Bone (mastoid)";
            }
            else if (lastModificationVia == "Bone (mastoid)")
            {
                otherVia = "Air";
            }

            double item = freqsToClear[freqsToClear.Count() - 1];
            freqsToClear.Remove(item);
            dbToClear.Remove(item);
            maskToClear.Remove(item);
            noReplyToClear.Remove(item);
            modifications.RemoveAt(modifications.Count() - 1);


            graph.GraphPane.CurveList.Clear();


            int i = 0;
            double linearizedFreq = 0;
            foreach (double freq in freqsToClear)
            {
                linearizedFreq = Math.Log(freq / 125, 2) + 1;
                TonalAudiometryTest.drawSymbol(graph, linearizedFreq, dbToClear[freq], maskToClear[freq], noReplyToClear[freq], lastModificationSide, lastModificationVia);
            }
            foreach (double freq in freqs)
            {
                linearizedFreq = Math.Log(freq / 125, 2) + 1;
                TonalAudiometryTest.drawSymbol(graph, linearizedFreq, db[freq], mask[freq], noReply[freq], lastModificationSide, otherVia);
            }
          
            TonalAudiometryTest.updateGraph(graph);
            updateCorrectAnswers(correctAnswers = 0);
        }
        private void resizeScreen()
        {
            double PCResolutionWidth = Screen.PrimaryScreen.Bounds.Width;
            double PCResolutionHeight = Screen.PrimaryScreen.Bounds.Height;

            double formWidth = this.Size.Width;
            double formHeight = this.Size.Height;

            if ((formWidth > PCResolutionWidth) | (formHeight > PCResolutionHeight * 0.925))
            {
                int newWidth = Convert.ToInt32(PCResolutionWidth * 0.8);
                int newHeight = Convert.ToInt32(PCResolutionHeight * 0.875);
                this.Size = new Size(newWidth, newHeight);
            }
        }

        private void manualAudiometricTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mask)
            {
                turnOffMask(); //close form when mask noise playing exception
            }
        }
    }
}