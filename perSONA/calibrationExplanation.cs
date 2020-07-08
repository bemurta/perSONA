using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace perSONA
{
    public partial class calibrationExplanation : Form
    {
        private readonly IvAInterface vAInterface;

        public calibrationExplanation(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            CalibrationObjectBrandBox.Items.Clear();
            CalibrationObjectModelBox.Items.Clear();
            EquipmentsBox.Items.Clear();

            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                Brand.Text = "Marca dos Fones de Ouvido";
                Model.Text = "Modelo dos Fones de Ouvido";
                //Brand
                CalibrationObjectBrandBox.Items.Add("3M");
                CalibrationObjectBrandBox.Items.Add("Beyer(dynamic)");
                CalibrationObjectBrandBox.Items.Add("BHM");
                CalibrationObjectBrandBox.Items.Add("Etymotic");
                CalibrationObjectBrandBox.Items.Add("Holmco");
                CalibrationObjectBrandBox.Items.Add("KLH");
                CalibrationObjectBrandBox.Items.Add("Koss");
                CalibrationObjectBrandBox.Items.Add("Maico");
                CalibrationObjectBrandBox.Items.Add("Otometrics");
                CalibrationObjectBrandBox.Items.Add("Radioear");
                CalibrationObjectBrandBox.Items.Add("Sennheiser");
                CalibrationObjectBrandBox.Items.Add("Telephonics");
          
                //Equipments
                EquipmentsBox.Items.Add("Orelha artificial");
                EquipmentsBox.Items.Add("Manequim");
            }
            else
            {
                Brand.Text = "Marca das Caixas de Som";
                Model.Text = "Modelo das Caixas de Som";
                //Brand
                CalibrationObjectBrandBox.Items.Add("Genelec");

                //Equipments
                EquipmentsBox.Items.Add("Medidor de nível de pressão sonora (MNPS)");
                EquipmentsBox.Items.Add("iPhone com aplicativo e sistema de microfones externos");
                EquipmentsBox.Items.Add("Sistema de microfones, calibrador sonoro e placa de som");
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CalibrationObjectBrandBox.Text) | string.IsNullOrWhiteSpace(CalibrationObjectModelBox.Text) | string.IsNullOrWhiteSpace(EquipmentsBox.Text))
            {
                MessageBox.Show("Preencha todos os campos do formulário para prosseguir", "Error");
            }
            else
            {
                //Save_Data();
                if (EquipmentsBox.Text == "Medidor de nível de pressão sonora (MNPS)")
                {
                    Properties.Settings.Default.CALIBRATION_MODE = "A1";
                    new calibrationSettingsA1(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text).Show();
                }
                else if (EquipmentsBox.Text == "iPhone com aplicativo e sistema de microfones externos")
                {
                    Properties.Settings.Default.CALIBRATION_MODE = "A2";
                    new calibrationSettingsA2(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text).Show();
                }
                else if (EquipmentsBox.Text == "Sistema de microfones, calibrador sonoro e placa de som")
                {
                    Properties.Settings.Default.CALIBRATION_MODE = "A3";
                    new calibrationSettingsA3(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text).Show();
                }
                else if (EquipmentsBox.Text == "Orelha artificial")
                {
                    Properties.Settings.Default.CALIBRATION_MODE = "B1";
                    new calibrationSettingsB1(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text).Show();
                }
                else
                {
                    Properties.Settings.Default.CALIBRATION_MODE = "B2";
                    new calibrationSettingsB2(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text).Show();
                }
                Properties.Settings.Default.Save();
                Close();
            }
        }

        private void CalibrationObjectBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalibrationObjectModelBox.Items.Clear();
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                Earphone_Models();
            }
            else
            {
                Speaker_Models();
            }
        }

        private void Earphone_Models() 
        {
            if (CalibrationObjectBrandBox.Text == "3M")
            {
                CalibrationObjectModelBox.Items.Add("Ear Tone 3A");
                CalibrationObjectModelBox.Items.Add("Ear Tone 5A");
            }
            else if (CalibrationObjectBrandBox.Text == "Beyer(dynamic)")
            {
                CalibrationObjectModelBox.Items.Add("AT1350");
                CalibrationObjectModelBox.Items.Add("DT 48A");
                CalibrationObjectModelBox.Items.Add("T50p");
                CalibrationObjectModelBox.Items.Add("T51");
            }
            else if (CalibrationObjectBrandBox.Text == "BHM")
            {
                CalibrationObjectModelBox.Items.Add("BC2");
            }
            else if (CalibrationObjectBrandBox.Text == "Etymotic")
            {
                CalibrationObjectModelBox.Items.Add("ER1");
                CalibrationObjectModelBox.Items.Add("ER1-02");
                CalibrationObjectModelBox.Items.Add("ER2");
                CalibrationObjectModelBox.Items.Add("ER3");
                CalibrationObjectModelBox.Items.Add("ER3C");
                CalibrationObjectModelBox.Items.Add("ER3-04");
                CalibrationObjectModelBox.Items.Add("ER3-06");
                CalibrationObjectModelBox.Items.Add("ER3-21");
                CalibrationObjectModelBox.Items.Add("ER-7");
            }
            else if (CalibrationObjectBrandBox.Text == "Holmco")
            {
                CalibrationObjectModelBox.Items.Add("PD81");
                CalibrationObjectModelBox.Items.Add("PD95");
            }
            else if (CalibrationObjectBrandBox.Text == "KLH")
            {
                CalibrationObjectModelBox.Items.Add("KLH96");
            }
            else if (CalibrationObjectBrandBox.Text == "Koss")
            {
                CalibrationObjectModelBox.Items.Add("Pró");
                CalibrationObjectModelBox.Items.Add("R80");
            }
            else if (CalibrationObjectBrandBox.Text == "Maico")
            {
                CalibrationObjectModelBox.Items.Add("MA25");
            }
            else if (CalibrationObjectBrandBox.Text == "Otometrics")
            {
                CalibrationObjectModelBox.Items.Add("NB71");
            }
            else if (CalibrationObjectBrandBox.Text == "Radioear")
            {
                CalibrationObjectModelBox.Items.Add("IP30");
                CalibrationObjectModelBox.Items.Add("DD45");
                CalibrationObjectModelBox.Items.Add("DD45C");
                CalibrationObjectModelBox.Items.Add("DD450");
                CalibrationObjectModelBox.Items.Add("B71");
                CalibrationObjectModelBox.Items.Add("B81");
            }
            else if (CalibrationObjectBrandBox.Text == "Sennheiser")
            {
                CalibrationObjectModelBox.Items.Add("HDA200");
                CalibrationObjectModelBox.Items.Add("HDA280");
                CalibrationObjectModelBox.Items.Add("HDA300");
            }
            else if (CalibrationObjectBrandBox.Text == "Telephonics")
            {
                CalibrationObjectModelBox.Items.Add("TDH-39");
                CalibrationObjectModelBox.Items.Add("TDH-40");
                CalibrationObjectModelBox.Items.Add("TDH-49P");
                CalibrationObjectModelBox.Items.Add("TDH-50P");
            }
        }

        private void Speaker_Models()
        {
            if (CalibrationObjectBrandBox.Text == "Genelec")
            {
                CalibrationObjectModelBox.Items.Add("8020C");
            }

        }
    }
}