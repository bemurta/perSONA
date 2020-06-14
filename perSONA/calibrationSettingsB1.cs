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
    public partial class calibrationSettingsB1 : Form
    {
        private readonly IvAInterface vAInterface;

        public calibrationSettingsB1(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            new calibrationHelp(vAInterface).Show();
            Close();
        }
    }
}
