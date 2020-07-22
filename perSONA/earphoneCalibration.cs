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

            volumeBar.Visible = false;
            volumeLabel.Visible = false;
            PhoneBalanceAdjusting.Visible = true;

            volumeBar.Value = Properties.Settings.Default.EARPHONE_VOLUME;
            volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.EARPHONE_VOLUME);


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
                volumeBar.Visible = true;
                volumeLabel.Visible = true;
                PhoneBalanceAdjusting.Visible = false;
                label1.Text = "Para calibrar altere o volume do fone de ouvido" + "\n" + "através da barra da parte inferior da tela";
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
                    volumeBar.Visible = true;
                    volumeLabel.Visible = true;
                    PhoneBalanceAdjusting.Visible = false;
                    label1.Text = "Para calibrar altere o volume do fone de ouvido" + "\n" + "através da barra da parte inferior da tela";
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

            if (i < 2)
            {
                vAInterface.createAcousticScene(speechFile, speechFile);
                vAInterface.playScene(1.7, (90 + (i * 180)), 40);
            }
            else 
            {
                vAInterface.allSoundPlayersPlayScene(1.7, 2, speechFile);
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
            if (calibration.EarphoneQuality == true)
            {
                paragraph[p].Add(new Chunk("Sistema de reprodução sonora do perSONA, composto por fones de ouvido da marca " + calibration.CalibrationObjectBrand + " e modelo " + calibration.CalibrationObjectModel + ", desenvolvido para exames audiológicos" + "\r\n" + "\r\n", H2));
            }
            else 
            {
                paragraph[p].Add(new Chunk("Sistema de reprodução sonora do perSONA, composto por fones de ouvido da marca " + calibration.CalibrationObjectBrand + " e modelo " + calibration.CalibrationObjectModel + ", não desenvolvido para exames audiológicos" + "\r\n" + "\r\n", H2));
            }
            p++; //New paragraph

            paragraph[p].Add(new Chunk("Sinal de calibração: ", H2bold));
            paragraph[p].Add(new Chunk("Ruído de banda estreita centralizado em 1000 Hz, com NPS de 94 dB" + "\r\n" + "\r\n", H2));
            p++;

            //Mannequin
            if (Properties.Settings.Default.CALIBRATION_MODE == "B2")
            {
                paragraph[p].Add(new Chunk("Medidor utilizado na calibração: ", H2bold));
                paragraph[p].Add(new Chunk("Manequim da marca " + calibration.MannequinBrand + " e modelo " + calibration.MannequinModel + "\r\n" + "\r\n", H2));
                p++;

                paragraph[p].Add(new Chunk("Pinna do manequin: ", H2bold));
                paragraph[p].Add(new Chunk(calibration.MannequinPinnae, H2));
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
            /* 
            //Image
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("C:/Users/Gustavo Trentin/Desktop/Fotos/Gustavo.jpeg");
            image.SetAbsolutePosition(90,90);
            image.ScaleAbsoluteHeight(20);
            image.ScaleAbsoluteWidth(90);
            doc.Add(image);
            */

            doc.Close();


            Properties.Settings.Default.CALIBRATION_ID = Properties.Settings.Default.CALIBRATION_ID + 1;
            Properties.Settings.Default.Save();
        }
        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.EARPHONE_VOLUME = volumeBar.Value;
            volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.EARPHONE_VOLUME);
            Properties.Settings.Default.Save();
        }

        private void PhoneBalanceAdjusting_Click(object sender, EventArgs e)
        {
             const string message = "Clique em \"tecla do windows + R\" ->" + "\n" + "Digite \"mmsys.cpl\" (sem aspas) -> \"Ok\" -> Dê um duplo clique em \"Fones de ouvido\" -> \"Níveis\" -> \"Balanço\"";
            const string caption = "Como ajustar o balanço dos fones de ouvido?";
            var result = MessageBox.Show(message, caption,
                MessageBoxButtons.OK);
        }
    }
}