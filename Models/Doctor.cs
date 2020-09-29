using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeScheduleMicroservice.Models
{
    public class Doctor
    {
        public string DoctorName { get; set; }
        public long ContactNo { get; set; }
        public string TreatingAilment { get; set; }
    }
}
