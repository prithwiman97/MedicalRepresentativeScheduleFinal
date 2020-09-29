using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRepresentativeScheduleMicroservice.Models;
using MedicalRepresentativeScheduleMicroservice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRepresentativeScheduleMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRepresentativeController : ControllerBase
    {     
        [HttpGet]
        public IEnumerable<MedicalRepresentative> Get()
        {
            return MedicalRepresentativeRepository.medicalRepresentativeList;
        }
     }
}
