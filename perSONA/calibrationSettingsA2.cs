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
    public partial class calibrationSettingsA2 : Form
    {
        private readonly IvAInterface vAInterface;

        public calibrationSettingsA2(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IPhoneModelBox.Text) | string.IsNullOrWhiteSpace(IOSVersionBox.Text) | string.IsNullOrWhiteSpace(microphoneBrandBox.Text) | string.IsNullOrWhiteSpace(microphoneModelBox.Text))
            {
                MessageBox.Show("Adicione o modelo do IPhone, vesão do IOS e marca e modelo do microfone externo para continuar", "Erro");
            }
            else
            {
                calibrationData calibration = new calibrationData()
                {
                    IPhoneModel = IPhoneModelBox.Text,
                    IOSVersion = IOSVersionBox.Text,
                    MicrophoneBrand = microphoneBrandBox.Text,
                    MicrophoneModel = microphoneModelBox.Text,
                    MicrophoneSerialNumber = microphoneSerialNumberBox.Text,
                    LastCalibrationDate = lastCalibrationDateBox.Value,
                    NotCalibrate = notCalibrateCheckbox.Checked,
                    ApplicationName = applicationNameBox.Text,
                    ApplicationVersion = applicationVersionBox.Text,
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
