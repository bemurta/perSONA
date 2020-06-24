using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perSONA
{
    class calibrationData
    {
        //SLM
        public string SLMBrand { get; set; }
        public string SLMModel { get; set; }
        public string SLMSerialNumber { get; set; }
        public string SLMCalibrationNumber { get; set; }

        //iPhone
        public string IPhoneModel { get; set; }
        public string IOSVersion { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }

        //Mannequin
        public string MannequinBrand { get; set; }
        public string MannequinModel { get; set; }
        public string MannequinPinnae { get; set; }

        //Variáveis que servem para mais de um modo
        public string MicrophoneBrand { get; set; }
        public string MicrophoneModel { get; set; }
        public string MicrophoneSerialNumber { get; set; }
        public DateTime LastCalibrationDate { get; set; }
        public bool NotCalibrate { get; set; }

        public DateTime CalibrationDateTime { get; set; }
    }
}