using System;
using System.Data.SQLite;
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
                _vitalDb.UpdateVitalByPatientId(patientId, vital);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) {StatusCode = 400};
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
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
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
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
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
        }

    }
}
