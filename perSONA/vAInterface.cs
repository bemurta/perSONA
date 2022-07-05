using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VA;

namespace perSONA
{
    public interface IvAInterface
    {
        void playScene(double radius, double angle, double snr);
        void playScene(double radius, bool speechON, string speechFile, double currentSpeechPower, double speechAngle, bool noiseON, string noiseFile, double noiseAngle, double currentNoisePower);
        void stopScene(bool speechON, bool noiseON);
        void allSoundPlayersPlayScene(double radius, int numberOfSoundPlayers, string speechFile);
        void addCompletedTest(speechPerceptionTest test);
        string getDatabaseFolder();
        string getDatabaseFiles(string Location);
        void createAcousticScene(string speechFile, string noiseFile);
        string getTitle(string speechFile);
        TimeSpan getDuration(string speechFile);
        void concatText(string textToAppend);
        VANet getVa();
        void plotSceneGraph(ZedGraph.ZedGraphControl graph, double[] radius, double[] angle);
        void fillWords(string speechFile, ListBox listbox, bool test = false);
        void updatePatientList();
        void updateApplicatorList();
        double getMeanSRT(double[] iterativeSNR);

        //double getActualSNR(double );
        void addCompletedAudiometry(TonalAudiometryTest Audiometry, string patientName);
    }
}
