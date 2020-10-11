using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMonitoringController : ControllerBase
    {
        readonly Repository.IMonitoringRepository _patientMonitoring;
        public PatientMonitoringController(Repository.IMonitoringRepository patientMonitoring)
        {
            this._patientMonitoring = patientMonitoring;
        }
        // GET: api/<PatientMonitoringController>
        [HttpGet]
        public IEnumerable<Entities.Vitals> GetVitals()
        {
            return _patientMonitoring.GetAllVitals();
        }
        // GET: api/<PatientMonitoringController>/9245fe4a-d402-451c-b9ed-9c1a04247482
        [HttpGet("{patientId}")]
        public string GetAlert(string patientId)
        {
            var vitals = _patientMonitoring.GetAllVitals();
            foreach (var vital in vitals)
            {
                if (vital.PatientId == patientId)
                {
                    return _patientMonitoring.CheckVitals(vital);
                } 
            }
            return null;
        }

    }
}
