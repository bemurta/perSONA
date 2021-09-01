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
using iTextSharp.text;
using iTextSharp.text.pdf;
using VA;

namespace perSONA
{
    public partial class speakerCalibration : Form
    {
        //VA
        private readonly IvAInterface vAInterface;
        public VANet vA { get; private set; }

        public List<Panel> speakers = new List<Panel>();
        int i = Properties.Settings.Default.ITERATOR;



        public speakerCalibration(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            volumeBar.Value = Properties.Settings.Default.SPEAKER_VOLUME;
            volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.SPEAKER_VOLUME);

            speakers.Add(panel1);
            speakers.Add(panel2);
            speakers.Add(panel3);
            speakers.Add(panel4);
            speakers.Add(panel5);
            speakers.Add(panel6);
            speakers.Add(panel7);
            speakers.Add(panel8);

            speakerLabel.BackColor = System.Drawing.Color.Yellow;

            volumeBar.Visible = false;
            volumeLabel.Visible = false;
            VolumeLabelAdjusting.Visible = true;

            if (i == 0)
            {
                foreach (Panel element in speakers)
                {
                    element.Visible = false;
                }
                speakers[0].Visible = true;
            }
            else if (i < 8)
            {
                foreach (Panel element in speakers)
                {
                    element.Visible = false;
                }
                speakers[i].Visible = true;
                speakerLabel.Text = "Caixa " + (i + 1) + "(" + (i * 45) + "°)";
            }
            else
            {
                label1.Text = "Altere o volume dos reprodutores sonoros" + "\n" + "através da barra da parte inferior da tela";
                Next.Text = "Finalizar";
                speakerLabel.Text = "Todas as Caixas";
                volumeBar.Visible = true;
                volumeLabel.Visible = true;
                VolumeLabelAdjusting.Visible = false;
            }

