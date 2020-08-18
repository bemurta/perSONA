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
    public partial class calibrationSettingsB2 : Form
    {
        private readonly IvAInterface vAInterface;
        string earphoneBrand;
        string earphoneModel;
        bool earphoneQuality;

        public calibrationSettingsB2(IvAInterface vAInterface, string calibrationObjectBrand, string calibrationObjectModel, bool phoneQuality)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            earphoneBrand = calibrationObjectBrand;
            earphoneModel = calibrationObjectModel;
            earphoneQuality = phoneQuality;
            fillMannequinBrandBox();
        }

        private void mannequinBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mannequinModelBox.Items.Clear();
            mannequinPinnaeBox.Items.Clear();
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(8);

            var linha = 2;

            while (true)
            {
                var ModelColumnCell = Table.Cell("B" + linha.ToString()).Value.ToString();
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();

                if (string.IsNullOrEmpty(BrandColumnCell)) break;

                if (mannequinBrandBox.Text == BrandColumnCell)
                {
                    mannequinModelBox.Items.Add(ModelColumnCell);
                }
                linha++;
            }
            wb.Dispose();
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

            //Add pinnae
            mannequinPinnaeBox.Items.Clear();
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(9);

            var linha = 2;

            while (true)
            {
                var PinnaeColumnCell = Table.Cell("B" + linha.ToString()).Value.ToString();
                var ModelColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();

                if (string.IsNullOrEmpty(ModelColumnCell)) break;

                if (mannequinModelBox.Text == ModelColumnCell)
                {
                    mannequinPinnaeBox.Items.Add(PinnaeColumnCell);
                }
                linha++;
            }
            wb.Dispose();
            mannequinPinnaeBox.Items.Add("Sem pinna");
        }
        private void Next_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(mannequinBrandBox.Text) | string.IsNullOrWhiteSpace(mannequinModelBox.Text) | string.IsNullOrWhiteSpace(mannequinPinnaeBox.Text))
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
        private void fillMannequinBrandBox()
        {
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            var Table = wb.Worksheet(8);

            var linha = 2;
            string previousCell = "";

            while (true)
            {
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();
                if (string.IsNullOrEmpty(BrandColumnCell)) break;
                if (previousCell != BrandColumnCell)
                {
                    mannequinBrandBox.Items.Add(BrandColumnCell);
                }
                linha++;
                previousCell = BrandColumnCell;
            }

            wb.Dispose();
        }
    }
} 