
using AlertToCareAPI.Repositories;
using AlertToCareAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlertToCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcuOccupancyController : ControllerBase
    {
        readonly IPatientDbRepository _occupantDb;
        public IcuOccupancyController(IPatientDbRepository repo)
        {
            this._occupantDb = repo;
        }

        [HttpGet("Patients")]
        public IActionResult Get()
        {
            return Ok(_occupantDb.GetAllPatients());

        }

        [HttpGet("Patients/{PatientId}")]
        public IActionResult Get(string patientId)
        {
            var patients = _occupantDb.GetAllPatients();
                foreach (var patient in patients)
                {
                    if (patient.PatientId == patientId)
                    {
                        return Ok(patient);
                    }
                }

                return BadRequest();
        }

        [HttpPost("Patients")]
        public IActionResult Post([FromBody] Patient patient)
        {
            try
            {
                _occupantDb.AddPatient(patient);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Patients/{PatientId}")]
        public IActionResult Put(string patientId, [FromBody] Patient patient)
        {
            try
            {
                _occupantDb.UpdatePatient(patientId, patient);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("Patients/{PatientId}")]
        public IActionResult Delete(string patientId)
        {
            try
            {
                _occupantDb.RemovePatient(patientId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
