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
using ZedGraph;

namespace perSONA
{
    public partial class patientManagement : Form
    {
        private readonly IvAInterface vAInterface;
        public string[] loadedTests = { };
        public Patient person;


        public patientManagement(IvAInterface ivAInterface)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
            masking.SelectedIndex = 0;
            audiometrySide.SelectedIndex = 0;
            Conduction.SelectedIndex = 0;
            sexTab.SelectedIndex = 0;
            motivationBox.SelectedIndex = 0;
            work.SelectedIndex = 0;
        }

        public patientManagement(IvAInterface ivAInterface, Patient person)
        {
            InitializeComponent();
            this.vAInterface = ivAInterface;
            masking.SelectedIndex = 0;
            audiometrySide.SelectedIndex = 0;
            Conduction.SelectedIndex = 0;
            sexTab.SelectedIndex = 0;
            motivationBox.SelectedIndex = 0;
            work.SelectedIndex = 0;
            bindPatient(person);
            this.person = person;
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
            sexTab.Text = person.Sex;
            loadedTests = person.Tests;
            testsBox.DataSource = person.Tests;
            audiometryLists.DataSource = person.Audiometrys;
            try
            {
                work.Text = person.Work;
                otherCoditions.Text = person.OtherConditions;
                leftActivationDate.Value = person.LeftActivationDate;
                leftDiagnosis.Value = person.LeftDiagnosis;
                leftDevice.Text = person.LeftEarDevice;
                leftHearingLoss.Text = person.LeftHearingLoss;
                leftLossDegree.Text = person.LeftLossDegree;
                leftManufacturer.Text = person.LeftManufacturer;
                leftModel.Text = person.LeftModel;
                leftPostLingual.Text = person.LeftPostLingual;
                leftPrivationYears.Text = person.LeftPrivationYears;
                rightActivationDate.Value = person.RightActivationDate;
                rightDiagnosis.Value = person.RightDiagnosis;
                rightDevice.Text = person.RightEarDevice;
                rightHearingLoss.Text = person.RightHearingLoss;
                rightLossDegree.Text = person.RightLossDegree;
                rightManufacturer.Text = person.RightManufacturer;
                rightModel.Text = person.RightModel;
                rightPostLingual.Text = person.RightPostLingual;
                rightPrivationYears.Text = person.RightPrivationYears;
            }
            catch (Exception)
            {
                MessageBox.Show("Problemas com dados do paciente");
            }
            

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
                Sex = sexTab.Text,
                UpdatedAt = DateTime.UtcNow,
                Work = work.Text,
                OtherConditions = otherCoditions.Text,
                LeftActivationDate = leftActivationDate.Value,
                LeftDiagnosis = leftDiagnosis.Value,
                LeftEarDevice = leftDevice.Text,
                LeftHearingLoss = leftHearingLoss.Text,
                LeftLossDegree = leftLossDegree.Text,
                LeftManufacturer = leftManufacturer.Text,
                LeftModel = leftModel.Text,
                LeftPostLingual = leftPostLingual.Text,
                LeftPrivationYears = leftPrivationYears.Text,
                RightActivationDate = rightActivationDate.Value,
                RightDiagnosis = rightDiagnosis.Value,
                RightEarDevice = rightDevice.Text,
                RightHearingLoss = rightHearingLoss.Text,
                RightLossDegree = rightLossDegree.Text,
                RightManufacturer = rightManufacturer.Text,
                RightModel = rightModel.Text,
                RightPostLingual = rightPostLingual.Text,
                RightPrivationYears = rightPrivationYears.Text,
                Tests = loadedTests

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
            this.person = person;
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

        private TonalAudiometryTest readAudiometry(string timestamp)
        {
            string jsonFile = string.Format("{0}/audiometry/test-{1}.json",
            Properties.Settings.Default.RESULTS_FOLDER,
            timestamp);
            string json = File.ReadAllText(jsonFile);
            TonalAudiometryTest test = JsonConvert.DeserializeObject<TonalAudiometryTest>(json);
            
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

            TonalAudiometryTest Audiometry = readAudiometry(audiometryLists.SelectedItem.ToString());
            plotAudiometry(audiometryGraph, Audiometry);
        }

        public void plotAudiometry(ZedGraphControl graph, 
                TonalAudiometryTest Audiometry)
        {
            GraphPane myPane = graph.GraphPane;
            myPane.CurveList.Clear();
            double[] freqs = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            double[] freqVec = { 125, 250, 500, 1000, 2000, 4000, 8000 };
            string audiometryString = "demo";
            string audiometryType = "Air Right Unmasked";

            //foreach (var item in audiometryLists.SelectedItems)
            //{
                //speechPerceptionTest test = readTest(timestamp);
                //double[] signalToNoiseArray = test.IterativeSNR;

            audiometryString = string.Concat(audiometryString, "\r\n", "freqs: ");



            PointPairList audiometry = new PointPairList
            {
                { freqs, Audiometry.ThisAudiometry }
            };
            vAInterface.concatText(audiometryType);
            LineItem audiometryCurve;

            audiometryCurve = myPane.AddCurve(Audiometry.AudiometryType, audiometry,
                        Audiometry.getColor(), Audiometry.getSymbol());
            audiometryCurve.Line.IsVisible = true;
            audiometryCurve.Line.Width = 2;
            audiometryCurve.Symbol.Size = 20;
            
            audiometryTextBox.Text = audiometryString;

            //dataGridView1.Rows.Add(audiometry);
            //}
            myPane.Legend.IsVisible = false;
            myPane.Legend.FontSpec.Size = 18;
            myPane.Legend.Border.IsVisible = false;


            myPane.Title.Text = "Audiograma";
            myPane.Title.FontSpec.Size = 21;
            myPane.XAxis.Title.Text = "Frequência em Hertz (Hz)";
            myPane.XAxis.Title.FontSpec.Size = 21;
            myPane.XAxis.Scale.FontSpec.Size = 21;

            myPane.YAxis.Title.Text = "Nível de audição em decibels (dB)";
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
            myPane.XAxis.Scale.TextLabels = Audiometry.Freqs.Select(x => x.ToString()).ToArray();
            graph.AxisChange();
            graph.Refresh();

            audiometryTextBox.Text = String.Format(
                "{0}\r\n Níveis auditivos: {1}\r\n Frequências: {2}",
                Audiometry.AudiometryType,
                string.Join(", ", Audiometry.ThisAudiometry.ToArray()),
                string.Join(", ", Audiometry.Freqs.ToArray()));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] thisAudiometry = { (double) freq1.Value,
                                        (double) freq2.Value,
                                        (double) freq3.Value,
                                        (double) freq4.Value,
                                        (double) freq6.Value,
                                        (double) freq7.Value,
                                        (double) freq8.Value};


            double[] freqs = { 125, 250, 500, 1000, 2000, 4000, 8000 };

            TonalAudiometryTest Audiometry = new TonalAudiometryTest();

            string audiometryType = "";
            Audiometry.AudiometryType = audiometryType;
            Audiometry.Freqs = freqs;
            Audiometry.ThisAudiometry = thisAudiometry;
            Audiometry.Side = audiometrySide.Text == "Esquerdo" ? "Left" : "Right";
            Audiometry.Masker = masking.Text == "Sim" ? true : false;
            Audiometry.Via = "Bone";

            if (Conduction.Text.StartsWith("A"))
            {
                Audiometry.Via = "Air";
            }

            if (!Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Right")
            {
                Audiometry.AudiometryType = "Orelha Direita, via Aérea, Sem Mascaramento";
            }
            else if (!Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Left")
            {
                Audiometry.AudiometryType = "Orelha Esquerda, via Aérea, Sem Mascaramento";
            }
            else if (Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Right")
            {
                Audiometry.AudiometryType = "Orelha Direita, via Aérea, Com Mascaramento";
            }
            else if (Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Left")
            {
                Audiometry.AudiometryType = "Orelha Esquerda, via Aérea, Com Mascaramento";
            }
            else
            {
                Audiometry.AudiometryType = "Condução Óssea (Em desenvolvimento)";
            }

        
            plotAudiometry(audiometryGraph, Audiometry);
        }

        private void button1_Click(object sender, EventArgs e)
        {


            double[] thisAudiometry = { (double) freq1.Value,
                                        (double) freq2.Value,
                                        (double) freq3.Value,
                                        (double) freq4.Value,
                                        (double) freq6.Value,
                                        (double) freq7.Value,
                                        (double) freq8.Value};


            double[] freqs = { 125, 250, 500, 1000, 2000, 4000, 8000 };

            TonalAudiometryTest Audiometry = new TonalAudiometryTest();

            string audiometryType = "";
            Audiometry.AudiometryType = audiometryType;
            Audiometry.Freqs = freqs;
            Audiometry.ThisAudiometry = thisAudiometry;
            Audiometry.Side = audiometrySide.Text == "Esquerdo" ? "Left" : "Right";
            Audiometry.Masker = masking.Text == "Sim" ? true : false;
            Audiometry.Via = "Bone";

            if (Conduction.Text.StartsWith("A"))
            {
                Audiometry.Via = "Air";
            }


            if (!Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Right")
            {
                Audiometry.AudiometryType = "Orelha Direita, via Aérea, Sem Mascaramento";
            }
            else if (!Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Left")
            {
                Audiometry.AudiometryType = "Orelha Esquerda, via Aérea, Sem Mascaramento";
            }
            else if (Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Right")
            {
                Audiometry.AudiometryType = "Orelha Direita, via Aérea, Com Mascaramento";
            }
            else if (Audiometry.Masker && Audiometry.Via == "Air" && Audiometry.Side == "Left")
            {
                Audiometry.AudiometryType = "Orelha Esquerda, via Aérea, Com Mascaramento";
            }
            else
            {
                Audiometry.AudiometryType = "Condução Óssea (Em desenvolvimento)";
            }

            vAInterface.addCompletedAudiometry(Audiometry, nameBox.Text);

            string jsonFile = string.Format("{0}/patients/{1}.json",
                                 Properties.Settings.Default.RESULTS_FOLDER,
                                 nameBox.Text);
            string json = File.ReadAllText(jsonFile);
            person = JsonConvert.DeserializeObject<Patient>(json);
            

            bindPatient(person);
            
        }
   

    }
}
