
using Microsoft.AspNetCore.Mvc;
using AlertToCareAPI.Repositories;


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
       /* [HttpGet]
        public IActionResult GetVitals()
        {
            var vitals = _patientMonitoring.GetAllVitals();
                return Ok(vitals);
        }*/
        // GET: api/<PatientMonitoringController>/9245fe4a-d402-451c-b9ed-9c1a04247482
        [HttpGet]
        public IActionResult GetAlerts()
        {
            
            var patientVitals = _patientMonitoring.GetAllVitals();
            string vitalCheck="";
            foreach (var patient in patientVitals)
            {   
              vitalCheck= vitalCheck + patient.PatientId + " " + _patientMonitoring.CheckVitals(patient) + "\n";
                  
            }
            return Ok(vitalCheck);
        }

    }
}
