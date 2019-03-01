using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        public Form1()
        {
            InitializeComponent();
            vA = new VANet();
//#if DEBUG
//            Process cmd = new Process();
//            cmd.StartInfo.FileName = "cmd.exe";
//            cmd.StartInfo.RedirectStandardInput = true;
//            cmd.StartInfo.RedirectStandardOutput = true;
//            cmd.StartInfo.CreateNoWindow = false;
//            cmd.StartInfo.UseShellExecute = false;
//            cmd.Start();

//            cmd.StandardInput.WriteLine("C:\\Users\\bernardo.murta\\Documents\\projects\\VA_full.v2018a_dev.win32-x64.vc12\\run_VAServer.bat");
//            cmd.StandardInput.Flush();
//            cmd.StandardInput.Close();
//            cmd.WaitForExit();
//            Console.WriteLine(cmd.StandardOutput.ReadToEnd());

//#else
            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "C:\\Users\\bernardo.murta\\Documents\\projects\\VA_full.v2018a_dev.win32-x64.vc12\\run_VAServer.bat";
            //process.StartInfo = startInfo;
            //process.Start();
//#endif
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(1000);
            bool success = vA.Connect();
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
        }
    }
}
