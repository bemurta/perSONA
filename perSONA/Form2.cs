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
        private readonly IvAInterface vAInterface;

        public Form2()
        {
            InitializeComponent();
            firstUse = Properties.Settings.Default.FIRST_USE;
            textBox1.Text = Properties.Settings.Default.RESULTS_FOLDER;

            const string message = "Bem-Vindo ao software perSONA. Antes de começar selecione a pasta que deseja salvar os resultados";
            const string caption = "Bem-Vindo ao perSONA";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

        public Form2(IvAInterface ivAInterface)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
            firstUse = Properties.Settings.Default.FIRST_USE;
            textBox1.Text = Properties.Settings.Default.RESULTS_FOLDER;
        }


        private void SelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DialogResult result = fbd.ShowDialog();

                vAInterface.concatText(fbd.SelectedPath.ToString());
                

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string selectedPath = fbd.SelectedPath.ToString();
                    textBox1.Text = selectedPath;
                }
            }
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RESULTS_FOLDER = textBox1.Text;
            Properties.Settings.Default.Save();
            if(Directory.Exists(Properties.Settings.Default.RESULTS_FOLDER))
            {
                firstUseCheck();
            }
            else
            {
                const string message = "Não foi possível selecionar a pasta destino de resultados especificada.";
                const string caption = "Erro";
                var result = MessageBox.Show(message, caption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
            }
        }

        private void crateDocsFolderButton_Click(object sender, EventArgs e)
        {
            string resultsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Resultados perSONA";
            if (!Directory.Exists(resultsFolder));
            {
                Directory.CreateDirectory(resultsFolder);
            }

            Properties.Settings.Default.RESULTS_FOLDER = resultsFolder;
            Properties.Settings.Default.Save();


            string message = "Uma pasta denomida 'Resultados perSONA' foi criada em 'Documentos'. Nesta pasta serão salvos todos os dados gerados no perSONA.";
            string caption = "Sucesso";
            var result = MessageBox.Show(message, caption,
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);

            firstUseCheck();
        }

        private void firstUseCheck()
        {
            if (firstUse == false)
            {
                vAInterface.updateApplicatorList();
                vAInterface.updatePatientList();
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