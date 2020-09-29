using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeScheduleMicroservice.Models
{
    public class RepSchedule
    {
        public string RepName { get; set; }
        public Doctor Doctor { get; set; }     
        public IEnumerable<string> Medicine { get; set; }
        public string MeetingSlot { get; set; }
        public DateTime  DateOfMeeting { get; set; }
       
    }
}
