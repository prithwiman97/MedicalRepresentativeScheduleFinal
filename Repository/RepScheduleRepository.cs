using MedicalRepresentativeScheduleMicroservice.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace MedicalRepresentativeScheduleMicroservice.Repository
{
    public class RepScheduleRepository : IRepScheduleRepository
    {
        IConfiguration configuration;
        readonly log4net.ILog _log4net;  
        public static List<Doctor> lsdoc = new List<Doctor>
        {
            new Doctor
            {
            DoctorName="D1",
            TreatingAilment="Orthopaedics",
            ContactNo=22244469
            },
            new Doctor
            {
              DoctorName="D2",
            TreatingAilment="General",
            ContactNo=22244470
            },
             new Doctor
            {
              DoctorName="D3",
            TreatingAilment="General",
            ContactNo=22244471
            },
               new Doctor
            {
              DoctorName="D4",
            TreatingAilment="Orthopaedics",
            ContactNo=22244472
            },
               new Doctor
            {
              DoctorName="D5",
            TreatingAilment="Gynaecology",
            ContactNo=22244473
            }
        };
        HttpClient client;
        Uri baseaddress;
        public RepScheduleRepository(IConfiguration config)
        {

           
            configuration = config;
            baseaddress = new Uri(configuration["Links:MedicineStock"]);
            client = new HttpClient();
            client.BaseAddress = baseaddress;
            _log4net = log4net.LogManager.GetLogger(typeof(RepScheduleRepository));
        }
        
        

        /// <summary>
        /// This function will fetch the MedicineStock by calling MedicineStock microservice
        /// </summary>
        /// <returns></returns>

        public IEnumerable<MedicineStock> Get(string token)
        {
            try
            {
                List<MedicineStock> medicineStockList = new List<MedicineStock>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    medicineStockList = JsonConvert.DeserializeObject<List<MedicineStock>>(data);
                    _log4net.Info(nameof(RepScheduleRepository)+"Medicine stock successfully fetched");
                    return medicineStockList;
                }
                _log4net.Info(nameof(RepScheduleRepository) + "Medicine stock null");
                return medicineStockList;
            }
            catch(Exception e)
            {
                _log4net.Error(nameof(RepScheduleRepository)+"Exception"+e.Message);
                throw e;
            }          
        }       
    }
}
