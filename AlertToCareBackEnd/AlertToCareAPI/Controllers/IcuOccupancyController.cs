using System;
using System.Data.SQLite;
using DataAccessLayer.BedManagement;
using DataAccessLayer.PatientManagement;
using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcuOccupancyController : ControllerBase
    {
        private readonly IPatientManagement _patientDb;
        private readonly IBedManagement _bedDb;
        public IcuOccupancyController(IPatientManagement patientDb, IBedManagement bedDb)
        {
            _patientDb = patientDb;
            _bedDb = bedDb;
        }

        [HttpGet("Patients")]
        public IActionResult Get()
        {
            return Ok(_patientDb.GetAllPatients());
        }

        [HttpGet("Patient/{PatientId}")]
        public IActionResult Get(string patientId)
        {
            try
            {
                return Ok(_patientDb.GetPatientById(patientId));
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
        }

        [HttpGet(template: "Beds/{icuId}")]
        public IActionResult GetAllAvailableBedsByIcuId(string icuId)
        {
            try
            {
                return Ok(_bedDb.GetAllAvailableBedsByIcuId(icuId));
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
        }

        [HttpPost("Patient")]
        public IActionResult Post([FromBody] Patient patient)
        {
            try
            {
                _patientDb.AddPatient(patient);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPut("Patient/{PatientId}")]
        public IActionResult Put(string patientId, [FromBody] Patient patient)
        {
            try
            {
                _patientDb.UpdatePatient(patientId, patient);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpDelete("Patient/{PatientId}")]
        public IActionResult Delete(string patientId)
        {
            try
            {
                _patientDb.RemovePatient(patientId);
                return Ok();
            }
            catch (SQLiteException exception)
            {
                return new ObjectResult(exception.Message) { StatusCode = 400 };
            }
        }
    }
}
