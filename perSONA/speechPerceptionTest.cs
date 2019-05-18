﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perSONA
{
    public class speechPerceptionTest
    {
        public string Label { get; set; }
        public double SignalToNoise { get; set; }
        public string SpeechFolder { get; set; }
        public string NoiseFile { get; set; }
        public double AngleSpeech { get; set; }
        public double RadiusSpeech { get; set; }
        public double AngleNoise { get; set; }
        public double RadiusNoise { get; set; }
        public double[] PresentingLogic { get; set; } = { 2, 1 };
        public double[] IterativeSNR { get; set; } = { };
        public double AcceptanceRule { get; set; } = 0.5;
        public double SignalToNoiseStep { get; set; } = 4;
        public DateTime TestStart { get; set; }

        public override string ToString()
        {
  
            return string.Format("New test: {0}\r\nSpeech R:{1} A:{2}\r\nNoise R:{3} A:{4} \r\n Logic: {5}-down-{6}-up, Criteria:{7}%, SNR:{8}dB SNR step:{9}dB", 
                                    Label, SpeechFolder, AngleSpeech, NoiseFile, AngleNoise, 
                                    PresentingLogic[0],PresentingLogic[1], AcceptanceRule*100, SignalToNoise, SignalToNoiseStep);
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
            RadiusNoise = radiusNoise;
            SignalToNoise = snr;
            SpeechFolder = speechFolder;
            NoiseFile = noiseFile;
            Label = label;
            TestStart = DateTime.Now;


        }

        public speechPerceptionTest(
                            double angleSpeech, double radiusSpeech,
                            double angleNoise, double radiusNoise,
                            string speechFolder, string noiseFile,
                            string label, double snr,
                            double[] presentingLogic,
                            double acceptanceRule, double signalToNoiseStep)
        {
            AngleSpeech = angleSpeech;
            RadiusSpeech = radiusSpeech;
            AngleNoise = angleNoise;
            RadiusNoise = radiusNoise;
            SignalToNoise = snr;
            SpeechFolder = speechFolder;
            NoiseFile = noiseFile;
            Label = label;
            PresentingLogic = presentingLogic;
            AcceptanceRule = acceptanceRule;
            SignalToNoiseStep = signalToNoiseStep;
            TestStart = DateTime.Now;
        }

    }
}
