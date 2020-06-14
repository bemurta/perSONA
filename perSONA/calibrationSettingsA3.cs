using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace perSONA
{
    public partial class calibrationSettingsA3 : Form
    {
        private readonly IvAInterface vAInterface;

        public calibrationSettingsA3(IvAInterface vAInterface)
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
