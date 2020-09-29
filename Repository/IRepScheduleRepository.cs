using MedicalRepresentativeScheduleMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeScheduleMicroservice.Repository
{
   public interface IRepScheduleRepository
    {
        public IEnumerable<MedicineStock> Get(string token);

    }
}