            if (Properties.Settings.Default.CALIBRATION_MODE == "A1")
            {
                SLM_Microphone.Image = SLM_Microphone.InitialImage; //SLM
            }
            else
            {
                SLM_Microphone.Image = SLM_Microphone.ErrorImage; //microphone
            }

        }
        private void Next_Click(object sender, EventArgs e)
        {
            if (calibrated.Checked == false)
            {
                const string message = "Clique em \"Calibrada\" antes de continuar";
                const string caption = "Erro";
                MessageBox.Show(message, caption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                if (i < 7)
                {
                    speakerLabel.Text = "Caixa " + (i + 2) + "(" + ((i + 1) * 45) + "°)";

                    //iterando sobre a lista
                    speakers[i].Visible = false;
                    speakers[(i + 1)].Visible = true;
                    i = i + 1;
                    Properties.Settings.Default.ITERATOR = i;
                }

                else if (i == 7)
                {
                    label1.Text = "Altere o volume dos reprodutores sonoros" + "\n" + "através da barra da parte inferior da tela";
                    Next.Text = "Finalizar";
                    speakerLabel.Text = "Todas as Caixas";
                    foreach (Panel element in speakers)
                    {
                        element.Visible = true;
                    }
                    i = i + 1;
                    Properties.Settings.Default.ITERATOR = i;
                    volumeBar.Visible = true;
                    volumeLabel.Visible = true;
                    VolumeLabelAdjusting.Visible = false;
                }
                else if (i == 8)
                {
                    PDF_Generate("Sucesso");
                    i = 0;
                    Properties.Settings.Default.ITERATOR = i;
                    Close();
                }
                speakerLabel.BackColor = System.Drawing.Color.Yellow;
                calibrated.Checked = false;
                Properties.Settings.Default.Save();
            }
        }

        private void calibrated_CheckedChanged(object sender, EventArgs e)
        {
            if (calibrated.Checked == false)
            {
                speakerLabel.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                speakerLabel.BackColor = System.Drawing.Color.LightGreen;
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

            if (i < 7)
            {
                vAInterface.createAcousticScene(speechFile, speechFile);
                vAInterface.playScene(1.7, (i * 45), 40);
            }
            else
            {
                vAInterface.allSoundPlayersPlayScene(1.7, 8, speechFile);
            }
        }

        static void PDF_Generate(string calibrationStatus)
        {
            //Take json data
            string jsonFile = string.Format("{0}/CalibrationData/{1}.json",
                           Properties.Settings.Default.RESULTS_FOLDER,
                           "Calibration " + Properties.Settings.Default.CALIBRATION_ID);
            var calibrationJson = File.ReadAllText(jsonFile);
            calibrationData calibration = Newtonsoft.Json.JsonConvert.DeserializeObject<calibrationData>(calibrationJson);


            //PDF
            Document doc = new Document(PageSize.A4, 70, 70, 70, 70);
            PdfWriter.GetInstance(doc, new FileStream(Properties.Settings.Default.RESULTS_FOLDER + "/Relatório de Calibração " + Properties.Settings.Default.CALIBRATION_ID + DateTime.Now.ToString(" dd-MM-yyyy") + ".pdf", FileMode.Create));

            doc.Open();

            //Fonts
            var H1 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 26);
            var H2 = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var H2bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            Paragraph title = new Paragraph();
            title.Add(new Chunk("Relatório de Calibração perSONA" + "\r\n" + "\r\n" + "\r\n", H1));
            title.Alignment = Element.ALIGN_CENTER;


            //contruict the Paragraph
            Paragraph p1 = new Paragraph();
            Paragraph p2 = new Paragraph();
            Paragraph p3 = new Paragraph();
            Paragraph p4 = new Paragraph();
            Paragraph p5 = new Paragraph();
            Paragraph p6 = new Paragraph();
            Paragraph p7 = new Paragraph();
            Paragraph p8 = new Paragraph();
            Paragraph p9 = new Paragraph();

            int p = 0;
            List<Paragraph> paragraph = new List<Paragraph>();
            paragraph.Add(p1);
            paragraph.Add(p2);
            paragraph.Add(p3);
            paragraph.Add(p4);
            paragraph.Add(p5);
            paragraph.Add(p6);
            paragraph.Add(p7);
            paragraph.Add(p8);
            paragraph.Add(p9);

            paragraph[p].Add(new Chunk("Objeto de calibração: ", H2bold));
            paragraph[p].Add(new Chunk("Sistema de reprodução sonora do perSONA, composto por arranjo de 8 alto-falantes da marca " + calibration.CalibrationObjectBrand + " e modelo " + calibration.CalibrationObjectModel + "\r\n" + "\r\n", H2));
            p++; //New paragraph

            paragraph[p].Add(new Chunk("Sinal de calibração: ", H2bold));
            paragraph[p].Add(new Chunk("Ruído de banda estreita centralizado em 1000 Hz, com NPS de 94 dB, medido no centro do arranjo" + "\r\n" + "\r\n", H2));
            p++;

            //SLM
            if (Properties.Settings.Default.CALIBRATION_MODE == "A1")
            {
                paragraph[p].Add(new Chunk("Medidor utilizado na calibração: ", H2bold));
                paragraph[p].Add(new Chunk("MNPS da marca " + calibration.SLMBrand + " e modelo " + calibration.SLMModel + "\r\n" + "\r\n", H2));
                p++;

                paragraph[p].Add(new Chunk("Número de série MNPS: ", H2bold));
                paragraph[p].Add(new Chunk(calibration.SLMSerialNumber, H2));
                p++;

                paragraph[p].Add(new Chunk("Data da última calibração do MNPS: ", H2bold));

                if (calibration.NotCalibrate)
                {
                    paragraph[p].Add(new Chunk("Nunca Calibrado", H2));
                    p++;
                }
                else
                {
                    paragraph[p].Add(new Chunk(calibration.LastCalibrationDate.ToString(), H2));
                    p++;

                    paragraph[p].Add(new Chunk("Certificado de calibração do MNPS:  ", H2bold));
                    paragraph[p].Add(new Chunk(calibration.SLMCalibrationNumber + "\r\n" + "\r\n", H2));
                    p++;
                }
            }


            //IPhone
            if (Properties.Settings.Default.CALIBRATION_MODE == "A2")
            {
                paragraph[p].Add(new Chunk("Sistema utilizado na calibração: ", H2bold));
                paragraph[p].Add(new Chunk(string.Format("{0} com sistema operacional {1}, com aplicativo {2} instalado na versão {3}, juntamente com o microfone {4} {5} com numero de série {6}",
                                        calibration.IPhoneModel, calibration.IOSVersion, calibration.ApplicationName, calibration.ApplicationVersion, calibration.MicrophoneBrand, calibration.MicrophoneModel, calibration.MicrophoneSerialNumber) + "\r\n" + "\r\n", H2));
                p++;

                paragraph[p].Add(new Chunk("Data da última calibração do microfone: ", H2bold));

                if (calibration.NotCalibrate)
                {
                    paragraph[p].Add(new Chunk("Nunca Calibrado" + "\r\n" + "\r\n", H2));
                    p++;
                }
                else
                {
                    paragraph[p].Add(new Chunk(calibration.LastCalibrationDate.ToString() + "\r\n" + "\r\n", H2));
                    p++;
                }
            }

            //Sound Card System
            if (Properties.Settings.Default.CALIBRATION_MODE == "A3")
            {
                paragraph[p].Add(new Chunk("Sistema utilizado na calibração: ", H2bold));
                paragraph[p].Add(new Chunk(string.Format("Calibrador sonoro {0} {1} {2}, auxiliado de um microfone {3} {4} {5} e uma placa de som {6} {7}",
                                        calibration.SLMBrand, calibration.SLMModel, calibration.SLMSerialNumber, calibration.MicrophoneBrand, calibration.MicrophoneModel, calibration.MicrophoneSerialNumber, calibration.SoundCardBrand, calibration.SoundCardModel) + "\r\n" + "\r\n", H2));
                p++;
            }



            paragraph[p].Add(new Chunk("Status da calibração do sistema: ", H2bold));
            paragraph[p].Add(new Chunk(calibrationStatus, H2));
            p++;

            paragraph[p].Add(new Chunk("Data e hora da calibração do sistema: ", H2bold));
            paragraph[p].Add(new Chunk(calibration.CalibrationDateTime.ToString(), H2));


            doc.Add(title);
            doc.Add(p1);
            doc.Add(p2);
            doc.Add(p3);
            doc.Add(p4);
            doc.Add(p5);
            doc.Add(p6);
            doc.Add(p7);
            doc.Add(p8);
            doc.Add(p9);
            
            
            //Image
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("data/Logo_Large.png");
            image.SetAbsolutePosition(492, 0);
            image.ScaleAbsoluteHeight(60);
            image.ScaleAbsoluteWidth(100);
            doc.Add(image);

            doc.Close();


            Properties.Settings.Default.CALIBRATION_ID = Properties.Settings.Default.CALIBRATION_ID + 1;
            Properties.Settings.Default.Save();



        }

        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.SPEAKER_VOLUME = volumeBar.Value;
            volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.SPEAKER_VOLUME);
            Properties.Settings.Default.Save();
        }
    }
}