using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA;

namespace perSONA
{
    public interface IvAInterface
    {

        void playScene(double radius, double angle);
        string getDatabaseFolder();
        void createAcousticScene(string speechFile, string noiseFile);
        string getTitle(string speechFile);
        TimeSpan getDuration(string speechFile);
        void concatText(String textToAppend);
        VANet getVa();

    }
}
