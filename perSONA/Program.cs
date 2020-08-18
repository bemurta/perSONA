using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace perSONA
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool demoVersion;
            bool firstUse;
            DateTime firstUseData;

            demoVersion = Properties.Settings.Default.DEMO_VERSION;
            firstUse = Properties.Settings.Default.FIRST_USE;
            firstUseData = Properties.Settings.Default.FIRST_USE_DATA;

            Properties.Settings.Default.ITERATOR = 0;
            Properties.Settings.Default.Save();


            //if first Use, select result forder. else if the version is demo and 90 days have passed,
            //block program and require Serial key, else run normally
            if (firstUse)
            {
                Application.Run(new Form2());
            }
            else if (demoVersion && (DateTime.Compare(DateTime.Now, firstUseData.AddDays(90)) == 1))
            {
                Application.Run(new licenseExpirationForm());
            }
            else
            {
                Application.Run(new Form5());
            }
        }
    }
}