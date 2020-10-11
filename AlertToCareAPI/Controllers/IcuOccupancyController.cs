
using System.Collections.Generic;
using AlertToCareAPI.Repositories;
using AlertToCareAPI.ICUDatabase.Entities;
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
        public IEnumerable<Patient> Get()
        {
            return _occupantDb.GetAllPatients();
        }


        [HttpGet("Patients/{PatientId}")]
        public Patient Get(string patientId)
        {
            var patients = _occupantDb.GetAllPatients();
            foreach (var patient in patients)
            {
                if (patient.PatientId == patientId)
                {
                    return patient;
                }
            }
            return null;
        }

        [HttpPost("Patients")]
        public void Post([FromBody] Patient patient)
        {
            _occupantDb.AddPatient(patient);
        }

        [HttpPut("Patients/{PatientId}")]
        public void Put(string patientId, [FromBody] Patient patient)
        {
            _occupantDb.UpdatePatient(patientId, patient);
        }

        [HttpDelete("Patients/{PatientId}")]
        public void Delete(string patientId)
        {
            _occupantDb.RemovePatient(patientId);
        }
    }
}
