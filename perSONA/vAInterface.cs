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
        void addCompletedTest(speechPerceptionTest test);
        string getDatabaseFolder();
        void createAcousticScene(string speechFile, string noiseFile);
        string getTitle(string speechFile);
        TimeSpan getDuration(string speechFile);
        void concatText(string textToAppend);
        VANet getVa();
        void plotSceneGraph(ZedGraph.ZedGraphControl graph, double[] radius, double[] angle);
        void fillWords(string speechFile, ListBox listbox);
        void updatePatientList();
        double getMeanSRT(double[] iterativeSNR);
        void addCompletedAudiometry(TonalAudiometryTest Audiometry, string patientName);
    }
}
