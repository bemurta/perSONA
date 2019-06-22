using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace perSONA
{
    public partial class Form3 : Form
    {
        private readonly IvAInterface vAInterface;

        public Form3(IvAInterface ivAInterface)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
        }

        public Form3(IvAInterface ivAInterface, Patient person)
        {
            InitializeComponent();
            bindPatient(person);
            this.vAInterface = ivAInterface;
        }

        public void bindPatient(Patient person)
        {

            nameBox.Text = person.Name;
            addressBox.Text = person.Address;
            bornDate.Value = person.BornDate;
            motivationBox.SelectedItem = person.Motivation;
            observationBox.Text = person.Observations;
            phoneBox.Text = person.PhoneNumber;
            scholarBox.Text = person.Scolarity;
            sexTab.SelectedText = person.Sex;

            testsBox.DataSource = person.Tests;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Patient person = new Patient()
            {
                Name = nameBox.Text,
                Address = addressBox.Text,
                BornDate = bornDate.Value,
                Motivation = motivationBox.SelectedItem.ToString(),
                Observations = observationBox.Text,
                PhoneNumber = phoneBox.Text,
                Scolarity = scholarBox.Text,
                Sex = sexTab.SelectedText,
                UpdatedAt = DateTime.UtcNow
            };

            string personJson = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            //IvAInterface.concatText(string.Format("Created patient {0}", person.Name));
            try
            {
                File.WriteAllText(string.Format("{0}/patients/{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                person.Name), personJson);

            }
            catch (DirectoryNotFoundException)
            {
                string dir = string.Format("{0}/patients", Properties.Settings.Default.RESULTS_FOLDER);
                Directory.CreateDirectory(dir);
                File.WriteAllText(string.Format("{0}/patients/{1}.json",
                                Properties.Settings.Default.RESULTS_FOLDER,
                                person.Name), personJson);
            }
            vAInterface.updatePatientList();
        }

        private void motivationBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void testsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateIterationGraph(testsGraph);
        }

        private speechPerceptionTest readTest(string timestamp)
        {
            string jsonFile = string.Format("{0}/tests/test-{1}.json",
            Properties.Settings.Default.RESULTS_FOLDER,
            timestamp);
            string json = File.ReadAllText(jsonFile);
            speechPerceptionTest test = Newtonsoft.Json.JsonConvert.DeserializeObject<speechPerceptionTest>(json);
            
            return test;
        }

        private void updateIterationGraph(ZedGraphControl graph)
        {

            
            GraphPane myPane = graph.GraphPane;
            myPane.CurveList.Clear();

            var colorRotator = new ColorSymbolRotator();

            string testsString = "";

            foreach (var item in testsBox.SelectedItems)
            {
                string timestamp = item.ToString();
                speechPerceptionTest test = readTest(timestamp);
                double[] signalToNoiseArray = test.IterativeSNR;

                testsString = string.Concat(testsString, "\r\n", test.ToString());

                PointPairList snrArray = new PointPairList();
                List<double> indexes = new List<double>();
                for (int i = 1; i < signalToNoiseArray.Length + 2; i++)
                {
                    double value = i;
                    indexes.Add(value);
                }
                snrArray.Add(indexes.ToArray(), signalToNoiseArray);
                LineItem snrCurve = myPane.AddCurve(timestamp.Substring(0,10), snrArray, colorRotator.NextColor, colorRotator.NextSymbol);
                snrCurve.Line.IsVisible = true;
                snrCurve.Line.Width = 2;
                snrCurve.Symbol.Size = 20;

            }
            testInfo.Text = testsString;
            myPane.Legend.FontSpec.Size = 18;
            myPane.Legend.Border.IsVisible = true;
            
            myPane.Title.FontSpec.Size = 21;
            myPane.XAxis.Title.FontSpec.Size = 21;
            myPane.XAxis.Scale.FontSpec.Size = 21;

            myPane.YAxis.Title.FontSpec.Size = 21;
            myPane.YAxis.Scale.FontSpec.Size = 21;

            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.MinAuto = false;
            myPane.YAxis.Scale.Min = -30;
            myPane.YAxis.Scale.Max = 30;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 20;
            graph.AxisChange();
            graph.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateIterationGraph(testsGraph);
        }
    }
}
