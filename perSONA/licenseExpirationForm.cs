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
    public partial class licenseExpirationForm : Form
    {
        public licenseExpirationForm()
        {
            InitializeComponent();
        }

        private void verifySerialKey_Click(object sender, EventArgs e)
        {
            CompareSerialKey();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSd7COM0ic-StEBNQFFBUqfiw6rShIg5I8GzXUFxfZgfE87z9g/viewform");
        }

        void CompareSerialKey()
        {
            String imputSerialKey;
            String correctSerialKey;
            bool demoVersion;

            demoVersion = Properties.Settings.Default.DEMO_VERSION;
            correctSerialKey = Properties.Settings.Default.SERIAL_KEY;
            imputSerialKey = textBox1.Text;

            if(imputSerialKey.Equals(correctSerialKey))
            {
                demoVersion = false;
                Properties.Settings.Default.DEMO_VERSION = demoVersion;
                new Form5().Show();
                Hide();
            }
            else 
            {
                label2.Text = "Key errada!";
            }

            Properties.Settings.Default.Save();
        }

        private void licenseExpirationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
