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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;

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

            fillSoundPlayerBrandBox();
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                EquipmentsBox.Items.Add("Orelha artificial");
                EquipmentsBox.Items.Add("Manequim");
                phoneQualityCheckBox.ForeColor = Color.Red;
            }
            else
            {
                EquipmentsBox.Items.Add("Medidor de nível de pressão sonora (MNPS)");
                EquipmentsBox.Items.Add("iPhone com aplicativo e sistema de microfones externos");
                EquipmentsBox.Items.Add("Sistema de microfones, calibrador sonoro e placa de som");
                phoneQualityCheckBox.Visible = false;
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
                    new calibrationSettingsB1(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text, phoneQualityCheckBox.Checked).Show();
                }
                else
                {
                    Properties.Settings.Default.CALIBRATION_MODE = "B2";
                    new calibrationSettingsB2(vAInterface, CalibrationObjectBrandBox.Text, CalibrationObjectModelBox.Text, phoneQualityCheckBox.Checked).Show();
                }
                Properties.Settings.Default.Save();
                Close();
            }
        }

        private void CalibrationObjectBrandBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            phoneQualityCheckBox.Checked = false;
            CalibrationObjectModelBox.Text = "";
            CalibrationObjectModelBox.Items.Clear();

            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);

            var Table = wb.Worksheet(2);
            //if earphone, change table
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                Table = wb.Worksheet(1);
            }

            var linha = 2;

            while (true)
            {
                var ModelColumnCell = Table.Cell("B" + linha.ToString()).Value.ToString();
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();

                if (string.IsNullOrEmpty(BrandColumnCell)) break;

                if (CalibrationObjectBrandBox.Text == BrandColumnCell)
                {
                    CalibrationObjectModelBox.Items.Add(ModelColumnCell);
                }
                linha++;
            }
            wb.Dispose();
        }

        private void fillSoundPlayerBrandBox()
        {
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);
            
            var Table = wb.Worksheet(2);
            //if earphone, change table
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                Table = wb.Worksheet(1);
            }
            var linha = 2;
            string previousCell = "";

            while (true)
            {
                var BrandColumnCell = Table.Cell("A" + linha.ToString()).Value.ToString();
                if (string.IsNullOrEmpty(BrandColumnCell)) break;
                if (previousCell != BrandColumnCell)
                {
                    CalibrationObjectBrandBox.Items.Add(BrandColumnCell);
                }
                linha++;
                previousCell = BrandColumnCell;
            }
            wb.Dispose();
        }

        private void CalibrationObjectModelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var wb = new XLWorkbook(Properties.Settings.Default.EQUIPMENTS_TABLE_LOCATION);

            var Table = wb.Worksheet(1);
            var linha = 2;

            while (true)
            {
                var ModelColumnCell = Table.Cell("B" + linha.ToString()).Value.ToString();
                if (string.IsNullOrEmpty(ModelColumnCell)) break;
                if (CalibrationObjectModelBox.Text == ModelColumnCell)
                {
                    phoneQualityCheckBox.Checked = true;
                }
                linha++;
            }
            wb.Dispose();
        }

        private void phoneQualityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (phoneQualityCheckBox.Checked == true)
            {
                phoneQualityCheckBox.ForeColor = Color.Green;
            }
            else
            {
                phoneQualityCheckBox.ForeColor = Color.Red;
            }
        }
    }
}