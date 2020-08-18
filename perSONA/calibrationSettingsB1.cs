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
using ClosedXML.Excel;

namespace perSONA
{
    public partial class calibrationSettingsB1 : Form
    {
        private readonly IvAInterface vAInterface;
        string earphoneBrand;
        string earphoneModel;
        bool earphoneQuality;

        public calibrationSettingsB1(IvAInterface vAInterface, string calibrationObjectBrand, string calibrationObjectModel, bool phoneQuality)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            earphoneBrand = calibrationObjectBrand;
            earphoneModel = calibrationObjectModel;
            earphoneQuality = phoneQuality;
            fillArtifialEarBrandBox();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(artificialEarBrandBox.Text) | string.IsNullOrWhiteSpace(artificialEarModelBox.Text))
            {
                MessageBox.Show("Preencha todos os campos para continuar", "Error");
            }

            else
            {
                calibrationData calibration = new calibrationData()
                {
                    CalibrationObjectBrand = earphoneBrand,
                    CalibrationObjectModel = earphoneModel,
                    EarphoneQuality = earphoneQuality,
                    ArtificialEarBrand = artificialEarBrandBox.Text,
                    ArtificialEarModel = artificialEarModelBox.Text,
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

        private void fillArtifialEarBrandBox()
        {
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(7);

            var linha = 2;
            string previousCell = "";

            while (true)
            {
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();
                if (string.IsNullOrEmpty(BrandColumnCell)) break;
                if (previousCell != BrandColumnCell)
                {
                    artificialEarBrandBox.Items.Add(BrandColumnCell);
                }
                linha++;
                previousCell = BrandColumnCell;
            }

            wb.Dispose();
        }

        private void artificialEarBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            artificialEarModelBox.Items.Clear();
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(7);

            var linha = 2;

            while (true)
            {
                var ModelColumnCell = Table.Cell("B" + linha.ToString()).Value.ToString();
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();

                if (string.IsNullOrEmpty(BrandColumnCell)) break;

                if (artificialEarBrandBox.Text == BrandColumnCell)
                {
                    artificialEarModelBox.Items.Add(ModelColumnCell);
                }
                linha++;
            }
            wb.Dispose();
        }
    }
}
