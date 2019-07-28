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
            List<double> maxsAndMins = new List<double>();

            foreach (var item in testsBox.SelectedItems)
            {
                string timestamp = item.ToString();
                speechPerceptionTest test = readTest(timestamp);
                double[] signalToNoiseArray = test.IterativeSNR;
                double meanSNR = vAInterface.getMeanSRT(test.IterativeSNR);
                testsString = string.Concat(testsString, "\r\n", string.Format("\r\nThis test SRT: {0} dB", meanSNR), "\r\n", test.testSummary());
                Color colorScene = Color.DodgerBlue;

                PointPairList snrArray = new PointPairList();
                List<double> indexes = new List<double>();
                for (int i = 1; i < signalToNoiseArray.Length + 2; i++)
                {
                    double value = i;
                    indexes.Add(value);
                    maxsAndMins.Add(value);
                }
                snrArray.Add(indexes.ToArray(), signalToNoiseArray);
                if (test.AngleSpeech == 0)
                {
                    colorScene = Color.DimGray;
                }
                else if(test.AngleSpeech == 90)
                {
                    colorScene = Color.Crimson;
                }

                LineItem snrCurve = myPane.AddCurve(timestamp.Substring(0,10), snrArray,
                    colorScene, colorRotator.NextSymbol);
                snrCurve.Line.IsVisible = true;
                snrCurve.Line.Width = 2;
                snrCurve.Symbol.Size = 10;

            }
            testInfo.Text = testsString;
            myPane.Legend.FontSpec.Size = 12;
            myPane.Legend.Border.IsVisible = true;
            
            myPane.Title.FontSpec.Size = 18;
            myPane.XAxis.Title.FontSpec.Size = 18;
            myPane.XAxis.Scale.FontSpec.Size = 21;

            myPane.YAxis.Title.FontSpec.Size = 18;
            myPane.YAxis.Scale.FontSpec.Size = 21;
            
            myPane.XAxis.Title.Text = "Iterações";
            myPane.YAxis.Title.Text = "SNR";
            myPane.Title.Text = "Razões sinal-ruído apresentadas";
            myPane.XAxis.Title.FontSpec.Size = 25;
            myPane.Title.FontSpec.Size = 25;
            myPane.YAxis.Title.FontSpec.Size = 25;

            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.MinAuto = false;
            myPane.YAxis.Scale.Min = -30;
            myPane.YAxis.Scale.Max = 30;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 20;
            myPane.XAxis.MinorGrid.IsVisible = true;
            myPane.Y2Axis.MinorGrid.IsVisible = true;
            

            graph.AxisChange();
            graph.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateIterationGraph(testsGraph);
        }

        private void groupBox17_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox39_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void plotAudiometry(ZedGraphControl graph, 
            double[] thisAudiometry)
        {
            GraphPane myPane = graph.GraphPane;
            myPane.CurveList.Clear();
            double[] freqs = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            string audiometryString = "demo";
            string audiometryType = "Air Right Unmasked";

            //foreach (var item in audiometryLists.SelectedItems)
            //{
                //speechPerceptionTest test = readTest(timestamp);
                //double[] signalToNoiseArray = test.IterativeSNR;

            audiometryString = string.Concat(audiometryString, "\r\n", "freqs: ");

            PointPairList audiometry = new PointPairList();
                
            audiometry.Add(freqs, thisAudiometry);
            vAInterface.concatText(audiometryType);
            LineItem audiometryCurve;
            switch (audiometryType)
            {
                case "Air Right Unmasked":
                    audiometryCurve = myPane.AddCurve(audiometryType, audiometry, 
                        Color.Red , SymbolType.Circle);
                    audiometryCurve.Line.IsVisible = true;
                    audiometryCurve.Line.Width = 2;
                    audiometryCurve.Symbol.Size = 20;
                    break;
                case "Air Left Unmasked":
                    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                        Color.Blue, SymbolType.XCross);
                    audiometryCurve.Line.IsVisible = true;
                    audiometryCurve.Line.Width = 2;
                    audiometryCurve.Symbol.Size = 20;
                    break;
                case "Air Right Masked":
                    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                        Color.Red, SymbolType.Triangle);
                    audiometryCurve.Line.IsVisible = true;
                    audiometryCurve.Line.Width = 2;
                    audiometryCurve.Symbol.Size = 20;
                    break;

                case "Air Left Masked":
                    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                        Color.Blue, SymbolType.Square);
                    audiometryCurve.Line.IsVisible = true;
                    audiometryCurve.Line.Width = 2;
                    audiometryCurve.Symbol.Size = 20;
                    break;

                //case "Mastoidal Right Unmasked":
                //    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                //        Color.Red, SymbolType.);
                //    break;
                //case "Mastoidal Left Unmasked":
                //    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                //        Color.Blue, SymbolType.XCross);
                //    break;
                //case "Mastoidal Right Masked":
                //    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                //        Color.Red, SymbolType.Triangle);
                //    break;

                //case "Mastoidal Left Masked":
                //    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                //        Color.Blue, SymbolType.Square);
                //    break;

                default:
                    audiometryCurve = myPane.AddCurve(audiometryType, audiometry,
                        Color.Red, SymbolType.Circle);
                    audiometryCurve.Line.IsVisible = true;
                    audiometryCurve.Line.Width = 2;
                    audiometryCurve.Symbol.Size = 20;
                    break;
            }

            audiometryTextBox.Text = audiometryString;

            //dataGridView1.Rows.Add(audiometry);
            //}

            myPane.Legend.FontSpec.Size = 18;
            myPane.Legend.Border.IsVisible = true;

            myPane.Title.FontSpec.Size = 21;
            myPane.XAxis.Title.FontSpec.Size = 21;
            myPane.XAxis.Scale.FontSpec.Size = 21;

            myPane.YAxis.Title.FontSpec.Size = 21;
            myPane.YAxis.Scale.FontSpec.Size = 21;

            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.MinAuto = false;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.Scale.Min = -10;
            myPane.YAxis.Scale.Max = 120;
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Scale.Min = 1;
            myPane.XAxis.Scale.Max = 7;
            myPane.XAxis.Scale.TextLabels = new string[] 
            { "125", "250", "500", "1000", "2000", "4000", "8000" };

            graph.AxisChange();
            graph.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] thisAudiometry = { 10, 20, 20, 25, 30, 30, 40 };
            double[] freqs = { 125, 250, 500, 1000, 2000, 4000, 8000 };
            plotAudiometry(audiometryGraph, thisAudiometry);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox41_Enter(object sender, EventArgs e)
        {

        }
    }
}
