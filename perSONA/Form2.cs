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
        bool firstUse;
        DateTime firstUseData;

        public Form2()
        {
            InitializeComponent();
            firstUse = Properties.Settings.Default.FIRST_USE;
            textBox1.Text = Properties.Settings.Default.RESULTS_FOLDER;
            if (firstUse) 
            {
                const string message = "Bem-Vindo ao software perSONA. Antes de começar selecione a pasta que deseja salvar os resultados";
                const string caption = "Bem-Vindo ao perSONA";
                var result = MessageBox.Show(message, caption,
                    MessageBoxButtons.OK);
            }
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

            if (firstUse == false) 
            {
                Close();
            }
            else
            {
                firstUse = false;
                firstUseData = DateTime.Now;
                Properties.Settings.Default.FIRST_USE = firstUse;
                Properties.Settings.Default.FIRST_USE_DATA = firstUseData;
                Properties.Settings.Default.Save();
                new Form5().Show();
                Hide();
            }
        }
    }
}