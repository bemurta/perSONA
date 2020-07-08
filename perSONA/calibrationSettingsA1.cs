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


namespace perSONA
{
    public partial class calibrationSettingsA1 : Form
    {
        private readonly IvAInterface vAInterface;
        string speakerBrand;
        string speakerModel;

        public calibrationSettingsA1(IvAInterface vAInterface, string calibrationObjectBrand, string calibrationObjectModel)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            speakerBrand = calibrationObjectBrand;
            speakerModel = calibrationObjectModel;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SLMBrandBox.Text) | string.IsNullOrWhiteSpace(SLMModelBox.Text))
            {
                MessageBox.Show("Adicione a marca e modelo do medidor de nível de pressão sonora para continuar", "Error");
            }

            else
            {
                calibrationData calibration = new calibrationData()
                {
                    CalibrationObjectBrand = speakerBrand,
                    CalibrationObjectModel = speakerModel,
                    SLMBrand = SLMBrandBox.Text,
                    SLMModel = SLMModelBox.Text,
                    SLMSerialNumber = SLMSerialNumberBox.Text,
                    LastCalibrationDate = lastCalibrationDateBox.Value,
                    NotCalibrate = notCalibrateCheckbox.Checked,
                    SLMCalibrationNumber = SLMCalibrationNumberBox.Text,
                    CalibrationDateTime = DateTime.Now
                };
                string calibrationJson = Newtonsoft.Json.JsonConvert.SerializeObject(calibration);


                try
                {
                    File.WriteAllText(string.Format("{0}/CalibrationData/{1}.json",
                                                  Properties.Settings.Default.RESULTS_FOLDER,
                                                  "Calibration " + Properties.Settings.Default.CALIBRATION_ID), calibrationJson);

                }
                catch (DirectoryNotFoundException)
                {
                    string dir = string.Format("{0}/CalibrationData", Properties.Settings.Default.RESULTS_FOLDER);
                    Directory.CreateDirectory(dir);
                    File.WriteAllText(string.Format("{0}/CalibrationData/{1}.json",
                                        Properties.Settings.Default.RESULTS_FOLDER,
                                        "Calibration " + Properties.Settings.Default.CALIBRATION_ID), calibrationJson);
                }
                new calibrationHelp(vAInterface).Show();
                Close();

            }
        }

        private void SLMBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SLMModelBox.Items.Clear();
            if (SLMBrandBox.Text == "01 dB")
            {
                SLMModelBox.Items.Add("Concerto 1ch");
                SLMModelBox.Items.Add("CUBE");
                SLMModelBox.Items.Add("dB4 (4 canais)");
                SLMModelBox.Items.Add("DUO");
                SLMModelBox.Items.Add("Fusion 3G");
                SLMModelBox.Items.Add("Fusion SLM");
                SLMModelBox.Items.Add("Harmonie 4ch");
                SLMModelBox.Items.Add("Net dB 4-12ch");
                SLMModelBox.Items.Add("SdB");
                SLMModelBox.Items.Add("SdB+");
                SLMModelBox.Items.Add("SIP95");
                SLMModelBox.Items.Add("SLS95");
                SLMModelBox.Items.Add("Solo SLM");
                SLMModelBox.Items.Add("Solo(grey / blue / black)");
                SLMModelBox.Items.Add("Symphonie 2ch");
            }
            else if (SLMBrandBox.Text == "Brüel and Kjaer")
            {
                SLMModelBox.Items.Add("2236");
                SLMModelBox.Items.Add("2237");
                SLMModelBox.Items.Add("2238");
                SLMModelBox.Items.Add("2239");
                SLMModelBox.Items.Add("2240");
                SLMModelBox.Items.Add("2245");
                SLMModelBox.Items.Add("2250");
                SLMModelBox.Items.Add("2250-L");
                SLMModelBox.Items.Add("2260");
                SLMModelBox.Items.Add("2270");
            }

            else if (SLMBrandBox.Text == "CESVA")
            {
                SLMModelBox.Items.Add("SC101");
                SLMModelBox.Items.Add("SC102");
                SLMModelBox.Items.Add("SC160");
                SLMModelBox.Items.Add("SC260");
                SLMModelBox.Items.Add("SC310");
                SLMModelBox.Items.Add("SC420");
            }
            else if (SLMBrandBox.Text == "Criffer")
            {
                SLMModelBox.Items.Add("Octava-Plus");
            }
            else if (SLMBrandBox.Text == "Instrutherm")
            {
                SLMModelBox.Items.Add("Dec - 5020");
                SLMModelBox.Items.Add("Dec - 6000");
            }
            else if (SLMBrandBox.Text == "Larson Davis")
            {
                SLMModelBox.Items.Add("80");
                SLMModelBox.Items.Add("81");
                SLMModelBox.Items.Add("82");
                SLMModelBox.Items.Add("83");
                SLMModelBox.Items.Add("712");
                SLMModelBox.Items.Add("812");
                SLMModelBox.Items.Add("814");
                SLMModelBox.Items.Add("820");
                SLMModelBox.Items.Add("824");
                SLMModelBox.Items.Add("831");
                SLMModelBox.Items.Add("831C");
                SLMModelBox.Items.Add("831-LOWN");
                SLMModelBox.Items.Add("LXT1-QPR");
                SLMModelBox.Items.Add("SoundExpert LXT");
                SLMModelBox.Items.Add("SoundTrack LXT");
            }
            else if (SLMBrandBox.Text == "Minipa")
            {
            }
            else if (SLMBrandBox.Text == "Norsonic")
            {
            }
            else if (SLMBrandBox.Text == "NTI")
            {
                SLMModelBox.Items.Add("XL2");
            }
        }
    }
}