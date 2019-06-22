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
        public DateTime UpdatedAt { get; set; }


    }
}
