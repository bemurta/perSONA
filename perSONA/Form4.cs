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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(@"data/persona_short_v3.png");
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }
    }
}
