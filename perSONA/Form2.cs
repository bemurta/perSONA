using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace perSONA
{
    public partial class Form2 : Form
    {

        private readonly IvAInterface vAInterface;

        public Form2(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
            textBox1.Text = Properties.Settings.Default.RESULTS_FOLDER;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Directory.GetCurrentDirectory();
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string selectedPath = fbd.SelectedPath.ToString();

                    textBox1.Text = selectedPath;
                    vAInterface.concatText("Changed default results folder to: " + selectedPath);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.RESULTS_FOLDER = textBox1.Text;
                Properties.Settings.Default.Save();
            }
            catch (DirectoryNotFoundException)
            {
                try
                {
                    string dir = textBox1.Text;
                    Directory.CreateDirectory(dir);
                }
                catch (Exception)
                {
                    const string message =
                   "Error ocurred. Couldn't set a results folder, reseting to default.";
                    const string caption = "Incorrect database format. Metadata required!";
                    var result = MessageBox.Show(message, caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RESULTS_FOLDER = "%DEFAULTUSERPROFILE%/perSONA";
            textBox1.Text = Properties.Settings.Default.RESULTS_FOLDER;
            Properties.Settings.Default.Save();
        }
    }
}