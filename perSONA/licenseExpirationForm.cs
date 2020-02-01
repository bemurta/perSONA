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

        private void button1_Click(object sender, EventArgs e)
        {
            CompareSerialKey();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSd7COM0ic-StEBNQFFBUqfiw6rShIg5I8GzXUFxfZgfE87z9g/viewform");
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void licenseExpirationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
