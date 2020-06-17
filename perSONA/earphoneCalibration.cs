using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using VA;

namespace perSONA
{
    public partial class earphoneCalibration : Form
    {
        //VA
        private readonly IvAInterface vAInterface;
        public VANet vA { get; private set; }

        public List<Panel> earphone = new List<Panel>();
        int i = Properties.Settings.Default.ITERATOR;



        public earphoneCalibration(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;

            earphone.Add(panel1);
            earphone.Add(panel2);

            earphoneLabel.BackColor = System.Drawing.Color.Yellow;

            if (i == 0)
            {
                foreach (Panel element in earphone)
                {
                    element.Visible = false;
                }
                earphone[0].Visible = true;
                earphoneLabel.Text = "Fone Direito";
            }
            else if (i == 1)
            {
                foreach (Panel element in earphone)
                {
                    element.Visible = false;
                }
                earphone[1].Visible = true;
                earphoneLabel.Text = "Fone Esquerdo";
            }
            else
            {
                label1.Text = "Calibre o Fone de Ouvido:";
                Next.Text = "Finalizar";
                earphoneLabel.Text = "Fones de Ouvido";
            }
        }
        private void Next_Click(object sender, EventArgs e)
        {
            if (calibrated.Checked == false)
            {
                const string message = "Clique em \"Calibrado\" antes de continuar";
                const string caption = "Erro";
                MessageBox.Show(message, caption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                if (i == 0)
                {
                    earphoneLabel.Text = "Fone Esquerdo";
                    earphone[0].Visible = false;
                    earphone[1].Visible = true;

                    i = i + 1;
                    Properties.Settings.Default.ITERATOR = i;
                }

                else if (i == 1)
                {
                    label1.Text = "Calibre o fone de ouvido:";
                    Next.Text = "Finalizar";
                    earphoneLabel.Text = "Fone de Ouvido";
                    foreach (Panel element in earphone)
                    {
                        element.Visible = true;
                    }
                    i = i + 1;
                    Properties.Settings.Default.ITERATOR = i;
                }
                else if (i == 2)
                {
                    PDF_Generate("Sucesso");
                    i = 0;
                    Properties.Settings.Default.ITERATOR = i;
                    Close();
                }
                earphoneLabel.BackColor = System.Drawing.Color.Yellow;
                calibrated.Checked = false;
                Properties.Settings.Default.Save();
            }
        }


        private void calibrated_CheckedChanged(object sender, EventArgs e)
        {
            if (calibrated.Checked == false)
            {
                earphoneLabel.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                earphoneLabel.BackColor = System.Drawing.Color.LightGreen;
            }
        }


        private void Fail_Click(object sender, EventArgs e)
        {
            PDF_Generate("Falha");
            i = 0;
            Properties.Settings.Default.ITERATOR = i;
            Close();
        }

        private void Help_Click(object sender, EventArgs e)
        {
            new calibrationHelp(vAInterface).Show();
            Hide();
        }


        private void Sound_Click(object sender, EventArgs e)
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

            vA.SetSoundReceiverPosition(receiverId, receiverPosition);      //this receiver have position (xSides, yHeight, zFront)
            vA.SetSoundReceiverOrientationVU(receiverId, receiverOrientationV, receiverOrientationU); //this receive look ahead with the top of the head up
            vAInterface.concatText(string.Format("Receiver: {3} at position: {0},{1},{2}, looking forward ", xSides, zFront, yHeight, receiverId));

            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);

            string speechFile = "data/Sounds/BandPassLimitedWhiteNoise.wav";
            vAInterface.concatText(speechFile);
            vAInterface.concatText(string.Format("Calibration sound angle: {0}", (90 + (i * 180))));
            vAInterface.createAcousticScene(speechFile, speechFile);


            if (i < 2)
            {
                vAInterface.playScene(1.2 , (90 + (i * 180)), 40);
            }
            else 
            {
                vAInterface.allSoundPlayersPlayScene(1.2, 2);
            }
        }

        static void PDF_Generate(string calibrationStatus)
        {
            /* 
            //Take json data
            string jsonFile = string.Format("{0}/CalibrationData/{1}.json",
                           Properties.Settings.Default.RESULTS_FOLDER,
                           "Calibration " + Properties.Settings.Default.CALIBRATION_ID);
            var calibrationJson = File.ReadAllText(jsonFile);
            calibrationData calibration = Newtonsoft.Json.JsonConvert.DeserializeObject<calibrationData>(calibrationJson);
            */
            //Generate PDF
        }
    }
}