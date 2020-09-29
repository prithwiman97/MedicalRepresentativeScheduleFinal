using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRepresentativeScheduleMicroservice.Models;
using MedicalRepresentativeScheduleMicroservice.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRepresentativeScheduleMicroservice.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RepScheduleController : ControllerBase
    {
        readonly log4net.ILog _log4net;

        private readonly RepScheduleProvider _providercon;
        public RepScheduleController(RepScheduleProvider provider)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(RepScheduleController));

            _providercon = provider;
        }
    

          [HttpGet("{date}")]
        public IActionResult Get(DateTime  date)
        {

            string token ;

          

            try
            {
                if (HttpContext==null)
                {
                    token = "";
                }
                else
                    token = HttpContext.Request.Headers["Authorization"].FirstOrDefault().Split(" ")[1];
                


                if (date != null)
                {
                    IEnumerable<RepSchedule> res = _providercon.GetByDate(date, token);
                    if (res.Count() == 0)
                    {
                        return NotFound(res);
                    }
                    _log4net.Info(nameof(RepScheduleController)+" Http GET request using Date");

                    return Ok(res);
                }
                else
                {
                    _log4net.Info(nameof(RepScheduleController)+"Date is null ");
                    
                    return BadRequest();
                }
            }
            catch(Exception e)
            {    
                _log4net.Error(nameof(RepScheduleController)+"Exception"+e.Message);
                return StatusCode(500);
            }
        }

    }
}
