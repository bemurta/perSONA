using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace perSONA
{
    public partial class calibrationSettingsB2 : Form
    {
        private readonly IvAInterface vAInterface;
        string earphoneBrand;
        string earphoneModel;

        public calibrationSettingsB2(IvAInterface vAInterface, string calibrationObjectBrand, string calibrationObjectModel)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            earphoneBrand = calibrationObjectBrand;
            earphoneModel = calibrationObjectModel;
        }

        private void mannequinBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            mannequinModelBox.Items.Clear();
            mannequinPinnaeBox.Items.Clear();
            if (mannequinBrandBox.Text == "01 dB")
            {
                //Model
                mannequinModelBox.Items.Add("Cortex MK I");
                mannequinModelBox.Items.Add("Cortex MK II");

                mannequinPinnaeBox.Items.Add("Sem Pinna");
            }
            else if (mannequinBrandBox.Text == "Bruel and Kjaer")
            {
                //model
                mannequinModelBox.Items.Add("4100");
                mannequinModelBox.Items.Add("4128-C");
                mannequinModelBox.Items.Add("4128-D");
                mannequinModelBox.Items.Add("5128");

                //Pinnae
                mannequinPinnaeBox.Items.Add("DZ-9769");
                mannequinPinnaeBox.Items.Add("DZ-9770");
                mannequinPinnaeBox.Items.Add("DZ-9773");
                mannequinPinnaeBox.Items.Add("DZ-9774");
                mannequinPinnaeBox.Items.Add("Sem Pinna");
            }
            else if (mannequinBrandBox.Text == "Neumann")
            {
                mannequinModelBox.Items.Add("KU-100");

                mannequinPinnaeBox.Items.Add("Sem Pinna");
            }
        }

        private void mannequinModelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //mannequinPicture.Image = Image.FromFile("C:\\Users\\Gustavo Trentin\\Desktop\\perSONA\\perSONA\\logo\\mannequin" + mannequinModelBox.Text + ".png");
                mannequinPicture.Image = Image.FromFile("logo/mannequin/" + mannequinModelBox.Text + ".png");
                //mannequinPicture.ImageLocation = "logo/mannequin/" + mannequinModelBox.Text + ".png";
            }
            catch(FileNotFoundException)
            {
                mannequinPicture.Image = null;
            }
        }
        private void Next_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(mannequinBrandBox.Text) | string.IsNullOrWhiteSpace(mannequinModelBox.Text))
            {
                MessageBox.Show("Adicione a marca e modelo do manequim para continuar", "Error");
            }

            else
            {
                calibrationData calibration = new calibrationData()
                {
                    CalibrationObjectBrand = earphoneBrand,
                    CalibrationObjectModel = earphoneModel,
                    MannequinBrand = mannequinBrandBox.Text,
                    MannequinModel = mannequinModelBox.Text,
                    MannequinPinnae = mannequinPinnaeBox.Text,
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