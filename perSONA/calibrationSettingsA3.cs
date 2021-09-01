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
    public partial class calibrationSettingsA3 : Form
    {
        private readonly IvAInterface vAInterface;
        string speakerBrand;
        string speakerModel;

        public calibrationSettingsA3(IvAInterface vAInterface, string calibrationObjectBrand, string calibrationObjectModel)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            speakerBrand = calibrationObjectBrand;
            speakerModel = calibrationObjectModel;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SLMBrandBox.Text) | string.IsNullOrWhiteSpace(SLMModelBox.Text) | string.IsNullOrWhiteSpace(microphoneBrandBox.Text) | string.IsNullOrWhiteSpace(microphoneModelBox.Text) | string.IsNullOrWhiteSpace(soundCardBrandBox.Text) | string.IsNullOrWhiteSpace(soundCardModelBox.Text))
            {
                MessageBox.Show("Adicione a marca e modelo dos componenetes utilizados", "Erro");
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
                    MicrophoneBrand = microphoneBrandBox.Text,
                    MicrophoneModel = microphoneModelBox.Text,
                    MicrophoneSerialNumber = microphoneSerialNumberBox.Text,
                    SoundCardBrand = soundCardBrandBox.Text,
                    SoundCardModel = soundCardModelBox.Text,
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
    }
}