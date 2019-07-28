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
            InitializeComponent();
            selectReproduction.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
