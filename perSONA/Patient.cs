using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perSONA
{
    public class Patient
    {
        public string Name { get; set; }
        public string Scolarity { get; set; }
        public DateTime BornDate { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Motivation { get; set; }
        public string Observations { get; set; }
        public string[] Tests { get; set; } = { };
        public string[] Audiometrys { get; set; } = { };
        public DateTime UpdatedAt { get; set; }
        
        public string OtherConditions { get; set; }
        public string Work { get; set; }

        public DateTime LeftActivationDate { get; set; }
        public DateTime LeftDiagnosis { get; set; }
        public string LeftEarDevice { get; set; }
        public string LeftHearingLoss { get; set; }
        public string LeftLossDegree { get; set; }
        public string LeftManufacturer { get; set; }
        public string LeftModel { get; set; }
        public string LeftPostLingual { get; set; }
        public string LeftPrivationYears { get; set; }

        public DateTime RightActivationDate { get; set; }
        public DateTime RightDiagnosis { get; set; }
        public string RightEarDevice { get; set; }
        public string RightHearingLoss { get; set; }
        public string RightLossDegree { get; set; }
        public string RightManufacturer { get; set; }
        public string RightModel { get; set; }
        public string RightPostLingual { get; set; }
        public string RightPrivationYears { get; set; }


    }
}
