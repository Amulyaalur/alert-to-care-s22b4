using DataAccessLayer.AlertManagement;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.VitalManagement;
using DataModels;


namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMonitoringController : ControllerBase
    {
        private readonly IVitalManagement _vitalDb;
        private readonly IAlertManagement _alertDb;
        public PatientMonitoringController(IVitalManagement vitalDb, IAlertManagement alertDb)
        {
            _vitalDb = vitalDb;
            _alertDb = alertDb;
        }
        [HttpGet(template:"Vitals")]
        public IActionResult GetAllPatientsVitals()
        {
            return Ok(_vitalDb.GetAllPatientsVitals());
        }

        [HttpGet(template:"Alerts")]
        public IActionResult GetAlerts()
        {
            return Ok(_alertDb.GetAllAlerts());
        }

        [HttpPut(template: "Vital/{patientId}")]
        public IActionResult UpdateVitalsByPatientId(string patientId, [FromBody] Vital vital)
        {
            try
            {

                return Ok(_vitalDb.UpdateVitalByPatientId(patientId, vital));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut(template: "Alert/{alertId}")]
        public IActionResult ToggleAlertStatusByAlertId(int alertId)
        {
            try
            {
                _alertDb.ToggleAlertStatusByAlertId(alertId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete(template: "Alert/{alertId}")]
        public IActionResult DeleteAlertByAlertId(int alertId)
        {
            try
            {
                _alertDb.DeleteAlertByAlertId(alertId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
