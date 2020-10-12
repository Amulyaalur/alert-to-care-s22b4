
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AlertToCareAPI.Repositories;
using AlertToCareAPI.ICUDatabase.Entities;


namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMonitoringController : ControllerBase
    {
        readonly IMonitoringRepository _patientMonitoring;
        public PatientMonitoringController(IMonitoringRepository patientMonitoring)
        {
            this._patientMonitoring = patientMonitoring;
        }
        // GET: api/<PatientMonitoringController>
        [HttpGet]
        public IActionResult GetVitals()
        {
            var vitals = _patientMonitoring.GetAllVitals();
                return Ok(vitals);
        }
        // GET: api/<PatientMonitoringController>/9245fe4a-d402-451c-b9ed-9c1a04247482
        [HttpGet("{patientId}")]
        public IActionResult GetAlert(string patientId)
        {
            
            var vitals = _patientMonitoring.GetAllVitals();
            foreach (var vital in vitals)
            {   
                if (vital.PatientId == patientId)
                {  
                    string vitalCheck= _patientMonitoring.CheckVitals(vital);
                    return Ok(vitalCheck);
                } 
            }
            return NotFound();
        }

    }
}
