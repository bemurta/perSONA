using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class preliminaryTest : Form
    {
        private readonly IvAInterface vAInterface;
        public VANet vA { get; private set; }
        bool calibrationEARPHONE;
        bool calibration2SPEAKER;
        bool calibration8SPEAKER;

        public preliminaryTest(IvAInterface vAInterface)
        {
            InitializeComponent();
            this.vAInterface = vAInterface;

            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                volume.Value = Properties.Settings.Default.EARPHONE_VOLUME;
                volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.EARPHONE_VOLUME);
            }
            else
            {
                volume.Value = Properties.Settings.Default.SPEAKER_VOLUME;
                volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.SPEAKER_VOLUME);
            }
        }

        private void soundSignal_Click(object sender, EventArgs e)
        {
            vA = vAInterface.getVa();
            vA.Reset();
            int receiverId = vA.CreateSoundReceiver("Subject");
            double xSides = 0;
            double zFront = 0;
            double yHeight = 1.7;

            VAVec3 receiverPosition = new VAVec3(xSides, yHeight, zFront);
            VAVec3 receiverOrientationV = new VAVec3(0, 0, -1);
            VAVec3 receiverOrientationU = new VAVec3(0, 1, 0);

            vA.SetSoundReceiverPosition(receiverId, receiverPosition);
            vA.SetSoundReceiverOrientationVU(receiverId, receiverOrientationV, receiverOrientationU);
            vAInterface.concatText(string.Format("Receiver: {3} at position: {0},{1},{2}, looking forward ",
                                     xSides, zFront, yHeight, receiverId));

            int hrirId = vA.CreateDirectivityFromFile("data/ITA_Artificial_Head_5x5_44kHz_128.v17.ir.daff");
            vA.SetSoundReceiverDirectivity(receiverId, hrirId);

            string speechFile = "data/Sounds/CalibrationNoise.wav";
            vAInterface.concatText(speechFile);
            vAInterface.concatText(string.Format("Calibration sound angle: {0}", 90));
            vAInterface.createAcousticScene(speechFile, speechFile);

            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                vAInterface.playScene(1.7, 90, 40);

            }
            else if (Properties.Settings.Default.REPRODUCTION_MODE == "2 Speakers")
            {
                vAInterface.playScene(1.7, 90, 40);

            }
            else
            {
                vAInterface.playScene(1.7, 0, 40);
            }
        }

        private void volume_Scroll(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
            {
                Properties.Settings.Default.EARPHONE_VOLUME = volume.Value;
                volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.EARPHONE_VOLUME);
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.REPRODUCTION_MODE != "Earphone")
            {
                Properties.Settings.Default.SPEAKER_VOLUME = volume.Value;
                volumeLabel.Text = string.Format("Volume: {0} %", Properties.Settings.Default.SPEAKER_VOLUME);
                Properties.Settings.Default.Save();
            }
        }

        private void calibrate_Click(object sender, EventArgs e)
        {
            calibrationEARPHONE = true;
            calibration2SPEAKER = true;
            calibration8SPEAKER = true;

            if (Properties.Settings.Default.REPRODUCTION_MODE == "Earphone")
                Properties.Settings.Default.CALIBRATED_SNR_EARPHONE = calibrationEARPHONE;

            else if (Properties.Settings.Default.REPRODUCTION_MODE == "2 Speakers")
                Properties.Settings.Default.CALIBRATED_SNR_2_SPEAKER = calibration2SPEAKER;

            else if (Properties.Settings.Default.REPRODUCTION_MODE == "8 Speakers")
                Properties.Settings.Default.CALIBRATED_SNR_8_SPEAKER = calibration8SPEAKER;

            Properties.Settings.Default.Save();

            string message = "Calibração da faixa dinâmica limite realizada.";
            const string caption = "Calibração do pré-ensaio";
            var result = MessageBox.Show(message, caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            Close();
        }
    }
}
