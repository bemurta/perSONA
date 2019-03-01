using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VA;

namespace perSONA
{
    public partial class Form1 : Form
    {
        VANet vA;
        public Form1()
        {
            InitializeComponent();
            vA = new VANet();
            
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            vA.Connect();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            vA.Disconnect();
        }
    }
}
