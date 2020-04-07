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

            // if fisrt time use persona, save first use date // 
            if (firstUse)
            {
                //Set destiny result folder
                String UserName = System.Environment.UserName;
                Properties.Settings.Default.RESULTS_FOLDER = "C:/Users/" + UserName + "/Documents/perSONA";

                //Set today date
                Properties.Settings.Default.FIRST_USE_DATA = DateTime.Now;
                firstUse = false;
                firstUseData = Properties.Settings.Default.FIRST_USE_DATA;
            }

            //if the version is demo and 90 days have passed block program and require Serial key
            if (demoVersion && (DateTime.Compare(DateTime.Now, firstUseData.AddDays(90)) == 1))
            {
                Application.Run(new licenseExpirationForm());
            }
            else
            {
                Application.Run(new Form5());
            }

            Properties.Settings.Default.FIRST_USE = firstUse;

            //Properties.Settings.Default.FIRST_USE = true;
            //Properties.Settings.Default.DEMO_VERSION = true;

            Properties.Settings.Default.Save();

        }
    }
}
