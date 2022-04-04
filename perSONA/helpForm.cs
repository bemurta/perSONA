using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var path = Path.Combine(dir.ToString(), "LVA-UFSC", "perSONA-BETA","perSONA","data","Manual_perSONA.pdf");
            Console.WriteLine(path);
            string filemanual = path;
            System.Diagnostics.Process.Start(filemanual);
        }
    }
}
