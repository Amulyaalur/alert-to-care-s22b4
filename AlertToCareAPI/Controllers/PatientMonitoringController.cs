using Microsoft.AspNetCore.Mvc;
using AlertToCareAPI.Repositories;
using DataAccessLayer.VitalManagement;
using DataModels;


namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMonitoringController : ControllerBase
    {
        readonly IMonitoringRepository _patientMonitoring;
        private readonly IVitalManagement _vitalDb;
        public PatientMonitoringController(IMonitoringRepository patientMonitoring, IVitalManagement vitalDb)
        {
            _vitalDb = vitalDb;
            this._patientMonitoring = patientMonitoring;
        }
        [HttpGet(template:"Vitals")]
        public IActionResult GetAllPatientsVitals()
        {
            return Ok(_vitalDb.GetAllPatientsVitals());
        }
        [HttpGet(template:"Alerts")]
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

        [HttpPut(template: "Vital/{patientId}")]
        public IActionResult UpdateVitalsByPatientId(string patientId, [FromBody] Vital vital)
        {
            try
            {
                _vitalDb.UpdateVitalByPatientId(patientId, vital);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        

    }
}
