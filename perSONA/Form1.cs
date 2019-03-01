using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VA;

namespace perSONA
{
    public partial class Form1 : Form
    {
        VANet vA;
        Process p;
        ProcessStartInfo info;
        StreamWriter sw;
        public Form1()
        {
            InitializeComponent();
            vA = new VANet();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(1000);
            bool success = vA.Connect();
            Console.WriteLine(success.ToString());
            if (success)
            {
                buttonConnect.BackColor = Color.Green;
            }
            else
            {
                buttonConnect.BackColor = Color.Red;
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            buttonConnect.BackColor = Color.Gray;
            vA.Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            vA.Disconnect();


            sw.WriteLine("q");
        }

        private void openServer_Click(object sender, EventArgs e)
        {
            p = new Process();
            info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            p.StartInfo = info;
            p.Start();

            using (sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("cd ../../..");
                    sw.WriteLine("run_VAServer.bat");
                }
            }
        }
    }
}
