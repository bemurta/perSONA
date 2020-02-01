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
    public partial class Form5 : Form
    {
        public Form5()
        {
            DateTime firstUseData;
            bool demoVersion;

            firstUseData = Properties.Settings.Default.FIRST_USE_DATA;
            demoVersion = Properties.Settings.Default.DEMO_VERSION;

            InitializeComponent();

            if (demoVersion) 
            {
                label1.Text = "Faltam " + firstUseData.AddDays(90).Subtract(DateTime.Now).ToString("dd") + " dias para sua licença expirar";
            }
            else
            {
                label1.Text = "Versão completa";
            }
            selectReproduction.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)           //When the button clicked the program verify if the license has expired 
        {
            int selected_index = selectReproduction.SelectedIndex;
            
            switch (selected_index)
                {
                    case 0:
                        new Form1("conf/VACore.ini", selected_index).Show();
                        Hide();
                        break;
                    case 1:
                        new Form1("conf/VACore_CLINIC_CTC.ini", selected_index).Show();
                        Hide();
                        break;
                    case 2:
                        new Form1("conf/VACore_UFSC_CTC.ini", selected_index).Show();
                        Hide();
                        break;


                    default:
                        new Form1("conf/VACore.ini", selected_index).Show();
                        Hide();
                        break;

                }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}