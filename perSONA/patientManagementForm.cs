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
using System.Net.Http.Headers;
using DocumentFormat.OpenXml.Presentation;

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
            resizeScreen();
            this.Text = "Módulo de Gerenciamento de Paciente";
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            this.vAInterface = ivAInterface;
            audiometrySide.SelectedIndex = 0;
            Conduction.SelectedIndex = 0;
            sexTab.SelectedIndex = 0;
            motivationBox.SelectedIndex = 0;
            work.SelectedIndex = 0;
        }

        public patientManagement(IvAInterface ivAInterface, Patient person)
        {
            InitializeComponent();
            resizeScreen();
            this.vAInterface = ivAInterface;
            audiometrySide.SelectedIndex = 0;
            Conduction.SelectedIndex = 0;
            sexTab.SelectedIndex = 0;
            motivationBox.SelectedIndex = 0;
            work.SelectedIndex = 0;
            bindPatient(person);
            TonalAudiometryTest.bindGraph(audiometryGraph);
            nameBox.Enabled = false;
            this.Text = "Paciente: " + person.Name;
            this.person = person;
        }

        public void bindPatient(Patient person)
        {
            nameBox.Text = person.Name;
            addressBox.Text = person.Address;
            bornDate.Value = person.BornDate;
            motivationBox.SelectedItem = person.Motivation;
            observationBox.Text = person.Observations;
            DDIBox.Text = person.DDI;
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
                leftHearingEtiology.Text = person.LeftHearingEtiology;
                leftLossDegree.Text = person.LeftLossDegree;
                leftManufacturer.Text = person.LeftManufacturer;
                leftModel.Text = person.LeftModel;
                leftPostLingual.Text = person.LeftPostLingual;
                leftPrivationYears.Text = person.LeftPrivationYears;
                rightActivationDate.Value = person.RightActivationDate;
                rightDiagnosis.Value = person.RightDiagnosis;
                rightDevice.Text = person.RightEarDevice;
                rightHearingLoss.Text = person.RightHearingLoss;
                rightHearingEtiology.Text = person.RightHearingEtiology;
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
            const string message = "Alterações salvas!";
            const string caption = "Sucesso";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK);
            Patient person = new Patient()
            {
                Name = nameBox.Text,
                Address = addressBox.Text,
                BornDate = bornDate.Value,
                Motivation = motivationBox.SelectedItem.ToString(),
                Observations = observationBox.Text,
                DDI = DDIBox.Text,
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
                LeftHearingEtiology = leftHearingEtiology.Text,
                LeftLossDegree = leftLossDegree.Text,
                LeftManufacturer = leftManufacturer.Text,
                LeftModel = leftModel.Text,
                LeftPostLingual = leftPostLingual.Text,
                LeftPrivationYears = leftPrivationYears.Text,
                RightActivationDate = rightActivationDate.Value,
                RightDiagnosis = rightDiagnosis.Value,
                RightEarDevice = rightDevice.Text,
                RightHearingLoss = rightHearingLoss.Text,
                RightHearingEtiology = rightHearingEtiology.Text,
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
            this.Close();
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

            Image img = Image.FromFile(@"C:\Program Files (x86)\LVA-UFSC\perSONA-BETA\perSONA\data\Logo_Large.png");
            var logo = new ImageObj(img, new RectangleF(0.92f, 1.13f, 0.13f, 0.13f), CoordType.ChartFraction, AlignH.Left, AlignV.Top);
            myPane.GraphObjList.Add(logo);

            var colorRotator = new ColorSymbolRotator();

            string testsString = "";
            List<double> maxsAndMins = new List<double>();

            foreach (var item in testsBox.SelectedItems)
            {
                string timestamp = item.ToString();
                speechPerceptionTest test = readTest(timestamp);

                double[] signalToNoiseArray = test.IterativeSNR;
                double actualSNR = signalToNoiseArray.Last<double>();
                double meanSNR = vAInterface.getMeanSRT(test.IterativeSNR);

                testsString = string.Concat(testsString, "\r\n", string.Format("\r\nSRT deste teste: {0} dB", Math.Round(meanSNR,2)), string.Format("\r\nLimiar de convergência SNR: {0} dB",actualSNR), "\r\n", test.testSummary());
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
            myPane.YAxis.Title.Text = "SNR [dB]";
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            vAInterface.concatText(audiometryLists.SelectedItems.Count.ToString());
            audiometryGraph.GraphPane.CurveList.Clear();

            if (audiometryLists.SelectedItems.Count == 0) // if no have selected itens
            {
                audiometryGraph.GraphPane.AxisChange();
                audiometryGraph.Refresh();
            }
            else if (audiometryLists.SelectedItems.Count == 1) // if have only one selected itens
            {
                confButtonsPanel.Visible = true;
                TonalAudiometryTest Audiometry = readAudiometry(audiometryLists.SelectedItem.ToString());
                plotAudiometry(audiometryGraph, Audiometry);

                audiometryDate.Value = Audiometry.audiometryDate;   // Date

                // Via
                if (Audiometry.Via == "Air") Conduction.Text = "aérea";
                else if (Audiometry.Via == "Bone (mastoid)") Conduction.Text = "óssea (mastóide)";
                else if (Audiometry.Via == "Bone (forehead)") Conduction.Text = "óssea (fronte)";
                else Conduction.Text = "campo livre";
                
                // Side                
                if (Audiometry.Side == "Left") audiometrySide.Text = "Esquerdo";
                else audiometrySide.Text = "Direito";
            }
            else 
            {
                confButtonsPanel.Visible = false;
                foreach (object audiometryItem in audiometryLists.SelectedItems)    // Take all selected itens and plot
                {
                    TonalAudiometryTest Audiometry = readAudiometry(audiometryItem.ToString());
                    plotAudiometry(audiometryGraph, Audiometry);
                }
            }
        }

        public void makeAudiometry(TonalAudiometryTest Audiometry)
        {
            List<double> freqs = new List<double>();
            List<double> decibels = new List<double>();
            List<double> masks = new List<double>();
            List<bool> noReply = new List<bool>();

            double[] allFreqs = { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };

            double[] allDecibels = { (double) dB125.Value, (double) dB250.Value, (double) dB500.Value, (double) dB750.Value,
                                    (double) dB1000.Value, (double) dB1500.Value, (double) dB2000.Value, (double) dB3000.Value,
                                    (double) dB4000.Value, (double) dB6000.Value, (double) dB8000.Value};

            double[] allMasks = { (double)masking125.Value, (double)masking250.Value, (double)masking500.Value, (double)masking750.Value,
                                  (double)masking1000.Value, (double)masking1500.Value, (double)masking2000.Value, (double)masking3000.Value,
                                  (double)masking4000.Value, (double)masking6000.Value, (double)masking8000.Value};


            bool[] allNoReply = { (bool) noReply125.Checked, (bool) noReply250.Checked, (bool) noReply500.Checked, (bool) noReply750.Checked,
                                  (bool) noReply1000.Checked, (bool) noReply1500.Checked, (bool) noReply2000.Checked, (bool) noReply3000.Checked,
                                  (bool) noReply4000.Checked, (bool) noReply6000.Checked, (bool)noReply8000.Checked};

            bool[] useFrequencyCheckResult = { (bool) useFrequency125.Checked, (bool) useFrequency250.Checked, (bool) useFrequency500.Checked, (bool) useFrequency750.Checked,
                                               (bool) useFrequency1000.Checked, (bool) useFrequency1500.Checked, (bool) useFrequency2000.Checked, (bool) useFrequency3000.Checked,
                                               (bool)useFrequency4000.Checked, (bool)useFrequency6000.Checked, (bool)useFrequency8000.Checked};

            int i = 0;
            foreach (bool useFrequency in useFrequencyCheckResult)
            {
                if (useFrequency)
                {
                    decibels.Add(allDecibels[i]);
                    masks.Add(allMasks[i]);
                    freqs.Add(allFreqs[i]);
                    noReply.Add(allNoReply[i]);
                }
                i++;
            }

            string audiometryType = "";
            Audiometry.audiometryDate = audiometryDate.Value;
            Audiometry.AudiometryType = audiometryType;
            Audiometry.Freqs = freqs;
            Audiometry.dB = decibels;
            Audiometry.Side = audiometrySide.Text == "Esquerdo" ? "Left" : "Right";
            Audiometry.Masker = masks;
            Audiometry.NoReply = noReply;

            if (Conduction.Text == "Aérea") Audiometry.Via = "Air";
            else if (Conduction.Text == "Óssea (mastóide)") Audiometry.Via = "Bone (mastoid)";
            else if (Conduction.Text == "Óssea (fronte)") Audiometry.Via = "Bone (forehead)";
            else Audiometry.Via = "Free field";

            audiometryType = "Orelha " + audiometrySide.Text;
            audiometryType = audiometryType.Remove(audiometryType.Length - 1);
            audiometryType = audiometryType + "a, Via " + Conduction.Text;

            Audiometry.AudiometryType = audiometryType;
        }
        public void plotAudiometry(ZedGraphControl graph, TonalAudiometryTest Audiometry)
        {
            GraphPane myPane = graph.GraphPane;
            //myPane.CurveList.Clear();
            string audiometryString = "demo";
            string audiometryType = "Air Right Unmasked";

            audiometryString = string.Concat(audiometryString, "\r\n", "freqs: ");

            //linearize freqs
            List<double> linearizedFreqs = new List<double>();
            foreach (double frequency in Audiometry.Freqs)
            {
                linearizedFreqs.Add(Math.Log(frequency / 125, 2) + 1);
            }

            for (int i = 0; i < Audiometry.Freqs.Count; i++)
            {
                TonalAudiometryTest.drawSymbol(graph, linearizedFreqs[i], Audiometry.dB[i],Audiometry.Masker[i],
                    Audiometry.NoReply[i], Audiometry.Side, Audiometry.Via);
                //2^(x-1)*125
            }

            PointPairList audiometry = new PointPairList
            {
                {linearizedFreqs.ToArray(), Audiometry.dB.ToArray()}
            };

            if (Audiometry.Via == "Air")
            {
                LineItem audiometryCurve;
                audiometryCurve = myPane.AddCurve(Audiometry.AudiometryType, audiometry,
                    Audiometry.getColor(), SymbolType.None);
                audiometryCurve.IsX2Axis = true;
                Audiometry.changeLine(audiometryCurve.Line);
            }

            audiometryTextBox.Text = audiometryString;
            audiometryTextBox.Text = String.Format(
                "{0}\r\n" + "Níveis auditivos: {1}\r\n" + "Frequências: {2}",
                Audiometry.AudiometryType,
                string.Join(", ", Audiometry.dB.ToArray()),
                string.Join(", ", Audiometry.Freqs.ToArray()));
            audiometryDate.Value = Audiometry.audiometryDate;

            graph.AxisChange();
            graph.Refresh();
        }

        private void previewAudiometryButton_Click(object sender, EventArgs e)
        {
            TonalAudiometryTest Audiometry = new TonalAudiometryTest();
            makeAudiometry(Audiometry);
            audiometryGraph.GraphPane.CurveList.Clear();
            plotAudiometry(audiometryGraph, Audiometry);
        }

        private void saveAudiometryButton_Click(object sender, EventArgs e)
        {
            TonalAudiometryTest Audiometry = new TonalAudiometryTest();            
            makeAudiometry(Audiometry);
            vAInterface.addCompletedAudiometry(Audiometry, nameBox.Text);

            string jsonFile = string.Format("{0}/patients/{1}.json",
                                 Properties.Settings.Default.RESULTS_FOLDER,
                                 nameBox.Text);
            string json = File.ReadAllText(jsonFile);
            person = JsonConvert.DeserializeObject<Patient>(json);

            bindPatient(person);
        }

        private void deleteAudiometryButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja deletar a audiometria?", audiometryLists.SelectedItem.ToString(),
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)

            {
                string jsonFile = string.Format("{0}/patients/{1}.json",
                                                Properties.Settings.Default.RESULTS_FOLDER,
                                                nameBox.Text);
                string json = File.ReadAllText(jsonFile);
                Patient patient = Newtonsoft.Json.JsonConvert.DeserializeObject<Patient>(json);


                List<string> tests = patient.Audiometrys.OfType<string>().ToList();
                string delete = audiometryLists.SelectedItem.ToString();
                tests.Remove(delete);

                patient.Audiometrys = tests.ToArray();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(patient);
                File.WriteAllText(jsonFile, output);


                json = File.ReadAllText(jsonFile);
                person = JsonConvert.DeserializeObject<Patient>(json);

                audiometryLists.DataSource = person.Audiometrys;

                jsonFile = string.Format("{0}/audiometry/{1}.json",
                                            Properties.Settings.Default.RESULTS_FOLDER,
                                            "test-" + delete);
                File.Delete(jsonFile);
            }
        }

        private void useFrequency_CheckedChanged(object sender, EventArgs e)
        {
            bool[] useFrequency = { (bool) useFrequency125.Checked, (bool) useFrequency250.Checked, (bool) useFrequency500.Checked, (bool) useFrequency750.Checked,
                                    (bool) useFrequency1000.Checked, (bool) useFrequency1500.Checked, (bool) useFrequency2000.Checked, (bool) useFrequency3000.Checked,
                                    (bool)useFrequency4000.Checked, (bool)useFrequency6000.Checked, (bool)useFrequency8000.Checked};

            System.Windows.Forms.Label[] freqsLabel = {label125, label250, label500, label750,
                                                       label1000, label1500, label2000, label3000,
                                                       label4000, label6000, label8000};
            int i = 0;
            foreach (bool check in useFrequency)
            {
                if (check)
                {
                    freqsLabel[i].ForeColor = Color.Green;
                }
                else
                {
                    freqsLabel[i].ForeColor = Color.Red;
                }
                i++;
            }
        }
        private void resizeScreen()
        {
            double PCResolutionWidth = Screen.PrimaryScreen.Bounds.Width;
            double PCResolutionHeight = Screen.PrimaryScreen.Bounds.Height;

            double formWidth = this.Size.Width;
            double formHeight = this.Size.Height;

            if ((formWidth > PCResolutionWidth) | (formHeight > PCResolutionHeight * 0.925))
            {
                int newWidth = Convert.ToInt32(PCResolutionWidth * 0.87);
                int newHeight = Convert.ToInt32(PCResolutionHeight * 0.9);
                this.Size = new Size(newWidth, newHeight);
            }
        }
        
        private void save_img_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog audiograph = new SaveFileDialog();
            audiograph.Title = "Salvar Audiograma"; 
            audiograph.DefaultExt = ".png";  
            audiograph.AddExtension = true;
            audiograph.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Folder Initialize "My Documents"
            audiograph.RestoreDirectory = true; //Folder Initialize in last oppened folder
            audiograph.Filter = "PNG Image|*.png|JPeg Image|*.jpg"; // Filter files by extension

            audiometryGraph.SaveFileDialog = audiograph;
            audiometryGraph.SaveAsBitmap();

        }

        private void print_audiograph_button_Click(object sender, EventArgs e)
        {
            audiometryGraph.DoPrint();
        }

        private void save_noisetest_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog noisetest = new SaveFileDialog();
            noisetest.Title = "Salvar Teste de Ruído"; 
            noisetest.DefaultExt = ".png"; 
            noisetest.AddExtension = true;
            noisetest.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            noisetest.RestoreDirectory = true; 
            noisetest.Filter = "PNG Image|*.png|JPeg Image|*.jpg"; 

            testsGraph.SaveFileDialog = noisetest;
            testsGraph.SaveAsBitmap();
        }

        private void print_noisetest_button_Click(object sender, EventArgs e)
        {
            testsGraph.DoPrint();
        }

        private void testsGraph_Load(object sender, EventArgs e)
        {
            GraphPane pane = testsGraph.GraphPane;
            Image img = Image.FromFile(@"C:\Program Files (x86)\LVA-UFSC\perSONA-BETA\perSONA\data\Logo_Large.png");
            var logo = new ImageObj(img, new RectangleF(0.1f, 0.1f, 0.11f, 0.13f), CoordType.ChartFraction, AlignH.Left, AlignV.Top);
            pane.GraphObjList.Add(logo); 
            testsGraph.Refresh();
        }
    }
}
