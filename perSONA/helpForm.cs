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
            string w = System.Windows.SystemParameters.PrimaryScreenWidth.ToString();
            string h = System.Windows.SystemParameters.PrimaryScreenHeight.ToString();
            int x = Int32.Parse(w);
            int y = Int32.Parse(h);
            this.MinimumSize = new System.Drawing.Size((x * 1100) / 1920, (y * 700) / 1080);
            //this.MinimumSize = new System.Drawing.Size(1100,700);
        }

        private void OpenManual_Click(object sender, EventArgs e)
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var path = Path.Combine(dir.ToString(), "LVA-UFSC", "perSONA-BETA","perSONA","data","MANUAL-DE-USUARIO.pdf");
            Console.WriteLine(path);
            string filemanual = path;
            System.Diagnostics.Process.Start(filemanual);
        }
    }
}
//this.MinimumSize = new System.Drawing.Size((1920 * 1100) / x, (1080 * 700) / y);
