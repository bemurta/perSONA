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
    public partial class helpForm : Form
    {
        public helpForm()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(1100,700);
        }

        private void OpenManual_Click(object sender, EventArgs e)
        {
            string filemanual = @"C:\Program Files (x86)\LVA-UFSC\perSONA-BETA\perSONA\data\Manual_perSONA.pdf";
            System.Diagnostics.Process.Start(filemanual);
        }
    }
}
