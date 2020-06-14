using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                SpeakerPanel.Visible = false;
                EarphonePanel.Visible = true;
            }
            else
            {
                SpeakerPanel.Visible = true;
                EarphonePanel.Visible = false;
            }
        }

        private void A1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CALIBRATION_MODE = "A1";
            Properties.Settings.Default.Save();
            new calibrationSettingsA1(vAInterface).Show();
            Close();
        }

        private void A2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CALIBRATION_MODE = "A2";
            Properties.Settings.Default.Save();
            new calibrationSettingsA2(vAInterface).Show();
            Close();
        }

        private void A3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CALIBRATION_MODE = "A3";
            Properties.Settings.Default.Save();
            new calibrationSettingsA3(vAInterface).Show();
            Close();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CALIBRATION_MODE = "B1";
            Properties.Settings.Default.Save();
            new calibrationSettingsB1(vAInterface).Show();
            Close();
        }

        private void B2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CALIBRATION_MODE = "B2";
            Properties.Settings.Default.Save();
            new calibrationSettingsB2(vAInterface).Show();
            Close();
        }
    }
}