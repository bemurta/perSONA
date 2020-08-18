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
using ClosedXML.Excel;


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
            fillSLMBrandBox();

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

        private void fillSLMBrandBox()
        {
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(3);

            var linha = 2;
            string previousCell = "";

            while (true)
            {
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();
                if (string.IsNullOrEmpty(BrandColumnCell)) break;
                if (previousCell != BrandColumnCell)
                {
                    SLMBrandBox.Items.Add(BrandColumnCell);
                }
                linha++;
                previousCell = BrandColumnCell;
            }

            wb.Dispose();
        }


        private void SLMBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SLMModelBox.Items.Clear();
            //fillModelBox
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(3);

            var linha = 2;

            while (true)
            {
                var ModelColumnCell = Table.Cell("B" + linha.ToString()).Value.ToString();
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();

                if (string.IsNullOrEmpty(BrandColumnCell)) break;

                if (SLMBrandBox.Text == BrandColumnCell)
                {
                    SLMModelBox.Items.Add(ModelColumnCell);
                }
                linha++;
            }

            wb.Dispose();

        }
    }
}