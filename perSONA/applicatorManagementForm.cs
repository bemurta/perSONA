using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace perSONA
{
    public partial class applicatorManagementForm : Form
    {
        private readonly IvAInterface vAInterface;
        public Applicator person;

        public applicatorManagementForm(IvAInterface ivAInterface)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
        }

        public applicatorManagementForm(IvAInterface ivAInterface, Applicator person)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
            bindApplicator(person);
            this.Text = "Aplicador: " + person.Name;
            this.person = person;
        }

        private void save_Click(object sender, EventArgs e)
        {
            const string message = "Alterações salvas!";
            const string caption = "Sucesso";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK);
            Applicator person = new Applicator()
            {
                Name = nameBox.Text,
                CRFa = CRFaBox.Text,
                BornDate = bornDate.Value,
                DDI = DDIBox.Text,
                PhoneNumber = phoneBox.Text,
                Address = addressBox.Text,
                EMail = emailBox.Text
            };

            string personJson = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            try
            {
                File.WriteAllText(string.Format("{0}/Applicators/{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                person.Name), personJson);

            }
            catch (DirectoryNotFoundException)
            {
                string dir = string.Format("{0}/Applicators", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(dir);
                File.WriteAllText(string.Format("{0}/Applicators/{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                person.Name), personJson);
            }
            this.person = person;
            vAInterface.updateApplicatorList();
        }

        public void bindApplicator(Applicator person)
        {
            nameBox.Text = person.Name;
            CRFaBox.Text = person.CRFa;
            bornDate.Value = person.BornDate;
            DDIBox.Text = person.DDI;
            phoneBox.Text = person.PhoneNumber;
            addressBox.Text = person.Address;
            emailBox.Text = person.EMail;
        }
    }
}
