
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
                // Write an if statemt if status is normal then skip/continue or print vitals
                // For normal status code, 0 for normal >0 for abnormal
              vitalCheck= vitalCheck+ " " + "BPM: "+_patientMonitoring.CheckBpm(patient.Bpm)+ "\tRespRate:"+ _patientMonitoring.CheckRespRate(patient.RespRate) + "\tSPO2:" + _patientMonitoring.CheckSpo2(patient.Spo2) + "\n";
                  
            }
            return Ok(vitalCheck);
        }

    }
}
