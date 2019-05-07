using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perSONA
{
    public class speechPerceptionTest
    {
        public string Label { get; set; } = "";
        public double SignalToNoise { get; set; } = 0;
        public string SpeechFolder { get; set; } = "N/A";
        public string NoiseFile { get; set; } = "N/A";
        public double AngleSpeech { get; set; } = 0;
        public double RadiusSpeech { get; set; } = 2;
        public double AngleNoise { get; set; } = 0;
        public double RadiusNoise { get; set; } = 2;
        public double[] PresentingLogic { get; set; } = { 2, 1 };
        public double[] IterativeSNR { get; set; } = { };
        public double AcceptanceRule { get; set; } = 0.5;
        public double SignalToNoiseStep { get; set; } = 4;

        public override string ToString()
        {
  
            return string.Format("Test {0}, Speech: {1} {2}, Noise: {3}, {4}", Label, SpeechFolder, AngleSpeech, NoiseFile, AngleNoise);
        }

        public speechPerceptionTest(
                                    double angleSpeech, double radiusSpeech, 
                                    double angleNoise, double radiusNoise,
                                    string speechFolder, string noiseFile,
                                    string label, double snr)
        {
            AngleSpeech = angleSpeech;
            RadiusSpeech = radiusSpeech;
            AngleNoise = angleNoise;
            RadiusNoise = angleNoise;
            SignalToNoise = snr;
            SpeechFolder = speechFolder;
            NoiseFile = noiseFile;
            Label = label;


        }

    }
}
