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
        public IEnumerable<Entities.Vitals> Get()
        {
            return _patientMonitoring.GetAllVitals();
        }
        // GET: api/<PatientMonitoringController>/12
        [HttpGet("{mrn}")]
        public string Get(Guid mrn)
        {
            var vitals = _patientMonitoring.GetAllVitals();
            foreach (var vital in vitals)
            {
                if (vital.Mrn == mrn)
                {
                    return _patientMonitoring.CheckVitals(vital);
                } 
            }
            return null;
        }

    }
}
