using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRepresentativeScheduleMicroservice.Models;
using MedicalRepresentativeScheduleMicroservice.Repository;
namespace MedicalRepresentativeScheduleMicroservice.Provider
{
    public class RepScheduleProvider
    {
        readonly log4net.ILog _log4net;
        IRepScheduleRepository irepcon;
        public RepScheduleProvider(IRepScheduleRepository _irepcon)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(RepScheduleProvider));
            irepcon = _irepcon;
        }
        public  List<RepSchedule> scheduleList = new List<RepSchedule>();
        /// <summary>
        /// This is the function where we are creating the schedule for medical representative with the doctor.
        /// By giving date it will create schedule for 5 days starting from the start date ,excluding Sunday.
        /// The function will not create any schedule on Sunday.
        /// </summary>
        /// <param name="date"></param>
        IEnumerable<MedicineStock> stock;
        public void CreateSchedule(DateTime  date)
        {
            try
            {               
                int count = 0;
                while (count != 5)
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        Doctor doc = RepScheduleRepository.lsdoc[count];
                        IEnumerable<string> medicines = from m in stock where m.TargetAilment == doc.TreatingAilment select m.Name;
                        RepSchedule rp = new RepSchedule();
                        rp.RepName = MedicalRepresentativeRepository.medicalRepresentativeList[count % 3].Name;
                        rp.Medicine = medicines;
                        rp.Doctor = doc;
                        rp.MeetingSlot = "1 pm to 2 pm";
                        rp.DateOfMeeting = date;
                        count++;
                        scheduleList.Add(rp);
                    }
                    date = date.AddDays(1);
                 }
                if (scheduleList.Count == 0)
                    _log4net.Info(nameof(RepScheduleProvider) + "RepSchedule list is null");
                else 
                    _log4net.Info(nameof(RepScheduleProvider) + "Succesfully schedule created");
            }
            catch(Exception e)
            {
                _log4net.Error(nameof(RepScheduleProvider) + "Exception"+e.Message);
            }
        }

        /// <summary>
        /// This method is being called in the controller 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<RepSchedule> GetByDate(DateTime  date,string token)
        {
            try
            {
                if (date.DayOfWeek != DayOfWeek.Sunday)
                {
                    stock = irepcon.Get(token);
                    CreateSchedule(date);

                    _log4net.Info(nameof(RepScheduleProvider) + "GetByDate called");
                    return scheduleList;
                }
                return scheduleList;
            }
            catch(Exception e)
            {
                _log4net.Error(nameof(RepScheduleProvider) + "Exception" + e.Message);
                throw e;
            }
        }
    }
}
